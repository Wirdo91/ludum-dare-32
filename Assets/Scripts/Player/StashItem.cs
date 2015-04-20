using UnityEngine;
using System.Collections;

public class StashItem : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;

    // Use this for initialization
    void Start()
    {
        if (sprites.Length > 0)
            this.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
