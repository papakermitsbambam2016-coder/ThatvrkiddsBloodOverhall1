using UnityEngine;

namespace ScratchLab
{
    internal static class NPCDetector
    {
        public static bool IsNPC(GameObject obj)
        {
            if (obj == null)
                return false;

            // Fast tag check
            if (obj.CompareTag("NPC"))
                return true;

            // Check object name
            if (Utilities.NameContains(obj.name, Config.NPCKeywords))
                return true;

            // Check parent names
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
with:

if (!NPCDetector.IsNPC(hitObject))
    return;
Before we continue
I have one question:

How are you planning to compile this?

Earlier you mentioned you don't have a PC, and we've now added several new .cs files. I want to make sure the workflow we're using matches how you'll eventually build the DLL. That will help me avoid having you create files that can't be compiled in your current setup.

