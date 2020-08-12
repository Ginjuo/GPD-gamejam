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
        private float _dialogtimer = 0;
        private float _dialogTime = 4.0f;
        private bool _startDialogTimer = false;

        // Audio stuff
        private AudioSource _audioSource;
        public AudioClip TalkClip;
        public AudioClip GetQuestClip;

        private void PlaySound(AudioClip clip)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.PlayOneShot(clip);
            }
        }

        public void HandleDialogTimerLogic()
        {
            if (_startDialogTimer)
            {
                _dialogtimer -= Time.deltaTime;
                if (_dialogtimer < 0)
                {
                    DialogBox.SetActive(false);
                    _startDialogTimer = false;
                    _dialogtimer = _dialogTime;
                }
            }
        }

        public void HandleDialogInit()
        {
            _audioSource = GetComponent<AudioSource>(); 
            DialogBox.SetActive(false);
            _dialogtimer = _dialogTime;

            if (DialogBox == null)
                return;

            if (HandlesObjective >= 0)
            {
                ShowsDialog = true;
                DialogText.text = GameController.Instance.GetNPCText(HandlesObjective);
            }
        }

        private void DisplayDialogBox()
        {
            _dialogtimer = _dialogTime;
            _startDialogTimer = true;
            DialogBox.SetActive(true);
        }


        public void HandleObjective()
        {
            if (GameController.Instance.CurrentObjectiveNumber-1 == HandlesObjective)
            {
                DisplayDialogBox();
                PlaySound(TalkClip);
            }

            else if (GameController.Instance.CurrentObjectiveNumber == HandlesObjective)
            {
                DisplayDialogBox();
                GameController.Instance.SetCurrentObjective(HandlesObjective);

                _audioSource.PlayOneShot(GetQuestClip);
                _audioSource.PlayOneShot(TalkClip);

                if (GameController.Instance.LastObjectiveNumber == HandlesObjective)
                    GameController.Instance.StartEndTimer = true;
            }
        }
    }
}
