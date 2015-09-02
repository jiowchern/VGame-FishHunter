﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpansionFeature.cs" company="Regulus Framework">
//   Regulus Framework
// </copyright>
// <summary>
//   Defines the ExpansionFeature type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using VGame.Project.FishHunter.Common.GPI;


namespace VGame.Project.FishHunter.Formula
{
	public struct ExpansionFeature
	{
		public IFormulaFarmRecorder FormulaFarmRecorder { get; private set; }

		public IAccountFinder AccountFinder { get; private set; }

		public IFormulaPlayerRecorder FormulaPlayerRecorder { get; private set; }

		public ExpansionFeature(
			IAccountFinder account_finder, 
			IFormulaFarmRecorder formula_stage_data_recorder, 
			IFormulaPlayerRecorder formula_player_recorder) : this()
		{
            AccountFinder = account_finder;
			FormulaFarmRecorder = formula_stage_data_recorder;
			FormulaPlayerRecorder = formula_player_recorder;
		}
	}
}
