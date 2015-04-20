using UnityEngine;
using System.Collections;

public class Burp : Weapon
{
    public override void Upgrade()
    {
        bullet.GetComponent<BurpBullet>().speed += 0.125f;
        bullet.GetComponent<BurpBullet>().lifeSpan += 0.125f;
        shotspersec += 0.0625f;
        attackspeed = (1.0f / shotspersec);
    }
}
