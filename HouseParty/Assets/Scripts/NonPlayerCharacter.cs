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
            if (Drink != null && GameController.Instance.CurrentObjectiveNumber == HandlesObjective)
            {
                Renderer r = Drink?.GetComponent<Renderer>();
                if (r != null)
                    r.enabled = false;
            }
            HandleObjective();
        }

        RubyController rc = collider.gameObject.GetComponent<RubyController>();
        if (rc == null)
            return;
        GetCovid(rc.gameObject.GetComponent<CovidLogic>());
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        //if (!ShowsDialog && collider.gameObject.GetComponent<RubyController>() != null)
        //    return;
        //DialogBox.SetActive(false);
    }
}
