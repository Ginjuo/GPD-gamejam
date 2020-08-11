using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class ObjectiveLogic : CovidLogic
    {
        public int HandlesObjective;
        public GameObject DialogBox;
        public bool ShowsDialog;
        public TextMeshProUGUI DialogText;

        public void HandleDialogInit(string name)
        {
            if (DialogBox == null)
                return;
            DialogBox.SetActive(false);
            if (HandlesObjective >= 0)
            {
                ShowsDialog = true;
                DialogText.text = GameController.Instance.GetNPCText(HandlesObjective);
            }
        }

        public void HandleObjective()
        {
            Debug.Log($"{HandlesObjective} is validating set active");
            if (HandlesObjective == GameController.Instance.CurrentObjectiveNumber)
                DialogBox.SetActive(true);

            else if (GameController.Instance.NextObjectiveNumber == HandlesObjective)
            {
                DialogBox.SetActive(true);
                GameController.Instance.SetCurrentObjective(HandlesObjective);
            }
        }
    }
}
