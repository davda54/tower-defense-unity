using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
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
        public HashSet<GameObject> Active;
        public Queue<GameObject> Available;

        public SinglePool()
        {
            Available = new Queue<GameObject>();
        }
    }
    
    public int StartCapacity = 10;

    private Dictionary<string, GameObject> pooledObjects = new Dictionary<string, GameObject>();
    private Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();    

    public void RegisterObject(GameObject prototype)
    {
        if (pooledObjects.ContainsKey(prototype.tag)) return;
        
        var singlePool = new Queue<GameObject>();
        for (var i = 0; i < StartCapacity; i++)
        {
            var newItem = Instantiate(prototype, transform);
            newItem.SetActive(false);
            singlePool.Enqueue(newItem);
        }

        pooledObjects.Add(prototype.tag, prototype);
        pool.Add(prototype.tag, singlePool);
    }

	public GameObject ActivateObject(string tag)
    {
        if (!pooledObjects.ContainsKey(tag))
            throw new KeyNotFoundException();

        var singlePool = pool[tag];

        if (singlePool.Count == 0)
        {
            var newItem = Instantiate(pooledObjects[tag], transform);
            return newItem;
        }

        var item = singlePool.Dequeue();

        return item;
    }

    public void DeactivateObject(GameObject item)
    {
        if (!pooledObjects.ContainsKey(item.tag))
            throw new KeyNotFoundException();

        var singlePool = pool[item.tag];

        item.SetActive(false);
        singlePool.Enqueue(item);
    }
}
