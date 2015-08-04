// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpecialItemRule.cs" company="Regulus Framework">
//   Regulus Framework
// </copyright>
// <summary>
//   ���o�D��
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

using VGame.Project.FishHunter.ZsFormula.DataStructs;

namespace VGame.Project.FishHunter.ZsFormula.Rules
{
	/// <summary>
	///     ���o�D��
	/// </summary>
	public class SpecialItemRule
	{
		private readonly AttackData _AttackData;

		private readonly StageDataVisit _StageDataVisit;

		public Func<int> WeaponIdFunc { get; set; }

		public SpecialItemRule(StageDataVisit stage_data_visit)
		{
			_StageDataVisit = stage_data_visit;
		}

		public void Run(Player.Data player_data)
		{
			if (player_data.NowSpecialWeaponData.IsUsed)
			{
				return;
			}

			var playerWeaponData = player_data.RecodeData.SpecialWeaponDatas.Find(x => x.SpId == player_data.NowSpecialWeaponData.SpId);
			playerWeaponData.WinFrequency++;

			var stageWeaponData  = _StageDataVisit.NowUseData.RecodeData.SpecialWeaponDatas.Find(x => x.SpId == player_data.NowSpecialWeaponData.SpId);
			stageWeaponData.WinFrequency++;

			player_data.NowSpecialWeaponData.IsUsed = false;

			this.WeaponIdFunc = () => player_data.NowSpecialWeaponData.SpId;

		}
	}
}