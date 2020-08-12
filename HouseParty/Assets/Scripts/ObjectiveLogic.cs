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
        public AudioClip GetQuestClip;
        public AudioClip CompleteQuestClip;

        void Start()
        {
            //_audioSource = GetComponentInChildren<AudioSource>();
        }

        public void PlaySound(AudioClip clip)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.PlayOneShot(clip);
            }
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
            {
                DialogBox.SetActive(true);

                _audioSource = GetComponent<AudioSource>();
                PlaySound(TalkClip);
            }

            else if (GameController.Instance.NextObjectiveNumber == HandlesObjective)
            {
                Debug.Log($"Next objective to be handled: {GameController.Instance.NextObjectiveNumber}. Handles obective: {HandlesObjective}");
                DialogBox.SetActive(true);
                GameController.Instance.SetCurrentObjective(HandlesObjective);

                _audioSource = GetComponent<AudioSource>();
                _audioSource.PlayOneShot(GetQuestClip);
                PlaySound(TalkClip);

                if (GameController.Instance.LastObjectiveNumber == HandlesObjective)
                    GameController.Instance.StartEndTimer = true;
            }
        }
    }
}
