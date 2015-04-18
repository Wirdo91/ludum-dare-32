using UnityEngine;
using System.Collections;

[RequireComponent(SpriteRenderer)]
public abstract class Bullet : MonoBehaviour
{
    [SerializeField]
    Sprite[] bulletSprites;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up);
        this.GetComponent<SpriteRenderer>().sprite = bulletSprites[Random.Range(0, bulletSprites)];
    }
}
