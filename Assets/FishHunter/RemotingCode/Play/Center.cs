﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Center.cs" company="Regulus Framework">
//   Regulus Framework
// </copyright>
// <summary>
//   Defines the Center type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region Test_Region

using Regulus.Framework;
using Regulus.Remoting;
using Regulus.Utility;

using VGame.Project.FishHunter.Common;
using VGame.Project.FishHunter.Common.GPI;

#endregion

namespace VGame.Project.FishHunter.Play
{
	public class Center : ICore
	{
		private readonly IAccountFinder _AccountFinder;

		private readonly IFishStageQueryer _FishStageQueryer;

		private readonly Hall _Hall;

		private readonly IRecordQueriers _RecordQueriers;

		private readonly ITradeNotes _Tradefinder;

		private readonly Updater _Updater;

	    public Center(IAccountFinder account_finder, IFishStageQueryer fish_stage_queryer, IRecordQueriers rq,
	        ITradeNotes trade_account)
	    {
	        _RecordQueriers = rq;
	        _AccountFinder = account_finder;
	        _FishStageQueryer = fish_stage_queryer;
	        _Tradefinder = trade_account;

	        _Updater = new Updater();
	        _Hall = new Hall();
	    }

		void ICore.AssignBinder(ISoulBinder binder)
		{
			var user = new User(binder, 
				_AccountFinder, 
				_FishStageQueryer, 
				_RecordQueriers, 
				_Tradefinder);

			_Hall.PushUser(user);
		}

		bool IUpdatable.Update()
		{
			_Updater.Working();
			return true;
		}

		void IBootable.Launch()
		{
			_Updater.Add(_Hall);
		}

		void IBootable.Shutdown()
		{
			_Updater.Shutdown();
		}
	}
}