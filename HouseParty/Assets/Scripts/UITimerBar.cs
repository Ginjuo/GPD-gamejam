﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UITimerBar : MonoBehaviour
    {
        private Slider _timerBar;
        private int _currentValue;

        void Awake()
        {
            _timerBar = GetComponent<Slider>();
            _timerBar.maxValue = (int)GameController.Instance.EndTimer;
            _timerBar.value = (int)GameController.Instance.EndTimer;
        }

        public void SetTimerPercent(double value)
        {
            _timerBar.value = (int)value;
        }
    }
}
