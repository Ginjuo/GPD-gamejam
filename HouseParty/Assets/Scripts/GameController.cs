using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        private readonly List<string> _predefinedNames = new List<string> { "Random1", "Random2", "Random3", "Random4", "Random5", "Random6", "Random7" };
        private readonly List<string> _reservedNames = new List<string>();
        private List<string> _userDefinedNames = new List<string>();
        private string _playerName;
        public static GameController Instance { get; private set; }
        private static readonly object NameLock = new object();
        private float _timer = 2.0f;
        private readonly Dictionary<int,string> _objectiveTextDict = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _npcTextDict = new Dictionary<int, string>();
        private double _endTimer = 120.0d; //2 min

        public string NameOfDrinkRecipient { get; set; }
        public string NameOfPersonToFind { get; set; }
        public TextMeshProUGUI UiText;
        public TextMeshProUGUI TimerText;
        public int CurrentObjectiveNumber { get; set; }
        public int NextObjectiveNumber { get; set; } = 1;
        public int LastObjectiveNumber => 5;
        public bool StartEndTimer { get; set; } = false;

        void Start()
        {
           
        }

        void Update()
        {
            TimerText.text = $"TIME LEFT: {_endTimer:n0}";
            _endTimer -= Time.deltaTime;

            if (StartEndTimer)
                _timer -= Time.deltaTime;

            if (_timer < 0 || _endTimer < 0)
                Loader.Load(Loader.Scene.EndscreenTimer);

        }


        void Awake()
        {
            Instance = this;
            _userDefinedNames = NameHolder.Instance.GetNpcNames();
            _playerName = NameHolder.Instance.GetPlayerName();
            if (_userDefinedNames.Count == 0)
            {
                NameOfDrinkRecipient = _predefinedNames[0];
                NameOfPersonToFind = _predefinedNames[1];
            }
            else if (_userDefinedNames.Count == 1)
            {
                NameOfDrinkRecipient = _userDefinedNames[0];
                _userDefinedNames.RemoveAt(0);
                NameOfPersonToFind = _predefinedNames[0];
                _predefinedNames.RemoveAt(0);
            }
            else
            {
                NameOfDrinkRecipient = _userDefinedNames[0];
                _userDefinedNames.RemoveAt(0);
                NameOfPersonToFind = _userDefinedNames[1];
                _userDefinedNames.RemoveAt(0);
            }
            SetTexts();
        }

        private void SetTexts()
        {
            _npcTextDict.Add(1, $"Hey {_playerName}! Welcome to the paartaaay! Go get a drink from the bartender");
            _npcTextDict.Add(2, $"Are you {_playerName}? {NameOfDrinkRecipient} told me to look for you and tell you that they wanted a drink");
            _npcTextDict.Add(3, $"There you are {_playerName}! and you brought me a drink... I've heard that {NameOfPersonToFind} would like to talk to you");
            _npcTextDict.Add(4, "Bla bla bla bla... Can you go to the DJ and ask him to change the music?");
            _npcTextDict.Add(5, "Yeah okay i can put on a new track...");

            _objectiveTextDict.Add(1, "Go find the bartender");
            _objectiveTextDict.Add(2, $"Bring the drink to {NameOfDrinkRecipient}");
            _objectiveTextDict.Add(3, $"{NameOfDrinkRecipient} told you that {NameOfPersonToFind} might want to talk to you");
            _objectiveTextDict.Add(4, $"{NameOfPersonToFind} asked you to go ask the DJ to change the music");
        }

        public int GetSpecificObjectiveNumber(string name)
        {
            if (NameOfDrinkRecipient == name)
                return 3;

            if (NameOfPersonToFind == name)
                return 4;

            return -1;
        }

        public string GetNPCText(int npcNumber)
        {
            if (!_npcTextDict.TryGetValue(npcNumber, out string npcText))
                return "";
            return npcText;
        }

        public void SetCurrentObjective(int objectiveNumber)
        {
            if (objectiveNumber < CurrentObjectiveNumber || !_objectiveTextDict.TryGetValue(objectiveNumber, out string objectiveText))
                return;

            UiText.text = objectiveText;
            CurrentObjectiveNumber = objectiveNumber;
            NextObjectiveNumber = objectiveNumber + 1;
        }

        public string GetName(int objectiveId)
        {
            if (objectiveId == 3)
                return NameOfDrinkRecipient;
            if (objectiveId == 4)
                return NameOfPersonToFind;

            string toReturn;
            if (_userDefinedNames.Count > 0)
            {
                toReturn = _userDefinedNames[0];
                _userDefinedNames.RemoveAt(0);
                return toReturn;
            }
            toReturn = _predefinedNames[0];
            _predefinedNames.RemoveAt(0);
            return toReturn;
        }
    }
}
