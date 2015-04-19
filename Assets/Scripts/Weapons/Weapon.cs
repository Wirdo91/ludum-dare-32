using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected GameObject bullet;

    [SerializeField]
    float shotspersec;

    [SerializeField]
    float despawnDelay = 3f;

    float attackspeed;

    float attacktimer = 0;

    [SerializeField]
    AudioClip[] soundEffects;

    SpriteRenderer render;

    public virtual void start()
    {
        if (shotspersec == 0)
            attackspeed = 0;
        else
            attackspeed = (1.0f / shotspersec);
    }

    void Start()
    {
        render = this.GetComponent<SpriteRenderer>();

        if (shotspersec == 0)
            attackspeed = 0;
        else
            attackspeed = (1.0f / shotspersec);
    }

    public virtual void update()
    {

        //Debug.Log(attackspeed);
        attacktimer += Time.deltaTime;
    }

    void Update()
    {
        if (render != null && render.enabled)
        {
            despawnDelay -= Time.deltaTime;
            if (despawnDelay <= 0)
            {
                Destroy(this.gameObject);
            }
        }
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

            if (soundEffects.Length > 0)
            {
                this.GetComponent<AudioSource>().enabled = true;
                this.GetComponent<AudioSource>().PlayOneShot(soundEffects[Random.Range(0, soundEffects.Length)]);
            }
        }
    }
}
