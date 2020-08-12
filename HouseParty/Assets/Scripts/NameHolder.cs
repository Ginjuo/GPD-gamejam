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
        public TMP_InputField Name5;

        private readonly List<string> _names = new List<string>( );
        public string PlayerName => "player";

        public static NameHolder Instance { get; private set; }

        public List<string> InfectedNames { get; set; } = new List<string>();

        public int NumberOfInfected { get; set; }

        void Awake()
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }

        public void SetNames()
        {
            if (Name1.text != "")
            {
                _names.Add(Name1.text);
            }

            if (Name2.text != "")
            {
                _names.Add(Name2.text);
            }

            if (Name3.text != "")
            {
                _names.Add(Name3.text);
            }

            if (Name4.text != "")
            {
                _names.Add(Name4.text);
            }

            if (Name5.text != "")
            {
                _names.Add(Name5.text);
            }
        }

        public List<string> GetNames()
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
