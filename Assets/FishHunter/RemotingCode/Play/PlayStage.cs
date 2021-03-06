﻿using System;
using System.Collections.Generic;
using System.Linq;


using Regulus.Remoting;
using Regulus.Utility;


using VGame.Project.FishHunter.Common.Data;
using VGame.Project.FishHunter.Common.GPI;

namespace VGame.Project.FishHunter.Play
{
	internal class PlayStage : IPlayer, IStage
	{
		public delegate void KillCallback(int kill_count);

		public delegate void PassCallback(int pass_stage);

		private event Action<int> _DeathFishEvent;

		private event Action<int> _MoneyEvent;

		public event KillCallback KillEvent;

		public event PassCallback PassEvent;

		private readonly ISoulBinder _Binder;

		private readonly List<Bullet> _Bullets;

		private readonly List<Fish> _Fishs;

		private readonly IFishStage _FishStage;

		private readonly GamePlayerRecord _Money;

		private readonly Dictionary<int, HitRequest> _Requests;

		private int _DeadFishCount;

		private short _FishIdSn;

		private int _WeaponBet;

		private WEAPON_TYPE _WeaponType;

		public PlayStage(ISoulBinder binder, IFishStage fish_stage, GamePlayerRecord money)
		{
			_Fishs = new List<Fish>();
			_Bullets = new List<Bullet>();
			_Requests = new Dictionary<int, HitRequest>();
			_Binder = binder;
			_FishStage = fish_stage;
			_DeadFishCount = 0;
			_Money = money;
			_WeaponType = WEAPON_TYPE.NORMAL;
			_WeaponBet = 10;
		}

		Value<int> IPlayer.Hit(int bullet_id, int[] fishids)
		{
			var hasBullet = _PopBullet(bullet_id);
			if(hasBullet == false)
			{
				return 0;
			}

			var logFishs = string.Empty;
			var count = 0;
			foreach(var fishid in fishids)
			{
				if(_PopFish(fishid) == false)
				{
					continue;
				}

				if(_Requests.ContainsKey(fishid))
				{
					continue;
				}

				count++;

				var fishs = new List<RequsetFishData>
				{
					new RequsetFishData
					{
						FishId = fishid, 
						FishOdds = 1, 
						FishStatus = FISH_STATUS.NORMAL, 
						FishType = FISH_TYPE.TROPICAL_FISH
					}
				};

				var weapon = new RequestWeaponData
				{
					TotalHits = fishids.Length, 
					WeaponBet = _WeaponBet, 
					BulletId = bullet_id, 
					WeaponOdds = 1, 
					WeaponType = _WeaponType
				};

				var request = new HitRequest(fishs.ToArray(), weapon);
				_Requests.Add(fishid, request);
				_FishStage.Hit(request);

				logFishs += fishid + ",";
			}

			if(count == 0)
			{
				_PushBullet(bullet_id);
			}

			Singleton<Log>.Instance.WriteInfo(
				string.Format(
					"all WEAPON_TYPE:{0} , targets:{1} , count:{2}", 
					bullet_id, 
					string.Join(",", (from id in fishids select id.ToString()).ToArray()), 
					fishids.Length));
			Singleton<Log>.Instance.WriteInfo(
				string.Format("requested WEAPON_TYPE:{0} , targets:{1} , count:{2}", bullet_id, logFishs, count));
			Singleton<Log>.Instance.WriteInfo(
				string.Format(
					"request fishs:{0} count:{1} ", 
					string.Join(",", (from id in _Requests.Keys select id.ToString()).ToArray()), 
					_Requests.Count));
			return count;
		}

		void IPlayer.Quit()
		{
			KillEvent(_DeadFishCount);
		}

		event Action<int> IPlayer.DeathFishEvent
		{
			add { _DeathFishEvent += value; }
			remove { _DeathFishEvent -= value; }
		}

		event Action<int> IPlayer.MoneyEvent
		{
			add { _MoneyEvent += value; }
			remove { _MoneyEvent -= value; }
		}

		int IPlayer.WeaponOdds
		{
			get { return _WeaponBet; }
		}

		WEAPON_TYPE IPlayer.WeaponType
		{
			get { return _WeaponType; }
		}

		Value<int> IPlayer.RequestBullet()
		{
			if(HasMoney(1) == false)
			{
				return 0;
			}

			AddMoney(-1);

			return _CreateBullet();
		}

		Value<short> IPlayer.RequestFish()
		{
			checked
			{
				var fishid = ++_FishIdSn;
				_Fishs.Add(new Fish(fishid));
				return fishid;
			}
		}

		void IPlayer.EquipWeapon(WEAPON_TYPE weapon_type, int odds)
		{
			_WeaponType = weapon_type;
			_WeaponBet = odds;
		}

		void IStage.Enter()
		{
			_Binder.Bind<IPlayer>(this);
			_FishStage.OnTotalHitResponseEvent += _FishStage_OnTotalHitResponseEvent;
		}

		void _FishStage_OnTotalHitResponseEvent(HitResponse[] hit_responses)
		{
			foreach(var hit in hit_responses)
			{
				_Response(hit);
			}
			
		}

		void IStage.Leave()
		{
			_FishStage.OnTotalHitResponseEvent -= _FishStage_OnTotalHitResponseEvent;
			_Binder.Unbind<IPlayer>(this);
		}

		void IStage.Update()
		{
		}

		private void _Response(HitResponse obj)
		{
			HitRequest request;
			if(!_Requests.TryGetValue(obj.FishId, out request))
			{
				return;
			}

			switch(obj.DieResult)
			{
				case FISH_DETERMINATION.DEATH:
					var onDeathFish = _DeathFishEvent;
					if(onDeathFish != null)
					{
						onDeathFish(obj.FishId);
					}

					AddMoney(request.WeaponData.GetTotalBet());
					_DeadFishCount++;
					break;

				case FISH_DETERMINATION.SURVIVAL:
					_PushFish(obj.FishId);
					break;
			}

			_Requests.Remove(obj.FishId);
		}

		private void _PushFish(int id)
		{
			_Fishs.Add(new Fish(id));
		}

		private bool _PopFish(int fishid)
		{
			return _Fishs.RemoveAll(fish => fish.Id == fishid) > 0;
		}

		private void _PushBullet(int bulletid)
		{
			_Bullets.Add(new Bullet(bulletid));
		}

		private bool _PopBullet(int bulletid)
		{
			return _Bullets.RemoveAll(b => b.Id == bulletid) > 0;
		}

		private void AddMoney(int p)
		{
			_Money.Money += p;
			_MoneyEvent(_Money.Money);
		}

		private bool HasMoney(short bullet_score)
		{
			return _Money.Money >= bullet_score;
		}

		private Value<int> _CreateBullet()
		{
			var bullet = new Bullet();
			_Bullets.Add(bullet);
			return bullet.Id;
		}
	}
}
