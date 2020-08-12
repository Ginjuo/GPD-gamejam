using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using NameHolder = Assets.Scripts.NameHolder;

public class StartScreen : MonoBehaviour
{
    public void GoButton()
    {
        NameHolder.Instance.SetNpcNames();
        //NameHolder.Instance.PrintNamesToLog();

        if (NameHolder.Instance.SetPlayerName())
        {
            Loader.Load(Loader.Scene.MainScene);
        }
    }
}
