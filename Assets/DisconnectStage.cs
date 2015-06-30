using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    class DisconnectStage  :Regulus.Utility.IStage
    {
        
        public Action DoneEvent;
        private Regulus.Remoting.Ghost.INotifier<Regulus.Utility.IOnline> notifier;

        public DisconnectStage(Regulus.Remoting.Ghost.INotifier<Regulus.Utility.IOnline> notifier)
        {
            // TODO: Complete member initialization
            this.notifier = notifier;
        }
        

        void Regulus.Utility.IStage.Enter()
        {
            if (notifier.Ghosts.Length == 0)
                DoneEvent();

            notifier.Unsupply += notifier_Unsupply;
            notifier.Supply += notifier_Supply;
            
        }

        void notifier_Unsupply(Regulus.Utility.IOnline obj)
        {
            DoneEvent();
        }

        void notifier_Supply(Regulus.Utility.IOnline obj)
        {
            obj.Disconnect();
        }

        void Regulus.Utility.IStage.Leave()
        {
            notifier.Unsupply -= notifier_Unsupply;
            notifier.Supply -= notifier_Supply;
        }

        void Regulus.Utility.IStage.Update()
        {
            
        }
    }
}
