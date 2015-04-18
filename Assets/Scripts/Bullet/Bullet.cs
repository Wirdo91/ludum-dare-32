using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Bullet : MonoBehaviour
{
    [SerializeField]
    protected Sprite[] bulletSprites;

    // Use this for initialization
    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime, Space.Self);
        this.GetComponent<SpriteRenderer>().sprite = bulletSprites[Random.Range(0, bulletSprites.Length)];
    }
}
