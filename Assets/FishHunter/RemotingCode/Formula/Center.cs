﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VGame.Project.FishHunter.Formula
{
    public class Center : Regulus.Utility.ICore
    {
        
        Regulus.Utility.CenterOfUpdateable _Update;
        Hall _Hall;
        private StorageController _Controller;
        
        private Center()
        {
        
            _Hall = new Hall();
            _Update = new Regulus.Utility.CenterOfUpdateable();
        }

        public Center(StorageController controller) : this()
        {
            // TODO: Complete member initialization
            this._Controller = controller;
        }
        void Regulus.Utility.ICore.ObtainController(Regulus.Remoting.ISoulBinder binder)
        {            
            _Hall.PushUser(new User(binder, _Controller));
        }

        bool Regulus.Utility.IUpdatable.Update()
        {         
            _Update.Working();
            return true;
        }

        void Regulus.Framework.ILaunched.Launch()
        {
            _Update.Add(_Hall);
        }

        void Regulus.Framework.ILaunched.Shutdown()
        {
         
            _Update.Shutdown();
        }
    }
}
