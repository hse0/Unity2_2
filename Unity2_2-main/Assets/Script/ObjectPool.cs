using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;        
    private int poolSize = 10;       
    private List<GameObject> pool;   

    void Start()
    {
        if (prefab == null) 
        {
            Debug.Log("Prefab is not assigned in ObjectPool!"); 
            return;
        }

        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetObjectFromPool() //
    {
        if (pool == null || pool.Count == 0)
        {
            Debug.Log("Pool is not initialized or empty!");
            return null;
        }
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }
}
