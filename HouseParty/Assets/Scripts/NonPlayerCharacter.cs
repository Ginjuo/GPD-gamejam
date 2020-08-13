using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;

public class NonPlayerCharacter : ObjectiveLogic
{
    public TextMeshProUGUI NameText;
    public GameObject Drink;

    void Start()
    {
        NameText.text = Name;
        HandleDialogInit();
        if (Drink != null)
            Drink.GetComponent<Renderer>().enabled = true;
    }

    void Update()
    {
        HandleDialogTimerLogic();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (ShowsDialog && collider.gameObject.GetComponent<RubyController>() != null)
        {
            HandleObjective();
            if (HandlesObjective == 2 && Drink != null)
            {
                Renderer r = Drink?.GetComponent<Renderer>();
                if (r != null)
                    r.enabled = false;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        //if (!ShowsDialog && collider.gameObject.GetComponent<RubyController>() != null)
        //    return;
        //DialogBox.SetActive(false);
    }
}
