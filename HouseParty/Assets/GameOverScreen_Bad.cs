using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen_Bad : MonoBehaviour
{
    public void ReplayGame()
    {
        Loader.Load(Loader.Scene.TrueScene);
    }
}
