using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }
    Dictionary<string, Queue<GameObject>> poolsDictionary;
    Transform deactivatedObjectsParent;

    private void Awake()
    {
        Instance = this;
        deactivatedObjectsParent = transform;
        poolsDictionary = new Dictionary<string, Queue<GameObject>>();
    }

    public GameObject SpawnObject(GameObject prefab)
    {
        if (!poolsDictionary.ContainsKey(prefab.name))
        {
            poolsDictionary[prefab.name] = new Queue<GameObject>();
        }

        GameObject result;

        if(poolsDictionary[prefab.name].Count > 0)
        {
            result = poolsDictionary[prefab.name].Dequeue();
            result.SetActive(true);
            return result;
        }

        result = Instantiate(prefab);
        result.name = prefab.name;

        return result;
    }

    public void DespawnObject(GameObject target)
    {
        poolsDictionary[target.name].Enqueue(target);
        target.transform.SetParent(deactivatedObjectsParent,false);
        target.SetActive(false);
    }
}
