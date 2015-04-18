﻿using UnityEngine;
using System.Collections;

public enum EnemyMovement
{
    Line,
    Curve,
    Random
}

public class Enemy : MonoBehaviour
{
    [SerializeField]
    EnemyMovement movementType = EnemyMovement.Curve;

    [SerializeField]
    private Vector2 startPos = Vector2.zero, endPos = Vector2.zero;
    private Vector2 curDir = Vector2.zero;

    StashItem currentItem = null;

    [SerializeField]
    float moveSpeed = 3f;

    // Use this for initialization
    void Start()
    {
        this.transform.position = startPos;

        curDir = (endPos - startPos).normalized;

        if (movementType == EnemyMovement.Curve)
        {
            curDir = curDir - (Vector2)(Quaternion.Euler(0, 0, 90) * curDir);

            curDir.Normalize();
        }
    }


    float timer = 0.0f;
    bool moveRight = true;
    // Update is called once per frame
    void Update()
    {
        if (movementType == EnemyMovement.Curve)
        {
            if (timer > 1f)
            {
                timer = 0.0f;
                moveRight = !moveRight;
            }
            timer += Time.deltaTime;

            curDir = Quaternion.Euler(0, 0, (-1 + (System.Convert.ToInt32(moveRight) * 2)) * 90 * Time.deltaTime) * curDir;
        }
        else if (movementType == EnemyMovement.Random)
        {
            curDir = Quaternion.Euler(0, 0, Random.Range(-10f, 10f)) * curDir;
        }

        this.transform.Translate(curDir.normalized * moveSpeed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Debug.Log("Out of bound");
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.GetComponent<PlayerStash>() != null)
        {
            currentItem = col.transform.GetComponent<PlayerStash>().TakeItem();
            currentItem.transform.position = this.transform.position + (Vector3)curDir;
        }
    }

    public void Kill()
    {
        if (currentItem != null)
        {
            currentItem.transform.position = this.transform.position;
        }

        Destroy(this.gameObject);
    }
}
