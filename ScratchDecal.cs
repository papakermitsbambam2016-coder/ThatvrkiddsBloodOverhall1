using UnityEngine;

namespace ThatvrkiddsBloodOverhall
{
    public class ScratchDecal : MonoBehaviour
    {
        public float destroyTime = 15f;

        void Start()
        {
            Destroy(gameObject, destroyTime);
        }

        public void AttachToNPC(Transform npc)
        {
            transform.SetParent(npc);
        }
    }
}
