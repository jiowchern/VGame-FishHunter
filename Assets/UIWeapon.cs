using System;

using UnityEngine;
using System.Collections;

using Regulus.Remoting.Ghost;

using VGame.Project.FishHunter.Common.GPI;
using VGame.Project.FishHunter.Common.Data;

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
						 1,2,3,4,5,6,7,8,9,10,20,30,40,60,70,80,90,100						 
					 };


		_Powers = new[]
					  {
						  VGame.Project.FishHunter.Common.Data.WEAPON_TYPE.NORMAL,                          
						  VGame.Project.FishHunter.Common.Data.WEAPON_TYPE.FREE_POWER,                          
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

			WeaponPower.text = string.Format("{0}", _Player.WeaponType.ToString());
		}
	}

	private int _OddsIndex;
	public void NextOdds()
	{
		if (this._Player == null)
		{
			return;
		}

		this._Player.EquipWeapon(this._Player.WeaponType, this._NextOdds());
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


    private WEAPON_TYPE[] _Powers;
	private int _PowerIndex;
    private WEAPON_TYPE _NextWeapon()
	{
		_PowerIndex ++;
		_PowerIndex %= _Powers.Length;

		return _Powers[_PowerIndex];
	}

    private WEAPON_TYPE _PrevWeapon()
	{
		_PowerIndex--;
		_PowerIndex = (_PowerIndex + _Powers.Length) % _Powers.Length;

		return _Powers[_PowerIndex];
	}
}
