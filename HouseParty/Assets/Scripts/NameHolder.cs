using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class NameHolder : MonoBehaviour
    {
        public TextMeshProUGUI Name1;
        public TextMeshProUGUI Name2;
        public TextMeshProUGUI Name3;
        public TextMeshProUGUI Name4;
        public TextMeshProUGUI Name5;

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

        public List<string> GetNames()
        {
            _names.Add("Carl");
            _names.Add("Bent");
            _names.Add("Heidi");
            _names.Add("Bente");
            return _names;
        }

    }
}
