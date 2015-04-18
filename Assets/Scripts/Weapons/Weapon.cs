using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected GameObject bullet;

    void Start()
    {
        bullet.AddComponent<SpriteRenderer>();
        bullet.AddComponent<Bullet>();
        bullet.SetActive(false);
    }

    public virtual void Shoot(Vector3 ppos, Vector3 dir)
    {
        GameObject go = Instantiate(bullet);
        go.transform.position = ppos;
        go.transform.eulerAngles = dir;
    }
}
