using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStash : MonoBehaviour
{
    [SerializeField]
    public uint StashStartAmount = 50;
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
            GameObject item = (GameObject)Instantiate(stashItem, this.transform.position, Quaternion.identity);
            item.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
            item.transform.position = this.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            item.GetComponent<CircleCollider2D>().enabled = false;
            //item.SetActive(false);
            stash.Enqueue(item.GetComponent<StashItem>());
        }
    }

    public StashItem TakeItem()
    {
        if (stash.Count <= 0)
            return null;

        StashItem currentItem = stash.Dequeue();
        //currentItem.gameObject.SetActive(true);
        return currentItem;
    }

    public void ReturnItem(StashItem item)
    {
        item.transform.position = this.transform.position;
        item.GetComponent<CircleCollider2D>().enabled = false;
        //item.gameObject.SetActive(false);
        stash.Enqueue(item);
    }
}
