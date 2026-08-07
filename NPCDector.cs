using UnityEngine;

namespace ScratchLab
{
    internal static class NPCDetector
    {
        public static bool IsNPC(GameObject obj)
        {
            if (obj == null)
                return false;

            if (obj.CompareTag("NPC"))
                return true;

            if (Utilities.NameContains(obj.name, Config.NPCKeywords))
                return true;

            Transform parent = obj.transform.parent;

            while (parent != null)
            {
                if (Utilities.NameContains(parent.name, Config.NPCKeywords))
                    return true;

                parent = parent.parent;
            }

            return false;
        }
    }
}
