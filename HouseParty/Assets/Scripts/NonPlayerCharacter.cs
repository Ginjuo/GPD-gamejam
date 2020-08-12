using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;

public class NonPlayerCharacter : ObjectiveLogic
{
    public TextMeshProUGUI NameText;

    void Start()
    {
        //_audioSource = GetComponent<AudioSource>();
        NameText.text = Name;
        HandleDialogInit();
    }

    void Update()
    { 
       
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (ShowsDialog && collider.gameObject.GetComponent<RubyController>() != null)
            HandleObjective();
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!ShowsDialog && collider.gameObject.GetComponent<RubyController>() != null)
            return;
        DialogBox.SetActive(false);
    }
}
