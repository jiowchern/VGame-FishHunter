using System;

using UnityEngine;
using System.Collections;

using Regulus.Remoting;

using VGame.Project.FishHunter;

[RequireComponent(typeof(Animator))]
public class FortBehavior : MonoBehaviour
{
    

    private int _LastBullet;

    private Vector3 _LastDirection;

    public event Action<int,Vector3> FireEvent;
    public event Action IdleEvent;    
    public void Idle(VGame.Project.FishHunter.BULLET bullet)
    {
        var animator = this.GetComponent<Animator>();
        animator.SetInteger("Fort", (int)bullet);
        animator.SetTrigger("Idle");
    }


    void _Fire()
    {
        if (_LastBullet != 0)
            FireEvent(_LastBullet , _LastDirection);
        _LastBullet = 0;
    }


    void _Idle()
    {
        IdleEvent();
    }

    public void Fire(int bulletid, Vector3 dir)
    {
        var animator = this.GetComponent<Animator>();
        animator.SetTrigger("Fire");
        _LastBullet = bulletid;
        _LastDirection = dir;
    }
}
