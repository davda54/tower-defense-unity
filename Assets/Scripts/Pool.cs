using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour {

    private static Pool poolInstance;
    public static Pool Instance
    {
        get
        {
            if (poolInstance == null)
                poolInstance = FindObjectOfType<Pool>();

            return poolInstance;
        }
    }

    private class SinglePool
    {
        public LinkedList<GameObject> Active;
        public Queue<GameObject> Available;

        public SinglePool()
        {
            Active = new LinkedList<GameObject>();
            Available = new Queue<GameObject>();
        }
    }
    
    public int StartCapacity = 10;

    private Dictionary<string, GameObject> pooledObjects = new Dictionary<string, GameObject>();
    private Dictionary<string, SinglePool> pool = new Dictionary<string, SinglePool>();    

    public void RegisterObject(GameObject prototype)
    {
        if (pooledObjects.ContainsKey(prototype.tag)) return;
        
        var singlePool = new SinglePool();
        for (var i = 0; i < StartCapacity; i++)
        {
            var newItem = Instantiate(prototype, transform);
            newItem.SetActive(false);
            singlePool.Available.Enqueue(newItem);
        }

        pooledObjects.Add(prototype.tag, prototype);
        pool.Add(prototype.tag, singlePool);
    }

	public GameObject ActivateObject(string tag)
    {
        if (!pooledObjects.ContainsKey(tag))
            throw new KeyNotFoundException();

        var singlePool = pool[tag];

        if (singlePool.Available.Count == 0)
        {
            var newItem = Instantiate(pooledObjects[tag], transform);
            singlePool.Active.AddLast(newItem);
            return newItem;
        }

        var item = singlePool.Available.Dequeue();
        singlePool.Active.AddLast(item);

        return item;
    }

    public void DeactivateObject(GameObject item)
    {
        if (!pooledObjects.ContainsKey(item.tag))
            throw new KeyNotFoundException();

        var singlePool = pool[item.tag];

        item.SetActive(false);
        singlePool.Active.Remove(item);
        singlePool.Available.Enqueue(item);
    }
}
