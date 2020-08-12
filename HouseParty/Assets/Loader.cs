using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        CarEndScreen,
        StdEndScreen,
        TrueScene,
        Startscreen
    }

    public static void Load(Scene sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad.ToString());
    }
}
