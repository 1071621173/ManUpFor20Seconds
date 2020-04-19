using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private SpriteRenderer spriteRenderer;

    private Vector3 startPos;
    private Vector3 targetPos;


// Start is called before the first frame update
void Start()
    {
        // Add some randomness;
        speed = speed * (1 + Randomness(0.1f));
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = RandomColor();

        startPos = transform.position;
        targetPos = Vector3.zero;


        //bullet
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((targetPos - startPos).normalized * speed * Time.deltaTime);
    }

    private Color RandomColor()
    {
        Color[] colors = new Color[]
        {
            Color.white,
            Color.grey,
            Color.green,
            Color.yellow,
            Color.magenta,
            Color.blue,
            Color.red,
            Color.cyan,
        };

        return colors[Random.Range(0, colors.Length)];
    }

    private float Randomness(float range)
    {
        return Random.Range(-range, range);
    }

}
