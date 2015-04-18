using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStash : MonoBehaviour
{

    [SerializeField]
    public readonly uint StashStartAmount = 10;

    Queue<StashItem> stash;

    // Use this for initialization
    void Start()
    {
        stash = new Queue<StashItem>();
        for (int i = 0; i < StashStartAmount; i++)
        {
            stash.Enqueue(new StashItem());
        }
    }

    public StashItem TakeItem()
    {
        return stash.Dequeue();
    }

    public void ReturnItem(StashItem item)
    {
        stash.Enqueue(item);
    }
}
