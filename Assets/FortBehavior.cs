using UnityEngine;
using System.Collections;

using Regulus.Remoting;

using VGame.Project.FishHunter;

[RequireComponent(typeof(Animator))]
public class FortBehavior : MonoBehaviour
{

    private Regulus.Remoting.Value<VGame.Project.FishHunter.BULLET> _Launch;

    private VGame.Project.FishHunter.BULLET _Bullet;
    public void Idle(VGame.Project.FishHunter.BULLET bullet)
    {
        var animator = this.GetComponent<Animator>();
        animator.SetInteger("Fort", (int)bullet);
        animator.SetTrigger("Idle");
    }

    public Regulus.Remoting.Value<VGame.Project.FishHunter.BULLET> Fire(VGame.Project.FishHunter.BULLET bullet)
    {

        if (_Launch != null)
        {
            return null;
        }
        var animator = this.GetComponent<Animator>();
        animator.SetTrigger("Fire");
        _Launch = new Value<BULLET>();
        _Bullet = bullet;
        return _Launch;
    }


    void _Fire()
    {
        if (_Launch == null)
            return;
        _Launch.SetValue(_Bullet);
        _Launch = null;
    }
}
