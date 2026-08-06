using UnityEngine;
using System.Collections.Generic;

namespace ThatvrkiddsBloodOverhall
{
    public class ScratchManager : MonoBehaviour
    {
        public static ScratchManager Instance;

        public List<GameObject> scratches = new List<GameObject>();

        public int maxScratches = 50;

        void Awake()
        {
            Instance = this;
        }

        public void AddScratch(GameObject scratch)
        {
            scratches.Add(scratch);

            if (scratches.Count > maxScratches)
            {
                Destroy(scratches[0]);
                scratches.RemoveAt(0);
            }
        }
    }
}
