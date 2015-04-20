using UnityEngine;
using System.Collections;

public class Fart : Weapon
{
    public override void Shoot(Vector3 ppos, Quaternion angle, string tag)
    {
        if (attacktimer >= attackspeed)
        {
            for (int i = -2; i <= 2; i++)
            {
                GameObject go = Instantiate(bullet);
                go.tag = tag;
                go.transform.position = ppos;
                go.transform.rotation = Quaternion.Euler(angle.x, angle.y, angle.eulerAngles.z + ((i * 10)));
                attacktimer = 0;
            }
            if (soundEffects.Length > 0)
            {
                this.GetComponent<AudioSource>().enabled = true;
                this.GetComponent<AudioSource>().PlayOneShot(soundEffects[Random.Range(0, soundEffects.Length)]);
            }

        }
    }
}
