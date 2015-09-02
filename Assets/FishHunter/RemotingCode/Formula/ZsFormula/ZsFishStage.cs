﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;


using Regulus.Extension;
using Regulus.Remoting;
using Regulus.Utility;


using VGame.Project.FishHunter.Common.Data;
using VGame.Project.FishHunter.Common.GPI;
using VGame.Project.FishHunter.Formula.ZsFormula.Data;


using Random = Regulus.Utility.Random;

namespace VGame.Project.FishHunter.Formula.ZsFormula
{
	internal class ZsFishStage : IFishStage
	{
		private event Action<HitResponse[]> _OnTotalHitResponseEvent;

		private readonly Guid _AccountId;

		private readonly FishFarmData _FishFarmData;

		private readonly IFormulaFarmRecorder _FormulaFarmRecorder;

		private readonly FormulaPlayerRecord _FormulaPlayerRecord;

		private readonly IFormulaPlayerRecorder _FormulaPlayerRecorder;

		private Action<string> _OnHitExceptionEvent;

		public ZsFishStage(
			Guid account_id, 
			FishFarmData fish_farm_data, 
			FormulaPlayerRecord formula_player_record, 
			IFormulaPlayerRecorder formula_player_recorder, 
			IFormulaFarmRecorder formula_stage_data_recorder)
		{
			_AccountId = account_id;
			_FishFarmData = fish_farm_data;

			_FormulaFarmRecorder = formula_stage_data_recorder;
			_FormulaPlayerRecorder = formula_player_recorder;

			_FormulaPlayerRecord = formula_player_record;
		}

		event Action<string> IFishStage.OnHitExceptionEvent
		{
			add { _OnHitExceptionEvent += value; }
			remove { _OnHitExceptionEvent -= value; }
		}

		event Action<HitResponse[]> IFishStage.OnTotalHitResponseEvent
		{
			add { _OnTotalHitResponseEvent += value; }
			remove { _OnTotalHitResponseEvent -= value; }
		}

		Guid IFishStage.AccountId
		{
			get { return _AccountId; }
		}

		int IFishStage.FishStage
		{
			get { return _FishFarmData.FarmId; }
		}

		void IFishStage.Hit(HitRequest request)
		{
			if(!_CheckDataLegality(request))
			{
				return;
			}

			_SetFishKingOdds(request);

			var totalRequest = new ZsHitChecker(_FishFarmData, _FormulaPlayerRecord, _CreateRandoms()).TotalRequest(request);

			_FormulaFarmRecorder.Save(_FishFarmData);
			_FormulaPlayerRecorder.Save(_FormulaPlayerRecord);

			_OnTotalHitResponseEvent.Invoke(totalRequest);

			_MakeLog(request, totalRequest);
		}

		/// <summary>
		///     檢查資料合法性
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		private bool _CheckDataLegality(HitRequest request)
		{
			if(request.FishDatas.Any(x => x.FishOdds <= 0))
			{
				_OnHitExceptionEvent.Invoke("FishData.Odds 不可為0");
				Singleton<Log>.Instance.WriteInfo("FishData.Odds 不可為0");
				return false;
			}

			if(request.FishDatas.Any(x => x.FishStatus < FISH_STATUS.NORMAL || x.FishStatus > FISH_STATUS.FREEZE))
			{
				_OnHitExceptionEvent.Invoke("FishData.FishStatus 無此狀態");
				Singleton<Log>.Instance.WriteInfo("FishData.FishStatus 無此狀態");
				return false;
			}

			if(request.WeaponData.TotalHits <= 0)
			{
				_OnHitExceptionEvent.Invoke("WeaponData.TotalHitOdds 不可為0");
				Singleton<Log>.Instance.WriteInfo("WeaponData.TotalHitOdds 不可為0");
				return false;
			}

			if(request.WeaponData.WeaponType < WEAPON_TYPE.NORMAL || request.WeaponData.WeaponType > WEAPON_TYPE.FREEZE_BOMB)
			{
				_OnHitExceptionEvent.Invoke("WeaponData.WeaponType，無此編號");
				Singleton<Log>.Instance.WriteInfo("WeaponData.WeaponType，無此編號");
				return false;
			}

			if(request.WeaponData.WeaponBet <= 0 || request.WeaponData.WeaponBet > 1000)
			{
				_OnHitExceptionEvent.Invoke("WeaponData.WeaponBet，押注分數錯誤");
				Singleton<Log>.Instance.WriteInfo("WeaponData.WeaponBet，押注分數錯誤");
				return false;
			}

			if(request.WeaponData.WeaponOdds != 1)
			{
				_OnHitExceptionEvent.Invoke("WeaponData.OddsResult，目前只能為1");
				Singleton<Log>.Instance.WriteInfo("WeaponData.OddsResult，目前只能為1");
				return false;
			}

			if(request.WeaponData.TotalHits != request.FishDatas.Length)
			{
				_OnHitExceptionEvent.Invoke("WeaponData.TotalHits，擊中數量不符");
				Singleton<Log>.Instance.WriteInfo("WeaponData.TotalHits，擊中數量不符");
				return false;
			}

			return true;
		}

		/// <summary>
		///     魚王倍數 = 所有同種類魚的 total odds
		/// </summary>
		/// <param name="request"></param>
		private void _SetFishKingOdds(HitRequest request)
		{
			var fishKings = request.FishDatas.Where(x => x.FishStatus == FISH_STATUS.KING).ToArray();

			if(!fishKings.Any())
			{
				return;
			}

			foreach(var king in fishKings)
			{
				var smallFishs = request.FishDatas.Where(x => x.FishStatus != FISH_STATUS.KING && x.FishType == king.FishType);

				king.FishOdds += smallFishs.Sum(x => x.FishOdds);
			}
		}

		private Value<FormulaPlayerRecord> _StroageLoad(Guid account_id)
		{
			var returnValue = new Value<FormulaPlayerRecord>();

			var val = _FormulaPlayerRecorder.Query(account_id);

			val.OnValue += game_player_record => { returnValue.SetValue(game_player_record); };

			return returnValue;
		}

		private void _MakeLog(HitRequest request, IEnumerable<HitResponse> responses)
		{
			const string formats = "PlayerVisitor:{0}\tStage:{1}\r\n<Request>\r\n{2}\r\n<Response>\r\n{3}";

			var log = string.Format(
				formats, 
				_AccountId, 
				_FishFarmData.FarmId, 
				_MakeRequestLog(request), 
				_MakeResponesLog(responses));

			Singleton<Log>.Instance.WriteInfo(log);
		}

		private string _MakeRequestLog(HitRequest request)
		{
			var weapon = request.WeaponData;

		    string weaponDataLog =
		        string.Format("WeaponData\r\n{0,-7}{1,-32}{2,-7}{3,-7}{4,-7}\r\n{5,-7}{6,-7}{7,-7}{8,-7}{9,-7}\r\n" 
                ,"Bullet" , "WeaponType" , "Bet" , "Odds" , "Hits" 
                , weapon.BulletId , weapon.WeaponType , weapon.WeaponBet , weapon.WeaponOdds , weapon.TotalHits);            

			var fishTitle = string.Format(
				"FishData\r\n{0,-7}{1,-32}{2,-32}{3,-7}\r\n", 
				"Id", 
				"FishType", 
				"FishStatus", 
				"Odds");

			var fishs = request.FishDatas;

			return weaponDataLog + fishTitle
					+ string.Join(
						"\r\n", 
						fishs.Select(
							fish_data =>
							string.Format(
								"{0,-7}{1,-32}{2,-32}{3,-7}", 
								fish_data.FishId, 
								fish_data.FishType, 
								fish_data.FishStatus, 
								fish_data.FishOdds)).ToArray());
		}

		private string _MakeResponesLog(IEnumerable<HitResponse> responses)
		{
			var title = string.Format(
				"{0,-7}{1,-7}{2,-16}{3,-64}{4,-7}{5,-24}{6,-24}\r\n", 
				"WepId", 
				"FishId", 
				"DisResult", 
				"FeedbackWeapons", 
				"Bet", 
				"OddsResult", 
				"DieRate");

			return title + string.Join(
				"\r\n", 
				(from response in responses
				let fws = response.FeedbackWeapons.ShowMembers(",")
				select
					string.Format(
						"{0,-7}{1,-7}{2,-16}{3,-64}{4,-7}{5,-24}{6,-24}", 
						response.WepId, 
						response.FishId, 
						response.DieResult, 
						fws, 
						response.WeaponBet, 
						response.OddsResult, 
						response.DieRate)).ToArray());
		}

		private List<RandomData> _CreateRandoms()
		{
			var rs = new List<RandomData>
			{
				_CreateAdjustPlayerPhaseRandom(), 
				_CreateTreasureRandom(), 
				_CreateDeathRandom(), 
				_CreateOddsRandom()
			};

			return rs;
		}

		private RandomData _CreateAdjustPlayerPhaseRandom()
		{
			var data = new RandomData
			{
				RandomType = RandomData.RULE.ADJUSTMENT_PLAYER_PHASE, 
				Randoms = new List<IRandom>()
			};

			data.Randoms.Add(Random.Instance);
			data.Randoms.Add(Random.Instance);

			return data;
		}

		private RandomData _CreateTreasureRandom()
		{
			var data = new RandomData
			{
				RandomType = RandomData.RULE.CHECK_TREASURE, 
				Randoms = new List<IRandom>()
			};

			data.Randoms.Add(Random.Instance);
			data.Randoms.Add(Random.Instance);

			return data;
		}

		private RandomData _CreateDeathRandom()
		{
			var data = new RandomData
			{
				RandomType = RandomData.RULE.DEATH, 
				Randoms = new List<IRandom>()
			};

			data.Randoms.Add(Random.Instance);
			data.Randoms.Add(Random.Instance);

			return data;
		}

		private RandomData _CreateOddsRandom()
		{
			var data = new RandomData
			{
				RandomType = RandomData.RULE.ODDS, 
				Randoms = new List<IRandom>()
			};

			data.Randoms.Add(Random.Instance);

			data.Randoms.Add(Random.Instance);

			data.Randoms.Add(Random.Instance);

			data.Randoms.Add(Random.Instance);

			data.Randoms.Add(Random.Instance);

			return data;
		}
	}
}
