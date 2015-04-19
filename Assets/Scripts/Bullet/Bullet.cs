using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Bullet : MonoBehaviour
{
    [SerializeField]
    protected Sprite[] bulletSprites;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float lifeSpan;
    protected float lifeTime;


    // Use this for initialization
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (lifeTime >= lifeSpan)
        {
            DestroyObject();
        }
        transform.Translate(Vector2.right * Time.deltaTime * speed, Space.Self);
        this.GetComponent<SpriteRenderer>().sprite = bulletSprites[Random.Range(0, bulletSprites.Length)];
        lifeTime += Time.deltaTime;
    }

    public virtual void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
