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

            Transform current = obj.transform.parent;

            while (current != null)
            {
                GameObject parentObject = current.gameObject;

                if (parentObject.CompareTag("NPC"))
                    return true;

                if (Utilities.NameContains(
                    parentObject.name,
                    Config.NPCKeywords))
                    return true;

                current = current.parent;
            }

            return false;
        }
    }
}
