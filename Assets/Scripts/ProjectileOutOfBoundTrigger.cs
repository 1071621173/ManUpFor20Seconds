using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOutOfBoundTrigger : MonoBehaviour
{
  public Turret turret;

  public void OnTriggerExit2D(Collider2D other) {
    if ("PlayArea" == other.gameObject.name && isOutOfBound(other.gameObject)) {
        turret.OnProjectileDestruction(this.gameObject);
    }
  }

  private bool isOutOfBound(GameObject playArea) {
    float x = Math.Abs(this.gameObject.transform.position.x);
    float y = Math.Abs(this.gameObject.transform.position.y);

    return turret.getPlayAreaBounds().x < x || turret.getPlayAreaBounds().y < y;
  }
}
