﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;

public class NonPlayerCharacter : ObjectiveLogic
{
    public TextMeshProUGUI NameText;

    void Start()
    {
        NameText.text = Name;
        HandleDialogInit();
    }

    void Update()
    {
        HandleDialogTimerLogic();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (ShowsDialog && collider.gameObject.GetComponent<RubyController>() != null)
            HandleObjective();
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        //if (!ShowsDialog && collider.gameObject.GetComponent<RubyController>() != null)
        //    return;
        //DialogBox.SetActive(false);
    }
}
