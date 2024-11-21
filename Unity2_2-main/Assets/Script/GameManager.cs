using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ObjectPool humanPool;
    public ObjectPool bananaPool;
    public GameObject zooKeeperPrefab;
    public GameObject quokka;
    public float spawnInterval = 2f;
    public float zooKeeperSpawnDelay = 10f;
    public float zooKeeperSpawnInterval = 5f;

    private float HumanBananaPosMin = -50.0f; // 쿼카의 현재 좌표 기준, 최소 x,y 생성 좌표
    private float HumanBananaPosMax = 50.0f; // 쿼카의 현재 좌표 기준, 최대 x,y 생성 좌표

    private float HBExceptPosMin = -20.0f; // 쿼카로부터-20 ~ 0 사이 좌표에서 떨어져서 생성되도록 하기 위해 예외처리
    private float HBExceptPosMax = 20.0f; // 쿼카로부터 0 ~ 20 사이 좌표에서 떨어져서 생성되도록 하기 위해 예외처리
    private float ZooKeeperPosMin = -100.0f; // 쿼카로부터 -100 ~ 0 사이 좌표에서 떨어져서 생성되도록 하기 위해 예외처리
    private float ZooKeeperPosMax = 100.0f; // 쿼카로부터 0 ~ 100 사이 좌표에서 떨어져서 생성되도록 하기 위해 예외처리

    private float GlobalPosXMin = -550.0f; // 맵 전역에 적용되는 예외처리들
    private float GlobalPosXMax = 550.0f;
    private float GlobalPosYMin = -460.0f;
    private float GlobalPosYMax = 460.0f;

    void Start()
    {
        StartCoroutine(ActivateHumans());
        StartCoroutine(ActivateBananas());
        StartCoroutine(SpawnZooKeeper());
    }

    IEnumerator SpawnZooKeeper()
    {
        yield return new WaitForSeconds(zooKeeperSpawnDelay);
        Debug.Log("ZooKeeper spawning started");
        while (true)
        {
            GameObject zooKeeper = Instantiate(zooKeeperPrefab);
            if (zooKeeper != null)
            {
                Vector3 spawnPosition = GetRandomSpawnPositionForZookeeper(quokka);
                zooKeeper.transform.position = spawnPosition;
                Debug.Log("ZooKeeper spawned at position: " + spawnPosition);
            }
            else
            {
                Debug.LogError("ZooKeeper prefab is not assigned!");
            }

            yield return new WaitForSeconds(zooKeeperSpawnInterval);
        }
    }

    IEnumerator ActivateHumans()
    {
        while (true)
        {
            GameObject human = humanPool.GetObjectFromPool();
            if (human != null)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition(quokka);
                human.transform.position = spawnPosition;

                SpriteRenderer sr = human.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.flipX = spawnPosition.x > 0;
                }

                human.SetActive(true);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator ActivateBananas()
    {
        while (true)
        {
            GameObject banana = bananaPool.GetObjectFromPool();
            if (banana != null)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition(quokka);
                banana.transform.position = spawnPosition;
                banana.SetActive(true);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    float GetValidSpawnCoordinate(float localMin, float localMax)
    {
        return Random.Range(localMin, localMax);
    }

    Vector3 GetRandomSpawnPosition(GameObject referenceObject)
    {
        Vector3 spawnPosition;
        int attempts = 0;

        do
        {
            float randomX = GetValidSpawnCoordinate(HumanBananaPosMin, HumanBananaPosMax);
            float randomY = GetValidSpawnCoordinate(HumanBananaPosMin, HumanBananaPosMax);
            spawnPosition = new Vector3(referenceObject.transform.position.x + randomX, referenceObject.transform.position.y + randomY, -1);

            attempts++;
            Debug.Log($"Attempt #{attempts}: SpawnPosition = {spawnPosition}");
        }
        while (
            spawnPosition.x < GlobalPosXMin || spawnPosition.x > GlobalPosXMax ||
            spawnPosition.y < GlobalPosYMin || spawnPosition.y > GlobalPosYMax ||
            (spawnPosition.x > referenceObject.transform.position.x + HBExceptPosMin && spawnPosition.x < referenceObject.transform.position.x + HBExceptPosMax) ||
            (spawnPosition.y > referenceObject.transform.position.y + HBExceptPosMin && spawnPosition.y < referenceObject.transform.position.y + HBExceptPosMax)
        );

        Debug.Log($"Valid SpawnPosition Found: {spawnPosition}");
        return spawnPosition;
    }

    Vector3 GetRandomSpawnPositionForZookeeper(GameObject referenceObject)
    {
        Vector3 spawnPosition;
        int attempts = 0;

        do
        {
            float randomX = GetValidSpawnCoordinate(ZooKeeperPosMin, ZooKeeperPosMax);
            float randomY = GetValidSpawnCoordinate(ZooKeeperPosMin, ZooKeeperPosMax);
            spawnPosition = new Vector3(referenceObject.transform.position.x + randomX, referenceObject.transform.position.y + randomY, -1);

            attempts++;
            Debug.Log($"Attempt #{attempts}: SpawnPosition = {spawnPosition}");
        }
        while (
            spawnPosition.x < GlobalPosXMin || spawnPosition.x > GlobalPosXMax ||
            spawnPosition.y < GlobalPosYMin || spawnPosition.y > GlobalPosYMax ||
            (spawnPosition.x > referenceObject.transform.position.x + HBExceptPosMin && spawnPosition.x < referenceObject.transform.position.x + HBExceptPosMax) ||
            (spawnPosition.y > referenceObject.transform.position.y + HBExceptPosMin && spawnPosition.y < referenceObject.transform.position.y + HBExceptPosMax)
        );

        Debug.Log($"Valid SpawnPosition Found for Zookeeper: {spawnPosition}");
        return spawnPosition;
    }
}
