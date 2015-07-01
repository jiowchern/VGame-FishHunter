using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VGame.Project.FishHunter
{
    class WeaponChancesTable : Regulus.Game.ChancesTable<int>
    {
        public WeaponChancesTable(Data[] datas)
            : base(datas)
        {
        }
      
    }
}
