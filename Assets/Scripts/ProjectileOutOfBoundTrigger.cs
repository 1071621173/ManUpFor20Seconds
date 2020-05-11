using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOutOfBoundTrigger : MonoBehaviour
{
    private float xAxisMax;
    private float yAxisMax;

    public void setBound(float xAxisMax, float yAxisMax)
    {
        this.xAxisMax = xAxisMax;
        this.yAxisMax = yAxisMax;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if ("PlayArea" == other.gameObject.name && isOutOfBound(other.gameObject))
        {
            this.gameObject.SetActive(false);
        }
    }

    private bool isOutOfBound(GameObject playArea) 
    {
        float x = Math.Abs(this.gameObject.transform.position.x);
        float y = Math.Abs(this.gameObject.transform.position.y);

        return xAxisMax < x || yAxisMax < y;
    }
}
