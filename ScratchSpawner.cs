using UnityEngine;

namespace ThatvrkiddsBloodOverhall
{
    public class ScratchSpawner : MonoBehaviour
    {
        public GameObject scratchPrefab;

        public void SpawnScratch(Vector3 position, Vector3 normal, Transform npc)
        {
            GameObject scratch = Instantiate(
                scratchPrefab,
                position,
                Quaternion.LookRotation(normal)
            );

            scratch.transform.SetParent(npc);
        }
    }
}
