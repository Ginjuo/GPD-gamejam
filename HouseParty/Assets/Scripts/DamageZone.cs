using UnityEngine;

namespace Assets.Scripts
{
    public class DamageZone : MonoBehaviour
    {
        void OnTriggerStay2D(Collider2D other)
        {
            RubyController controller = other.GetComponent<RubyController>();

            if (controller != null)
            {

            }
        }
    }
}
