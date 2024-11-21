using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanObjectPool : MonoBehaviour
{
    public GameObject ManPrefab;
    public GameObject WomanPrefab; // ������ ������
    private int poolSize = 20;        // Ǯ ũ��
    private List<GameObject> pool;   // ������Ʈ Ǯ ����Ʈ

    void Start()
    {
        if (ManPrefab == null || WomanPrefab == null) // prefab�� ������� �ʾ��� ��
        {
            Debug.Log("Prefab is not assigned in ObjectPool!"); //���� Ȯ�ο� ����� �޼���
            return;
        }

        // ������ Ǯ�����ŭ ����
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize / 2; i++)
        {
            GameObject obj = Instantiate(ManPrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
        for (int i = 0; i < poolSize / 2; i++)
        {
            GameObject obj = Instantiate(WomanPrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        if (pool == null || pool.Count == 0)
        {
            Debug.Log("Pool is not initialized or empty!"); //���� Ȯ�ο� ����� �޼���
            return null;
        }
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;  // ����� �� �ִ� ������Ʈ�� ������ null ��ȯ
    }
}
