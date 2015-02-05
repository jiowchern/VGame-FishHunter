﻿namespace VGame.Project.FishHunter
{
    public class DummyStandalong : Regulus.Utility.ICore
    {

        VGame.Project.FishHunter.Center _Center;
        VGame.Project.FishHunter.DummyStorage _Storage;

        Regulus.Utility.ICore _Core { get { return _Center; } }
        Regulus.Utility.Updater _Updater;
        public DummyStandalong()
        {
            _Storage = new DummyStorage();
            _Updater = new Regulus.Utility.Updater();
            _Center = new Center(_Storage);
        }

        void Regulus.Utility.ICore.ObtainController(Regulus.Remoting.ISoulBinder binder)
        {
            _Core.ObtainController(binder);
        }

        bool Regulus.Utility.IUpdatable.Update()
        {
            _Updater.Update();
            return true;
        }

        void Regulus.Framework.ILaunched.Launch()
        {
            _Updater.Add(_Center);
        }

        void Regulus.Framework.ILaunched.Shutdown()
        {
            _Updater.Shutdown();
        }
    }

}
