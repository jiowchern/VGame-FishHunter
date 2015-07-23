using System;

using UnityEngine;
using System.Collections;

using Regulus.Remoting.Ghost;

using VGame.Project.FishHunter;

using Debug = System.Diagnostics.Debug;

/// <summary>
/// The ui weapon.
/// </summary>
public class UIWeapon : MonoBehaviour 
{
    private Client _Clinet;

    private IPlayer _Player;

    public UILabel WeaponOdds;

    public UILabel WeaponPower;

    public UIWeapon()
    {
        _Oddss = new[]
                     {
                         10,
                         20,
                         30,
                         40
                     };


        _Powers = new[]
                      {
                          VGame.Project.FishHunter.BULLET.WEAPON1,
                          VGame.Project.FishHunter.BULLET.WEAPON2,
                          VGame.Project.FishHunter.BULLET.WEAPON3,
                          VGame.Project.FishHunter.BULLET.WEAPON4,
                           VGame.Project.FishHunter.BULLET.WEAPON5,
                          VGame.Project.FishHunter.BULLET.WEAPON6,
                          VGame.Project.FishHunter.BULLET.WEAPON7,
                          VGame.Project.FishHunter.BULLET.WEAPON8
                      };
        ;
    }

    void OnDestroy()
    {
        if (_Clinet != null)
        {
            _Clinet.User.PlayerProvider.Supply -= _PlayerProviderOnSupply;            
        }
    }
    // Use this for initialization
	void Start ()
	{
	    _Clinet = Client.Instance;


	    if (_Clinet != null)
	    {
	        _Clinet.User.PlayerProvider.Supply += _PlayerProviderOnSupply;
	    }
	}

    private void _PlayerProviderOnSupply(IPlayer player)
    {
        _Player = player;


    }

    // Update is called once per frame
	void Update () 
    {
	    if (_Player != null)
	    {
	        WeaponOdds.text = string.Format("x{0}", _Player.WeaponOdds);

	        WeaponPower.text = string.Format("{0}", _Player.Bullet.ToString());
	    }
	}

    private int _OddsIndex;
    public void NextOdds()
    {
        if (this._Player == null)
        {
            return;
        }

        this._Player.EquipWeapon(this._Player.Bullet, this._NextOdds());
    }
    
    private readonly int[] _Oddss;
    private int _NextOdds()
    {
        _OddsIndex ++;
        _OddsIndex %= _Oddss.Length;

        return _Oddss[_OddsIndex];
    }

    public void PrevWeapon()
    {
        if (this._Player == null)
        {
            return;
        }
        this._Player.EquipWeapon( _PrevWeapon() , _Player.WeaponOdds);        
    }

    public void NextWeapon()
    {
        if (this._Player == null)
        {
            return;
        }
        this._Player.EquipWeapon(_NextWeapon(), _Player.WeaponOdds);
    }


    private BULLET[] _Powers;
    private int _PowerIndex;
    private BULLET _NextWeapon()
    {
        _PowerIndex ++;
        _PowerIndex %= _Powers.Length;

        return _Powers[_PowerIndex];
    }

    private BULLET _PrevWeapon()
    {
        _PowerIndex--;
        _PowerIndex = (_PowerIndex + _Powers.Length) % _Powers.Length;

        return _Powers[_PowerIndex];
    }
}
