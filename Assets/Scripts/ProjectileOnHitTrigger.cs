using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileOnHitTrigger : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if ("PlayerShip" == other.gameObject.name)
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
    }
}
