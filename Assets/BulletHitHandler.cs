using UnityEngine;
using System.Collections;

using VGame.Project.FishHunter;

public abstract class BulletHitHandler : MonoBehaviour 
{
    public abstract void Hit(int id, FishBounds[] hits);    
}
