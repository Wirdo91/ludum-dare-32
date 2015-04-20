using UnityEngine;
using System.Collections;

public class Vomit : Weapon
{
    public override void Upgrade()
    {
        bullet.GetComponent<VomitBullet>().maxlength += 0.05f;
        bullet.GetComponent<VomitBullet>().width += 0.001f;
        bullet.GetComponent<VomitBullet>().speed += 0.25f;
        shotspersec += 0.025f;
        attackspeed = (1.0f / shotspersec);
    }
}
