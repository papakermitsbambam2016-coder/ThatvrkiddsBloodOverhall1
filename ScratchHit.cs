using UnityEngine;

namespace ThatvrkiddsBloodOverhall
{
    public class ScratchHit : MonoBehaviour
    {
        public GameObject scratchEffect;

        private void OnCollisionEnter(Collision collision)
        {
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
    }
}
