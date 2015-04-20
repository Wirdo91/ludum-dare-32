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
    [SerializeField]
    Sprite[] playerSprites;

    int index = 0;
    float timer;

    StashItem currentItem;

    // Use this for initialization
    void Start()
    {
        MoveSpeed = 5;
        Health = 3;
        CurrentWeapon = Instantiate(CurrentWeapon.gameObject).GetComponent<Weapon>();
        CurrentWeapon.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelController.GameStarted || LevelController.GameOver)
        {
            return;
        }

        CurrentWeapon.update();
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
            CurrentWeapon.Shoot(this.transform.position, this.transform.rotation, "Player");
        }
        if (currentItem != null && dir != Vector2.zero)
        {
            currentItem.transform.position = this.transform.position + (Vector3)dir * 0.2f;
        }
        timer += Time.deltaTime;
        if (dir != Vector2.zero && (float)(1.0f / 15.0f) <= timer)
        {
            index++;
            if (index == playerSprites.Length)
            {
                index = 0;
            }
            this.GetComponent<SpriteRenderer>().sprite = playerSprites[index];
            timer = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Bullet>() != null && col.tag == "Enemy")
        {
            Debug.Log("Player hit");
            Health--;
        }
        if (currentItem == null && col.GetComponent<StashItem>() != null)
        {
            currentItem = col.GetComponent<StashItem>();
        }
        if (currentItem != null && col.GetComponent<PlayerStash>() != null)
        {
            col.GetComponent<PlayerStash>().ReturnItem(currentItem);
            currentItem = null;
        }

        if (col.GetComponent<Weapon>() != null)
        {
            Destroy(CurrentWeapon.gameObject);

            CurrentWeapon = col.GetComponent<Weapon>();
            CurrentWeapon.GetComponent<Collider2D>().enabled = false;
            CurrentWeapon.GetComponent<SpriteRenderer>().enabled = false;
            CurrentWeapon.start();
        }
    }
}