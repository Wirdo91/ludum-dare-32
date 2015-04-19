using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStash : MonoBehaviour
{
    [SerializeField]
    public readonly uint StashStartAmount = 10;
    [SerializeField]
    GameObject stashItem;

    public int Count
    {
        get { return stash.Count; }
    }

    Queue<StashItem> stash;

    // Use this for initialization
    void Start()
    {
        stash = new Queue<StashItem>();
        for (int i = 0; i < StashStartAmount; i++)
        {
            GameObject item = Instantiate(stashItem);
            item.transform.position = Vector3.up * 100;
            stash.Enqueue(item.GetComponent<StashItem>());
        }
    }

    public StashItem TakeItem()
    {
        return stash.Dequeue();
    }

    public void ReturnItem(StashItem item)
    {
        item.transform.position = Vector3.up * 100;
        stash.Enqueue(item);
    }
}
