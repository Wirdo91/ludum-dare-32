using UnityEngine;
using System.Collections;

public class Fart : Weapon
{
    float spread = 1;

    public override void start()
    {
        spread = 1;
        base.start();
    }

    public override void Start()
    {
        spread = 1;
        base.Start();
    }

    public override void Shoot(Vector3 ppos, Quaternion angle, string tag)
    {
        if (attacktimer >= attackspeed)
        {
            for (int i = -(int)spread; i <= (int)spread; i++)
            {
                GameObject go = Instantiate(bullet);
                go.tag = tag;
                go.transform.position = ppos;
                go.transform.rotation = Quaternion.Euler(angle.x, angle.y, angle.eulerAngles.z + ((i * 5)));
                go.SetActive(true);
                attacktimer = 0;
            }
            if (soundEffects.Length > 0)
            {
                this.GetComponent<AudioSource>().enabled = true;
                this.GetComponent<AudioSource>().PlayOneShot(soundEffects[Random.Range(0, soundEffects.Length)]);
            }

        }
    }

    public override void Upgrade()
    {
        bullet.GetComponent<FartBullet>().speed += 0.0625f;
        shotspersec += 0.125f;
        attackspeed = (1.0f / shotspersec);
        spread += 0.5f;
    }
}