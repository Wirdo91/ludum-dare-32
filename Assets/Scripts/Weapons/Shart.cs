using UnityEngine;
using System.Collections;

public class Shart : Weapon
{
    public override void Upgrade()
    {
        bullet.GetComponent<ShartBullet>().speed += 0.0625f;
        shotspersec += 0.0625f;
        attackspeed = (1.0f / shotspersec);
        bullet.GetComponent<ShartBullet>().ExplosionTime += 0.0625f;
        bullet.GetComponent<ShartBullet>().Explosionradius += 0.0625f;
    }
}
