using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        private List<string> _predefinedNames = new List<string> { "Lis", "Sofia", "David", "John", "Eric", "Jess", "Taylor", "Mel", "Denny", "Drew", "Logan", "Edward", "Caden", "Kira", "Emily", "Robyn", "Dennis", "Tobias", "Casper", "Louis", "Alex", "Helena", "Erica", "Anna", "Rene", "Karina" };
        private List<string> _reservedNames = new List<string>();
        private List<string> _userDefinedNames = new List<string>();
        private string _playerName;
        public static GameController Instance { get; private set; }
        private float _timer = 5.0f; // 5s
        private readonly Dictionary<int,string> _objectiveTextDict = new Dictionary<int, string>();
        private readonly Dictionary<int, string> _npcTextDict = new Dictionary<int, string>();
        private UITimerBar _uiTimerBar;

        public string NameOfDrinkRecipient { get; set; }
        public string NameOfPersonToFind { get; set; }
        public TextMeshProUGUI UiText;
        public int CurrentObjectiveNumber { get; set; } = 1;
        public int LastObjectiveNumber => 5;
        public bool StartEndTimer { get; set; } = false;

        public double EndTimer = 120.0d; //2 min

        void Update()
        {
            EndTimer -= Time.deltaTime;
            _uiTimerBar.SetTimerPercent(EndTimer);

            if (StartEndTimer)
                _timer -= Time.deltaTime;

            if (_timer < 0 || EndTimer < 0)
                Loader.Load(Loader.Scene.StdEndScreen);

        }

        private void FilterAndFixNames()
        {
            _userDefinedNames = _userDefinedNames.Select(s => s.Split(' ')[0]).ToList();
            _predefinedNames = _predefinedNames.Where(s1 => !_userDefinedNames.Any(s2 => s2.Equals(s1, StringComparison.OrdinalIgnoreCase))).Select(s => s).ToList();

            int index;
            if (_userDefinedNames.Count == 0)
            {
                index = UnityEngine.Random.Range(0, _predefinedNames.Count);
                NameOfDrinkRecipient = _predefinedNames[index];
                _predefinedNames.RemoveAt(index);

                index = UnityEngine.Random.Range(0, _predefinedNames.Count);
                NameOfPersonToFind = _predefinedNames[index];
                _predefinedNames.RemoveAt(index);
            }
            else if (_userDefinedNames.Count == 1)
            {
                NameOfDrinkRecipient = _userDefinedNames[0];
                _userDefinedNames.RemoveAt(0);

                index = UnityEngine.Random.Range(0, _predefinedNames.Count);
                NameOfPersonToFind = _predefinedNames[index];
                _predefinedNames.RemoveAt(index);
            }
            else
            {
                index = UnityEngine.Random.Range(0, _userDefinedNames.Count);
                NameOfDrinkRecipient = _userDefinedNames[index];
                _userDefinedNames.RemoveAt(index);

                index = UnityEngine.Random.Range(0, _userDefinedNames.Count);
                NameOfPersonToFind = _userDefinedNames[index];
                _userDefinedNames.RemoveAt(index);
            }
        }

        void Awake()
        {
            Instance = this;
            _uiTimerBar = GameObject.Find("TimerBar").GetComponent<UITimerBar>();
            _userDefinedNames = NameHolder.Instance.GetNpcNames();
            _playerName = NameHolder.Instance.GetPlayerName();

            FilterAndFixNames();

            
            SetTexts();
        }

        private void SetTexts()
        {
            _npcTextDict.Add(1, $"Nice to see you {_playerName}! Welcome to the paartaaay! Go get a drink from the bartender");
            _npcTextDict.Add(2, $"Are you {_playerName}? {NameOfDrinkRecipient} told me to look for you and tell you that they wanted a drink");
            _npcTextDict.Add(3, $"There you are {_playerName}! and you brought me a drink... I've heard that {NameOfPersonToFind} would like to talk to you");
            _npcTextDict.Add(4, $"Heeeey {_playerName}! Can you go to the DJ and ask him to change the music?");
            _npcTextDict.Add(5, "Yeah okay i can put on a new track...");

            _objectiveTextDict.Add(1, "Go find the bartender");
            _objectiveTextDict.Add(2, $"Bring the drink to {NameOfDrinkRecipient}");
            _objectiveTextDict.Add(3, $"{NameOfDrinkRecipient} told you that {NameOfPersonToFind} might want to talk to you");
            _objectiveTextDict.Add(4, $"{NameOfPersonToFind} asked you to go ask the DJ to change the music");
            _objectiveTextDict.Add(5, $"{NameOfPersonToFind} asked you to go ask the DJ to change the music");
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
            CurrentObjectiveNumber++;
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
            int index = UnityEngine.Random.Range(0, _predefinedNames.Count);
            toReturn = _predefinedNames[index];
            _predefinedNames.RemoveAt(index);
            return toReturn;
        }
    }
}
