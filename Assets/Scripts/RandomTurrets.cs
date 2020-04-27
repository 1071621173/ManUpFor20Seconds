using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomTurrets : MonoBehaviour, Turret
{
  public int maxProjectiles;
  public float projectileSpeed;
  public GameObject projectilePrefab;

  private float xAxisMax;
  private float yAxisMax;
  private ISet<GameObject> projectiles = new HashSet<GameObject>();

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
  }

  // Update is called once per frame
  void Update()
  {
    if (projectiles.Count < maxProjectiles)
    {
      fireBlindly();
    }
  }

  private void fireBlindly() 
  {
    // Find a random point on the 4 borders of the screen. This point will be the origination of the new projectile
    int borderSupplierIndex = Random.Range(0, 4);
    Vector2 origin = randomPointOnBorder(borderSupplierIndex);

    // create new projectile
    GameObject projectile = Instantiate(projectilePrefab, new Vector3(origin.x, origin.y, 0), Quaternion.identity);
    projectile.GetComponent<ProjectileOutOfBoundTrigger>().turret = this;

    // give it a velocity
    setVelocity(projectile, borderSupplierIndex, origin);

    projectiles.Add(projectile);
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

  public void OnProjectileDestruction(GameObject projectile) {
    projectiles.Remove(projectile);
    Destroy(projectile);
  }
}
