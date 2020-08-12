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
            InfectedText.text = $"Before the party you had unknowingly contracted the COVID-19 virus. {Environment.NewLine} {Environment.NewLine} During the party you came too close to the other guests and infected {NameHolder.Instance.NumberOfInfected} others.";

            string personWithStory;

            List<string> intersection = NameHolder.Instance.InfectedNames.Where(inf => NameHolder.Instance.GetNpcNames().Contains(inf)).ToList();

            personWithStory = intersection.Count > 0 ? intersection[UnityEngine.Random.Range(0, intersection.Count)] : NameHolder.Instance.InfectedNames.Count > 0 ? NameHolder.Instance.InfectedNames[UnityEngine.Random.Range(0, NameHolder.Instance.InfectedNames.Count)] : "Someone from the party ";
            StoryText.text = $"{personWithStory} visited their grandmother the day after the party. {Environment.NewLine} The grandmother then got infected with the virus and passed away shortly after. {Environment.NewLine} {Environment.NewLine} Can you do better ? ";
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
