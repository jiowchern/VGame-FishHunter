﻿using VGame.Project.FishHunter.Common.Data;
using VGame.Project.FishHunter.Formula.ZsFormula.Data;

namespace VGame.Project.FishHunter.Formula.ZsFormula.Rule
{
	/// <summary>
	///     平均押注的调整
	/// </summary>
	public class AdjustmentAverageRule
	{
		private readonly DataVisitor _DataVisitor;

		private readonly HitRequest _HitRequest;

		public AdjustmentAverageRule(DataVisitor fish_visitor, HitRequest hit_request)
		{
			_DataVisitor = fish_visitor;
			_HitRequest = hit_request;
		}

		public void Run()
		{
			var bufferData = _DataVisitor.Farm.FindBuffer(
				_DataVisitor.FocusBufferBlock, 
				FarmBuffer.BUFFER_TYPE.NORMAL);

			// 前1000局，按照实际总玩分/总玩次，获得平均押注
			// 之后，每次减去1/100000，再补上最新的押注
			if(bufferData.BufferTempValue.AverageTimes < 1000)
			{
				bufferData.BufferTempValue.AverageTimes += 1;

				bufferData.BufferTempValue.AverageTotal += _HitRequest.WeaponData.GetTotalBet();

				bufferData.BufferTempValue.AverageValue = bufferData.BufferTempValue.AverageTotal
														/ bufferData.BufferTempValue.AverageTimes;

				if(bufferData.BufferTempValue.AverageTimes == 1000)
				{
					bufferData.BufferTempValue.AverageTotal = bufferData.BufferTempValue.AverageTotal / 1000 * 100000;
				}
			}
			else
			{
				bufferData.BufferTempValue.AverageTotal -= bufferData.BufferTempValue.AverageTotal / 100000;
				bufferData.BufferTempValue.AverageTotal += _HitRequest.WeaponData.GetTotalBet();
				bufferData.BufferTempValue.AverageValue = bufferData.BufferTempValue.AverageTotal / 100000;
			}
		}

		
	}
}
