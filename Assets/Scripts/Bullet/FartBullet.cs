﻿using UnityEngine;
using System.Collections;

public class FartBullet : Bullet
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
        transform.Translate(-Vector2.right * Time.deltaTime * speed, Space.Self);
        this.GetComponent<SpriteRenderer>().sprite = bulletSprites[Random.Range(0, bulletSprites.Length)];
        lifeTime += Time.deltaTime;
    }
}
