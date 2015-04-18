using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject CrossHead;
    public int Health { get; set; }
    public Weapon CurrentWeapon { get; set; }
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

        CrossHead.transform.position = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * Mathf.Clamp(Vector3.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position), 0.0f, 5.0f) + transform.position;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //TODO: Shoot fucking bullets!
            CurrentWeapon.Shoot(this.transform.position, this.transform.eulerAngles);
        }
	}
}