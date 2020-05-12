using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Profile
{
    private static Profile instance;
    public int score;

    private Profile()
    {

    }

    public static Profile GetInstance()
    {
        if (instance == null)
            instance = new Profile();
        return instance;
    }
}
