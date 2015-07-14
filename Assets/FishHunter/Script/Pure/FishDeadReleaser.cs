using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.FishHunter.Script.Pure
{
    class FishDeadReleaser : Regulus.Utility.IUpdatable
    {
        private FishCollider fish;


        bool _Dead;
        public FishDeadReleaser(FishCollider fish)
        {
            // TODO: Complete member initialization
            this.fish = fish;
            fish.DeadEvent += fish_DeadEvent;
        }

        void fish_DeadEvent()
        {
            _Dead = true;
        }

        bool Regulus.Utility.IUpdatable.Update()
        {
            return _Dead == false;
        }

        void Regulus.Framework.IBootable.Launch()
        {
            
        }

        void Regulus.Framework.IBootable.Shutdown()
        {
            fish.DeadEvent -= fish_DeadEvent;
        }
    }
}
