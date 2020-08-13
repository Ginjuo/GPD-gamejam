using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class EndTextLogic : MonoBehaviour
    {
        public TextMeshProUGUI InfectedText;
        public TextMeshProUGUI StoryText;

        // Start is called before the first frame update
        void Start()
        {
            //InfectedText.text = $"It turned out you had contracted the COVID-19 virus before you even went to the party. {Environment.NewLine} {Environment.NewLine} During the party you came too close to the other guests and infected {NameHolder.Instance.NumberOfInfected} others.";
            InfectedText.text = $"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}During the party you came too close to the other guests and infected {NameHolder.Instance.NumberOfInfected} others.";
            string personWithStory;

            List<string> intersection = NameHolder.Instance.InfectedNames.Where(inf => NameHolder.Instance.GetNpcNames().Contains(inf)).ToList();

            personWithStory = intersection.Count > 0 ? intersection[UnityEngine.Random.Range(0, intersection.Count)] : NameHolder.Instance.InfectedNames.Count > 0 ? NameHolder.Instance.InfectedNames[UnityEngine.Random.Range(0, NameHolder.Instance.InfectedNames.Count)] : "Someone from the party";
            StoryText.text = $"{personWithStory} visited their grandmother the day after the party.{Environment.NewLine}The grandmother then got infected with the virus and passed away shortly after.{Environment.NewLine}{Environment.NewLine}Can you do better?";

            // Emptying N infected
            NameHolder.Instance.NumberOfInfected = 0;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
