using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public int Health { get; set; }
    public Weapons Weapon { get; set; }
    public float MoveSpeed { get; set; }

	// Use this for initialization
	void Start ()
    {
        MoveSpeed = 5;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            dir += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir -= Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir -= Vector2.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir += Vector2.right;
        }
        transform.Translate(dir.normalized * Time.deltaTime * MoveSpeed);
	}
}

public enum Weapons
{
    Beam, Bullet, Rocket,
}