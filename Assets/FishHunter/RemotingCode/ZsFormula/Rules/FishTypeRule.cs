// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FishTypeRule.cs" company="Regulus Framework">
//   Regulus Framework
// </copyright>
// <summary>
//   Defines the FishTypeRule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using VGame.Project.FishHunter.ZsFormula.DataStructs;

namespace VGame.Project.FishHunter.ZsFormula.Rules
{
	/// <summary>
	/// �O�U�S������X��
	/// </summary>
	public class FishTypeRule
	{
		private readonly StageDataVisit _StageDataVisit;

		public FishTypeRule(StageDataVisit stage_data_visit)
		{
			_StageDataVisit = stage_data_visit;
		}

		public void Run(AttackData attack_data, Player.Data player_data)
		{
			if (attack_data.FishData.FishType < FishDataTable.Data.FISH_TYPE.DEF_100)
			{
				return;
			}

			// �S�ιL���N�i�H
			// �s���a��
			var data = player_data.RecodeData.SpecialWeaponDatas.Find(x => x.IsUsed == false);
			data.SpId = (int)attack_data.FishData.FishType;
			data.WinFrequency++;

			// �sstage��
			var stageData = _StageDataVisit.NowUseData.RecodeData.SpecialWeaponDatas.Find(x => x.IsUsed == false);
			stageData.SpId = (int)attack_data.FishData.FishType;
			stageData.WinFrequency++;
		}
	}
}