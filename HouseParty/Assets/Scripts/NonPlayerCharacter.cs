using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;

public class NonPlayerCharacter : ObjectiveLogic
{
    // Audio stuff
    //private AudioSource _audioSource;
    //public AudioClip TalkAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        //_audioSource = GetComponent<AudioSource>();

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
