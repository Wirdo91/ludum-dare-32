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

    [SerializeField]
    StashItem currentItem = null;

    [SerializeField]
    Weapon currentWeapons;

    [SerializeField]
    float moveSpeed = 3f;

    public void Initiate(SingleEnemy settings)
    {
        this.startPos = settings.StartPos;
        this.endPos = settings.EndPos;
        this.movementType = settings.Movement;
    }

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
        if (currentWeapons != null)
        {
            currentWeapons.update();
        }

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

        this.transform.Translate(curDir.normalized * moveSpeed * Time.deltaTime, Space.World);
        this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(curDir.y, curDir.x) * Mathf.Rad2Deg);

        if (currentItem != null)
        {
            currentItem.transform.position = this.transform.position + (Vector3)curDir * 0.2f;
        }

        if (currentWeapons != null)
        {
            currentWeapons.Shoot(this.transform.position, this.transform.rotation);
        }
    }

    void OnBecameInvisible()
    {
        Debug.Log("Out of bound");
        Destroy(currentItem.gameObject);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.GetComponent<PlayerStash>() != null)
        {
            currentItem = col.transform.GetComponent<PlayerStash>().TakeItem();
        }
        if (col.gameObject.GetComponent<Bullet>())
        {
            Kill();
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
