using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected GameObject bullet;

    [SerializeField]
    float shotspersec;

    float attackspeed;

    float attacktimer = 0;

    public virtual void start()
    {
        if (shotspersec == 0)
            attackspeed = 0;
        else
            attackspeed = (1.0f / (float)shotspersec);
    }

    public virtual void update()
    {
        //Debug.Log(attackspeed);
        attacktimer += Time.deltaTime;
    }

    public virtual void Shoot(Vector3 ppos, Quaternion angle, string tag)
    {
        if (attacktimer >= attackspeed)
        {
            GameObject go = Instantiate(bullet);
            go.tag = tag;
            go.transform.position = ppos;
            go.transform.rotation = angle;
            attacktimer = 0;
        }
    }
}
