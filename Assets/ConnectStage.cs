using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class ConnectStage : Regulus.Utility.IStage
    {
        private string _Ip;
        private int _Port;
        private Regulus.Remoting.Ghost.INotifier<Regulus.Utility.IConnect> _Provider;

        public delegate void DoneCallback();
        public event DoneCallback SuccessEvent;
        public event DoneCallback FailEvent;

        public ConnectStage(string _Ip, int _Port, Regulus.Remoting.Ghost.INotifier<Regulus.Utility.IConnect> providerNotice)
        {
            
            this._Ip = _Ip;
            this._Port = _Port;
            this._Provider = providerNotice;
        }
        void Regulus.Utility.IStage.Leave()
        {
            _Provider.Supply -= _Connect;
        }
        void Regulus.Utility.IStage.Enter()
        {
            _Provider.Supply += _Connect;            
        }

        private void _Connect(Regulus.Utility.IConnect obj)
        {
            _Provider.Supply -= _Connect;
            obj.Connect(_Ip, _Port).OnValue += _Result;
        }

        private void _Result(bool success)
        {

            if (success)
                SuccessEvent();
            else
            {
                FailEvent();
            }
                

            
        }

        

        void Regulus.Utility.IStage.Update()
        {
            
        }
    }
}
