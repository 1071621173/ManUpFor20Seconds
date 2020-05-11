using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreTextController : MonoBehaviour
{
    private Text text;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        Profile.score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Profile.score = Mathf.FloorToInt(timer * 100);
        text.text = "Score: " + Profile.score;
    }
}
