using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private Text text;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        Profile.GetInstance().score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Profile.GetInstance().score = Mathf.FloorToInt(timer * 100);
        text.text = "Score: " + Profile.GetInstance().score;
    }
}
