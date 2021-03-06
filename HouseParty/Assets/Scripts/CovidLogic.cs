﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class CovidLogic : MonoBehaviour
    {
        public bool HasCovid, CanGetCovid;
        public int ContractionChance;
        public string Name;
        public void GetCovid(CovidLogic covidLogic)
        {
            if (HasCovid || !CanGetCovid)
                return;

            if (covidLogic.HasCovid && Random.Range(1, 101) <= ContractionChance)
            {
                HasCovid = true;
                //if(NameHolder.Instance.GetNpcNames().Contains(Name))
                NameHolder.Instance.InfectedNames.Add(Name);

                NameHolder.Instance.NumberOfInfected++;
                //Debug.Log($"{Name} got COVID from: " + covidLogic.Name + " N_infected = " + NameHolder.Instance.NumberOfInfected);
            }
        }
    }
}
