﻿
using System.Collections.Generic;
using System.Linq;


using Regulus.Utility;


using VGame.Project.FishHunter.Common.Data;

namespace VGame.Project.FishHunter.Formula.ZsFormula.Data
{
	public class FishTreasure
	{
		public FISH_TYPE FishType { get; set; }

		public FISH_STATUS FishStatus { get; set; }

		/// <summary>
		///     隨機武器
		/// </summary>
		public WEAPON_TYPE[] RandomWeapons { get; private set; }

		/// <summary>
		///     必出武器
		/// </summary>
		public WEAPON_TYPE[] CertainWeapons { get; private set; }

		public static List<FishTreasure> Get()
		{
			var treasures = new List<FishTreasure>();

			treasures =
			   EnumHelper.GetEnums<FISH_TYPE>()
						 .Where(x => x >= FISH_TYPE.TROPICAL_FISH && x <= FISH_TYPE.SPECIAL_EAT_FISH_CRAZY)
						 .Select(
							 i => new FishTreasure
							 {
								 FishType = i,
								 RandomWeapons = new[]
								 {
									  WEAPON_TYPE.SUPER_BOMB,
									  WEAPON_TYPE.ELECTRIC_NET,
									  WEAPON_TYPE.FREE_POWER
								 },
								 CertainWeapons = new[]
								 {
									  WEAPON_TYPE.INVALID,
									  WEAPON_TYPE.KING
								 }
							 }).ToList();


			treasures.Add(
				new FishTreasure
				{
					FishType = FISH_TYPE.SPECIAL_FREEZE_BOMB,
					RandomWeapons = new[]
					{
						WEAPON_TYPE.SUPER_BOMB,
						WEAPON_TYPE.ELECTRIC_NET,
						WEAPON_TYPE.FREE_POWER
					},
					CertainWeapons = new[]
					{
						WEAPON_TYPE.FREEZE_BOMB,
					}
				});

			treasures.Add(
				new FishTreasure
				{
					FishType = FISH_TYPE.SPECIAL_SCREEN_BOMB,
					RandomWeapons = new[]
					{
						WEAPON_TYPE.SUPER_BOMB,
						WEAPON_TYPE.ELECTRIC_NET,
						WEAPON_TYPE.FREE_POWER
					},
					CertainWeapons = new[]
					{
						WEAPON_TYPE.SCREEN_BOMB
					}
				});

			treasures.Add(
				new FishTreasure
				{
					FishType = FISH_TYPE.SPECIAL_THUNDER_BOMB,
					RandomWeapons = new[]
					{
						WEAPON_TYPE.SUPER_BOMB,
						WEAPON_TYPE.ELECTRIC_NET,
						WEAPON_TYPE.FREE_POWER
					},
					CertainWeapons = new[]
					{
						WEAPON_TYPE.THUNDER_BOMB
					}
				});

			treasures.Add(
				new FishTreasure
				{
					FishType = FISH_TYPE.SPECIAL_FIRE_BOMB,
					RandomWeapons = new[]
					{
						WEAPON_TYPE.SUPER_BOMB,
						WEAPON_TYPE.ELECTRIC_NET,
						WEAPON_TYPE.FREE_POWER
					},
					CertainWeapons = new[]
					{
						WEAPON_TYPE.FIRE_BOMB
					}
				});

			treasures.Add(
				new FishTreasure
				{
					FishType = FISH_TYPE.SPECIAL_DAMAGE_BALL,
					RandomWeapons = new[]
					{
						WEAPON_TYPE.SUPER_BOMB,
						WEAPON_TYPE.ELECTRIC_NET,
						WEAPON_TYPE.FREE_POWER
					},
					CertainWeapons = new[]
					{
						WEAPON_TYPE.DAMAGE_BALL
					}
				});

			treasures.Add(
				new FishTreasure
				{
					FishType = FISH_TYPE.SPECIAL_OCTOPUS_BOMB,
					RandomWeapons = new[]
					{
						WEAPON_TYPE.SUPER_BOMB,
						WEAPON_TYPE.ELECTRIC_NET,
						WEAPON_TYPE.FREE_POWER
					},
					CertainWeapons = new[]
					{
						WEAPON_TYPE.OCTOPUS_BOMB
					}
				});

			treasures.Add(
				new FishTreasure
				{
					FishType = FISH_TYPE.SPECIAL_BIG_OCTOPUS_BOMB,
					RandomWeapons = new[]
					{
						WEAPON_TYPE.SUPER_BOMB,
						WEAPON_TYPE.ELECTRIC_NET,
						WEAPON_TYPE.FREE_POWER
					},
					CertainWeapons = new[]
					{
						WEAPON_TYPE.BIG_OCTOPUS_BOMB
					}
				});
			return treasures;
		}
		public FishTreasure()
		{
		   
		}
	}
}