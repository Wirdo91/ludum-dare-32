using UnityEngine;
using System.Collections;

public class VomitBullet : Bullet
{
    public float maxlength = 0.5f;
    public float width = 0.025f;

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
        this.transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x + (Time.deltaTime * speed), 0.0f, maxlength), width, 1);
        Transform t = GameObject.Find("Player").transform;
        this.transform.position = t.position;
        this.transform.rotation = t.rotation;
    }
}
