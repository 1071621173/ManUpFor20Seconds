using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Turret
{
    void OnProjectileDestruction(GameObject projectile);

    Vector2 getPlayAreaBounds();
}
