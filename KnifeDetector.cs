using UnityEngine;

namespace ThatvrkiddsBloodOverhall
{
    public class KnifeDetector : MonoBehaviour
    {
        public GameObject scratchEffect;

        private void OnCollisionEnter(Collision collision)
        {
            if (!IsKnife())
                return;

            if (collision.gameObject.CompareTag("NPC"))
            {
                ContactPoint hit = collision.contacts[0];

                Instantiate(
                    scratchEffect,
                    hit.point,
                    Quaternion.LookRotation(hit.normal)
                );
            }
        }

        bool IsKnife()
        {
            string name = gameObject.name.ToLower();

            return name.Contains("knife") ||
                   name.Contains("blade") ||
                   name.Contains("dagger");
        }
    }
}
