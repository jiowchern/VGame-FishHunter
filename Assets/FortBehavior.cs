using System;
using System.Linq;

using UnityEngine;
using System.Collections;

using Regulus.Remoting;

using VGame.Project.FishHunter;
using VGame.Project.FishHunter.Common.Data;

[RequireComponent(typeof(Animator))]
public class FortBehavior : MonoBehaviour
{
    

    private int _LastBullet;

    private Vector3 _LastDirection;
    private Animator _Animator;

    public event Action<int,int,Vector3> FireEvent;
    public event Action IdleEvent;

    struct WeaponCondition
    {
        public int Id;
        public bool Lock;
        public VGame.Project.FishHunter.Common.Data.BULLET Type;
    }
    


    void Start()
    {
        _Animator = this.GetComponent<Animator>();

        
    }
    public void Idle(VGame.Project.FishHunter.Common.Data.BULLET bullet  , bool lock_mode,int odds)
    {
        
        var animator = _Animator;
        animator.SetInteger("Fort", (int)bullet);
        animator.SetInteger("Odds", odds);
        animator.SetBool("Lock", lock_mode);
        animator.SetTrigger("Idle");
    }

    


    void _Fire(int bullet_type)
    {
        if (_LastBullet != 0)
            FireEvent(_LastBullet, bullet_type, _LastDirection);
        _LastBullet = 0;
    }


    void _Idle()
    {
        IdleEvent();
    }

    public void Fire(int bulletid, Vector3 dir)
    {
        var animator = _Animator;
        animator.SetTrigger("Fire");
        _LastBullet = bulletid;
        _LastDirection = dir;
    }
}
