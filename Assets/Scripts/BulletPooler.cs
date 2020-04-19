using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private int bulletCount;


    private void Awake()
    {
        
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.transform.position = RandomSpawnPosition(20);
        }

    }

    private Vector3 RandomSpawnPosition(float distance)
    {
        Vector3 spawnPoint;
        Vector3 targetPos = Vector3.zero;
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        spawnPoint = targetPos + direction * distance;
        return spawnPoint;
    }

    [System.Obsolete("子弹现在太密集，需要一个程序打开窗口让飞船逃生。")]
    private Vector3 OpenGap()
    {
        return Vector3.zero;
    }
}
