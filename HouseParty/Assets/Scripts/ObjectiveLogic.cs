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

        public void PlaySound(AudioClip clip)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.PlayOneShot(clip);
            }
        }

        public void HandleDialogInit()
        {
            _audioSource = GetComponent<AudioSource>();
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
            if (GameController.Instance.CurrentObjectiveNumber-1 == HandlesObjective)
            {
                DialogBox.SetActive(true);

                PlaySound(TalkClip);
            }

            else if (GameController.Instance.CurrentObjectiveNumber == HandlesObjective)
            {
                DialogBox.SetActive(true);
                GameController.Instance.SetCurrentObjective(HandlesObjective);

                _audioSource.PlayOneShot(GetQuestClip);
                _audioSource.PlayOneShot(TalkClip);

                if (GameController.Instance.LastObjectiveNumber == HandlesObjective)
                    GameController.Instance.StartEndTimer = true;
            }
        }
    }
}
