﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

public class RandomTurrets : MonoBehaviour
{
    public int maxProjectiles;
    public float projectileSpeed;
    public GameObject projectilePrefab;

    private float xAxisMax;
    private float yAxisMax;
    private ProjectilePool projectilePool;

    private IList<Func<Vector2>> turretPositionGenerator = new List<Func<Vector2>>(
        new Func<Vector2>[] {
            () => new Vector2(1, UnityEngine.Random.Range(-1f, 1f)),
            () => new Vector2(UnityEngine.Random.Range(-1f, 1f), 1),
            () => new Vector2(-1, UnityEngine.Random.Range(-1f, 1f)),
            () => new Vector2(UnityEngine.Random.Range(-1f, 1f), -1)
        }
    );

    // Start is called before the first frame update
    void Start()
    {
        this.yAxisMax = Camera.main.orthographicSize;
        this.xAxisMax = this.yAxisMax * Screen.width / Screen.height;

        this.projectilePool = new ProjectilePool(projectilePrefab, maxProjectiles, xAxisMax, yAxisMax);
        projectilePool.init();
    }

    // Update is called once per frame
    void Update()
    {
        var inactiveProjectiles = projectilePool.getInactive();
        if (inactiveProjectiles.Count > 0)
        {
            fire(inactiveProjectiles.First());
        }
    }

    private void fire(GameObject projectile) 
    {
        // Find a random point on the 4 borders of the screen. This point will be the origination of the new projectile
        int borderSupplierIndex = Random.Range(0, 4);
        Vector2 origin = randomPointOnBorder(borderSupplierIndex);

        // give it a velocity
        projectile.SetActive(true);
        projectile.transform.position = new Vector3(origin.x, origin.y, 0);
        setVelocity(projectile, borderSupplierIndex, origin);
    }

    private void setVelocity(GameObject projectile, int originBorderIndex, Vector2 origin) {
        // aim for a random point on the opposite border
        int oppositeBorderIndex = (originBorderIndex + 2) % 4;
        Vector2 target = randomPointOnBorder(oppositeBorderIndex);
        Vector2 velocityDirection = target - origin;
        velocityDirection.Normalize();

        Rigidbody2D rigidBody = projectile.GetComponent<Rigidbody2D>();
        rigidBody.velocity = velocityDirection * projectileSpeed;
    }

    private Vector2 randomPointOnBorder(int borderIndex) {
        Vector2 randomBorder = turretPositionGenerator[borderIndex].Invoke();
        return randomBorder * new Vector2(xAxisMax, yAxisMax);
    }

    public Vector2 getPlayAreaBounds() {
        return new Vector2(xAxisMax, yAxisMax);
    }

    private class ProjectilePool {
        private readonly GameObject projectilePrefab;
        private readonly int size;
        private readonly float xAxisMax;
        private readonly float yAxisMax;
        private ISet<GameObject> pool = new HashSet<GameObject>();

        public ProjectilePool(GameObject projectilePrefab, int size, float xAxisMax, float yAxisMax) {
            this.projectilePrefab = projectilePrefab;
            this.size = size;
            this.xAxisMax = xAxisMax;
            this.yAxisMax = yAxisMax;
        }

        public void init()
        {
            for (int i = 0; i < size; i++) {
                GameObject projectile = Instantiate(projectilePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                projectile.SetActive(false);
                projectile.GetComponent<ProjectileOutOfBoundTrigger>().setBound(xAxisMax, yAxisMax);
                pool.Add(projectile);
            }
        }

        public List<GameObject> getInactive() {
            return pool.Where(go => !go.activeSelf).ToList();
        }
    }
}
