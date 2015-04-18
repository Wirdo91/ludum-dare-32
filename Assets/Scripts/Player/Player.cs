using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject CrossHead;
    public int Health { get; set; }
    [SerializeField]
    public Weapon CurrentWeapon;
    public float MoveSpeed { get; set; }

	// Use this for initialization
	void Start ()
    {
        MoveSpeed = 5;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
        transform.Translate(dir.normalized * Time.deltaTime * MoveSpeed, Space.World);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouseWorldPos.y - this.transform.position.y, mouseWorldPos.x - this.transform.position.x) * Mathf.Rad2Deg);
        CrossHead.transform.position = (mouseWorldPos - (Vector2)transform.position).normalized * Mathf.Clamp(Vector2.Distance(mouseWorldPos, transform.position), 0.0f, 5.0f) + (Vector2)transform.position;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //TODO: Shoot fucking bullets!
            CurrentWeapon.Shoot(this.transform.position, this.transform.rotation);
        }
	}
}