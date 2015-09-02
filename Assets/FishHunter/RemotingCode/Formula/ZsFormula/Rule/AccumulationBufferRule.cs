﻿using System.Linq;


using Regulus.Utility;


using VGame.Project.FishHunter.Common.Data;
using VGame.Project.FishHunter.Formula.ZsFormula.Data;

namespace VGame.Project.FishHunter.Formula.ZsFormula.Rule
{
	/// <summary>
	///     算法伺服器-累積buffer規則
	/// </summary>
	public class AccumulationBufferRule
	{
		private readonly HitRequest _Request;

		private readonly DataVisitor _Visitor;

		public AccumulationBufferRule(DataVisitor visitor, HitRequest request)
		{
			_Visitor = visitor;
			_Request = request;
		}

		public void Run()
		{
			var enumData = EnumHelper.GetEnums<FarmBuffer.BUFFER_TYPE>();

			foreach(var data in enumData.Select(buffer_type => _Visitor.Farm.FindBuffer(_Visitor.FocusBufferBlock, buffer_type)))
			{
				_AddBufferRate(data);
			}

			_Record();
		}

		private void _AddBufferRate(FarmBuffer data)
		{
			data.Count += _Request.WeaponData.GetTotalBet() * data.Rate;

			if(data.Count < 1000)
			{
				return;
			}

			data.Buffer += data.Count / 1000;
			data.Count = data.Count % 1000;
		}

		private void _Record()
		{
			_Visitor.Farm.Record.PlayTimes += 1;
			_Visitor.Farm.Record.PlayTotal += _Request.WeaponData.GetTotalBet();

			_Visitor.PlayerRecord.FindFarmRecord(_Visitor.Farm.FarmId).PlayTimes += 1;
			_Visitor.PlayerRecord.FindFarmRecord(_Visitor.Farm.FarmId).PlayTotal += _Request.WeaponData.GetTotalBet();
		}

		
	}
}
