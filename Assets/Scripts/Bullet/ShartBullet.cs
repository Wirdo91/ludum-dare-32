using UnityEngine;
using System.Collections;

public class ShartBullet : Bullet
{
    float ExplosionTime = 0.5f;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (lifeTime >= lifeSpan)
        {
            if (lifeTime >= lifeSpan + ExplosionTime)
            {
                DestroyObject();
                return;
            }
            this.GetComponent<CircleCollider2D>().radius = 2;
            this.GetComponent<SpriteRenderer>().sprite = bulletSprites[Random.Range(1, bulletSprites.Length)];
            this.GetComponent<SpriteRenderer>().color = new Color32(180, 60, 10, 255);
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.Translate(-Vector2.right * Time.deltaTime * speed, Space.Self);
            this.GetComponent<SpriteRenderer>().sprite = bulletSprites[0];
            this.GetComponent<SpriteRenderer>().color = new Color32(60, 30, 0, 255);
        }
        lifeTime += Time.deltaTime;
    }
}
