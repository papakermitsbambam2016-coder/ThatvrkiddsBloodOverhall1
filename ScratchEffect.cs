using UnityEngine;

namespace ThatvrkiddsBloodOverhall
{
    public class ScratchEffect : MonoBehaviour
    {
        public float lifeTime = 10f;

        void Start()
        {
            Destroy(gameObject, lifeTime);
        }
    }
}
