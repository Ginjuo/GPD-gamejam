using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public void GoButton()
    {
        NameHolder.Instance.SetNames();
        //NameHolder.Instance.PrintNamesToLog();

        Loader.Load(Loader.Scene.MainScene);
    }
}
