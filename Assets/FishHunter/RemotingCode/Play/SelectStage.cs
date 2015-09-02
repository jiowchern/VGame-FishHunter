﻿using System;
using System.Linq;


using Regulus.Remoting;
using Regulus.Utility;


using VGame.Project.FishHunter.Common;
using VGame.Project.FishHunter.Common.GPI;

namespace VGame.Project.FishHunter.Play
{
	internal class SelectStage : IStage, ILevelSelector
	{
		public delegate void DoneCallback(IFishStage fish_stage);

		public event DoneCallback DoneEvent;

		private readonly ISoulBinder _Binder;

		private readonly IFishStageQueryer _FishStageQueryer;

		private readonly int[] _Stages;

		private bool _Querying;

		public SelectStage(int[] stages, ISoulBinder binder, IFishStageQueryer fish_stag_queryer)
		{
			_Binder = binder;
			_FishStageQueryer = fish_stag_queryer;
			_Querying = false;
			_Stages = stages;
		}

		Value<bool> ILevelSelector.Select(int level)
		{
			if(_Check(level) == false)
			{
				return false;
			}

			if(_Querying == false)
			{
				_Querying = true;
				var val = new Value<bool>();
				checked
				{
					_FishStageQueryer.Query(Guid.Empty, (byte)level).OnValue += fish_stage =>
					{
						if(fish_stage != null)
						{
							DoneEvent(fish_stage);
							val.SetValue(true);
						}
						else
						{
							val.SetValue(false);
						}

						_Querying = false;
					};
				}

				return val;
			}

			return false;
		}

		Value<int[]> ILevelSelector.QueryStages()
		{
			return _Stages;
		}

		void IStage.Enter()
		{
			_Binder.Bind<ILevelSelector>(this);
		}

		void IStage.Leave()
		{
			// ReSharper disable once ArrangeThisQualifier
			_Binder.Unbind<ILevelSelector>(this);
		}

		void IStage.Update()
		{
		}

		private bool _Check(int level)
		{
			return _Stages.Any(stage => stage == level);
		}
	}
}
