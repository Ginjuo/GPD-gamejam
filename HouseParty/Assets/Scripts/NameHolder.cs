using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class NameHolder : MonoBehaviour
    {
        public TMP_InputField Name1;
        public TMP_InputField Name2;
        public TMP_InputField Name3;
        public TMP_InputField Name4;
        public TextMeshProUGUI StoryText;

        private string _errorText = "You forgot to enter your name, you can go back and change it";

        private string _storyText =
            $"Halleluja! The lockdown is easing up and you are finally allowed to go to parties with your highschool friends again!  {Environment.NewLine} Your mother drives you to the party and says: You are only allowed to stay here for an hour. Are you sure you dont want to hang out at home instead?.  {Environment.NewLine} You: No! I want to go meet all of my friends!  {Environment.NewLine} You have been to a pre-party and is a bit drunk, so you might stumble into a person or two...  {Environment.NewLine}  {Environment.NewLine} Use<WASD> or arrow keys to move!";

        public TMP_InputField PlayerName;

        private readonly List<string> _names = new List<string>( );
        private string _playerName;

        public static NameHolder Instance { get; private set; }

        public List<string> InfectedNames { get; set; } = new List<string>();

        public int NumberOfInfected { get; set; }

        void Awake()
        {
            DontDestroyOnLoad(this);
            Instance = this;
            StoryText.text = _errorText;
            PlayerName.onValueChanged.AddListener(delegate { ValueCheck();});
        }

        private void ValueCheck()
        {
            if(PlayerName.text != string.Empty)
                StoryText.text = _storyText;
        }
        //private void Update()
        //{
        //    if (Input.GetKey(KeyCode.Escape))
        //    {
        //        Application.Quit();
        //    }
        //}

        public bool SetPlayerName()
        {
            if (PlayerName.text != string.Empty)
            {
                _playerName = PlayerName.text;
                return true;
            }

            return false;
        }

        public string GetPlayerName()
        {
            return _playerName;
        }

        public void SetNpcNames()
        {
            if (Name1.text != String.Empty)
            {
                _names.Add(Name1.text);
            }

            if (Name2.text != String.Empty)
            {
                _names.Add(Name2.text);
            }

            if (Name3.text != String.Empty)
            {
                _names.Add(Name3.text);
            }

            if (Name4.text != String.Empty)
            {
                _names.Add(Name4.text);
            }

            // if (Name5.text != String.Empty)
            // {
            //     _names.Add(Name5.text);
            // }
        }

        public List<string> GetNpcNames()
        {
            return _names;
        }

        public void PrintNamesToLog()
        {
            if (_names.Count == 0)
            {
                Debug.Log("There were no names to print");
            }
            
            foreach (string name in _names)
            {
                Debug.Log(name);
            }
        }

    }
}
