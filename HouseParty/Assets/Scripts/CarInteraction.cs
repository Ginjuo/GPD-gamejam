using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CarInteraction : MonoBehaviour
    {
        // Start is called before the first frame update
        private void OnTriggerEnter2D(Collider2D collider)
        {
            RubyController rc = collider.gameObject.GetComponent<RubyController>();
            if (rc == null)
                return;
            Loader.Load(Loader.Scene.CarEndScreen);
        }
    }
}