using System;
using System.Collections.Generic;

using Regulus.Remoting;

using VGame.Project.FishHunter;

public class Magazine 
{
    private readonly IPlayer _Player;


    private readonly Queue<int> _Bullets;

    private int _Sn;

    private bool _Wait;

    public Magazine(IPlayer player)
    {
        if (player == null)
        {
            throw new ArgumentNullException("player");
        }
        _Player = player;        

        _Bullets = new Queue<int>();
    }

    public Magazine()
    {        
        _Bullets = new Queue<int>();
    }

    public void Reload()
    {

        if (Check() == false && _Wait == false)
        {
            if (_Player != null)
            {
                _Wait = true;
                _Player.RequestBullet().OnValue += _Load;
            }
            else
            {
                _Bullets.Enqueue(++_Sn);
            }    
        }
        
    }

    private void _Load(int id)
    {
        _Wait = false;
        _Bullets.Enqueue(id);
    }

    public bool Check()
    {
        return _Bullets.Count > 0;
    }

    public int Pop()
    {
        return _Bullets.Dequeue();
    }

    public bool TryGet(out int bulletid)
    {
        bulletid = 0;
        if (Check())
        {
            bulletid = Pop();            
        }        
        return bulletid != 0;

    }
}