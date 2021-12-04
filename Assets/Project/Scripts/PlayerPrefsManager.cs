using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class PlayerPrefsManager
{
    const string livesPref = "lives";
    const string fishesPref = "fish";
    const string popsciclesPref = "popscicles";

    public static int GetLives()
    {
        return PlayerPrefs.GetInt(livesPref);
    }

    public static void SetLives(int lives)
    {
        PlayerPrefs.SetInt(livesPref, lives);
    }

    public static int GetFish()
    {
        return PlayerPrefs.GetInt(fishesPref);
    }

    public static void SetFish(int fish)
    {
        PlayerPrefs.SetInt(fishesPref, fish);
    }

    public static int GetPopscicles()
    {
        return PlayerPrefs.GetInt(popsciclesPref);
    }

    public static void SetPopscicles(int popscicles)
    {
        PlayerPrefs.SetInt(popsciclesPref, popscicles);
    }
}
