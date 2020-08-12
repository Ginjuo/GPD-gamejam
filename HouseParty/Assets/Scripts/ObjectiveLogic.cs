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

        // Audio stuff
        private AudioSource _audioSource;
        public AudioClip TalkClip;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }

        public void HandleDialogInit()
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
            if (HandlesObjective == GameController.Instance.CurrentObjectiveNumber)
                DialogBox.SetActive(true);

            else if (GameController.Instance.NextObjectiveNumber == HandlesObjective)
            {
                DialogBox.SetActive(true);
                GameController.Instance.SetCurrentObjective(HandlesObjective);

                if (GameController.Instance.LastObjectiveNumber == HandlesObjective)
                    GameController.Instance.StartEndTimer = true;
            }

            if (HandlesObjective != -1)
            {
                Debug.Log("NPC with number: " + HandlesObjective + ", playing TalkClip");
                //PlaySound(TalkClip);
            }
        }
    }
}
