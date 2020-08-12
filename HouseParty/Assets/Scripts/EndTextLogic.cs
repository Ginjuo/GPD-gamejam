using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class EndTextLogic : MonoBehaviour
    {
        public TextMeshProUGUI EndGameText;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            string text =
                $"The actions you took at the party, caused you to infect {NameHolder.Instance.NumberOfInfected} people with the COVID-19 virus";

            EndGameText.text = text;
        }
    }
}
