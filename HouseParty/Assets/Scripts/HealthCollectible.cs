using UnityEngine;

namespace Assets.Scripts
{
    public class HealthCollectible : MonoBehaviour
    {
        public AudioClip collectedClip;
        void OnTriggerEnter2D(Collider2D other)
        {
            RubyController controller = other.GetComponent<RubyController>();

            if (controller != null)
            {
            
            }
        }

    }
}
