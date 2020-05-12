using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class FinalScoreText : MonoBehaviour
{
    private Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = "Score: " + Profile.GetInstance().score;
    }
}
