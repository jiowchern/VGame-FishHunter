﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    class VerifyStage : Regulus.Utility.IStage
    {
        private string _Account;
        private string _Password;
        private Regulus.Remoting.Ghost.IProviderNotice<VGame.Project.FishHunter.IVerify> _Provider;

        public delegate void DoneCallback();
        public event DoneCallback SuccessEvent;
        public event DoneCallback FailEvent;

        public VerifyStage(string _Account, string _Password, Regulus.Remoting.Ghost.IProviderNotice<VGame.Project.FishHunter.IVerify> providerNotice)
        {
            // TODO: Complete member initialization
            this._Account = _Account;
            this._Password = _Password;
            this._Provider = providerNotice;
        }
        void Regulus.Utility.IStage.Enter()
        {
            _Provider.Supply += _Provider_Supply;
        }

        void _Provider_Supply(VGame.Project.FishHunter.IVerify obj)
        {
            obj.Login(_Account, _Password).OnValue += _Result ;
        }

        private void _Result(bool obj)
        {
            if (obj)
                SuccessEvent();
            else
                FailEvent();
        }

        void Regulus.Utility.IStage.Leave()
        {
            
        }

        void Regulus.Utility.IStage.Update()
        {
            
        }
    }
}
