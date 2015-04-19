using UnityEngine;
using System.Collections;

public class VomitBullet : Bullet
{
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
            DestroyObject();
        }
        this.GetComponent<SpriteRenderer>().sprite = bulletSprites[Random.Range(0, bulletSprites.Length)];
        lifeTime += Time.deltaTime;
        this.transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x + (Time.deltaTime * 2.5f), 0.0f, 0.5f), 0.025f, 1);
        Transform t = GameObject.Find("Player").transform;
        this.transform.position = t.position;
        this.transform.rotation = t.rotation;
    }
}
