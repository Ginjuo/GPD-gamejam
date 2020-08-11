using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        private readonly List<string> _names = new List<string> {"Carl", "Bent", "Heidi", "Bente"};

        private readonly List<string> _predefinedNames = new List<string> {"Random1", "Random2", "Random3", "Random4", "Random5", "Random6", "Random7" };

        public static GameController Instance { get; private set; }

        private static readonly object NameLock = new object();

        public TextMeshProUGUI UiText;

        void Awake()
        {
            DontDestroyOnLoad(this);
            Instance = this;
            Debug.Log("Game controller instantiated in awake");
            UiText.alpha = 0;
        }

        public string GetName()
        {
            lock (NameLock)
            {
                string toReturn;
                if (_names.Count > 0)
                {
                    toReturn = _names[0];
                    _names.RemoveAt(0);
                    return toReturn;
                }
                toReturn = _predefinedNames[0];
                _predefinedNames.RemoveAt(0);
                return toReturn;
            }
        }
    }

}
