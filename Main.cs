using System.Collections.Generic;
using UnityEngine;
using MelonLoader;

namespace ScratchLab
{
    public class Main : MelonMod
    {
        private static readonly HashSet<int> scannedObjects =
            new HashSet<int>();

        private float nextScanTime;

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg(
                VersionInfo.FullName + " Loaded!");

            GameObject manager =
                new GameObject("ScratchLab");

            Object.DontDestroyOnLoad(manager);

            if (ScratchSystem.Instance == null)
                manager.AddComponent<ScratchSystem>();

            if (ScratchRenderer.Instance == null)
                manager.AddComponent<ScratchRenderer>();

            if (BloodRenderer.Instance == null)
                manager.AddComponent<BloodRenderer>();

            MelonLogger.Msg(
                "[ScratchLab] Systems initialized.");
        }

        public override void OnUpdate()
        {
            if (!Config.ModEnabled)
                return;

            if (Time.time < nextScanTime)
                return;

            nextScanTime = Time.time + 2f;

            ScanForKnives();
        }

        private void ScanForKnives()
        {
            GameObject[] objects =
                Object.FindObjectsOfType<GameObject>();

            int found = 0;

            foreach (GameObject obj in objects)
            {
                if (obj == null)
                    continue;

                int id = obj.GetInstanceID();

                if (scannedObjects.Contains(id))
                    continue;

                if (!LooksLikeKnife(obj))
                    continue;

                scannedObjects.Add(id);

                AttachKnifeComponents(obj);

                found++;
            }

            if (found > 0)
            {
                MelonLogger.Msg(
                    "[ScratchLab] Found and initialized " +
                    found +
                    " possible knife object(s).");
            }
        }

        private static bool LooksLikeKnife(GameObject obj)
        {
            string name =
                obj.name.ToLowerInvariant();

            return
                name.Contains("knife") ||
                name.Contains("dagger") ||
                name.Contains("blade") ||
                name.Contains("sword") ||
                name.Contains("shiv");
        }

        private static void AttachKnifeComponents(
            GameObject obj)
        {
            KnifeTracker tracker =
                obj.GetComponent<KnifeTracker>();

            if (tracker == null)
            {
                tracker =
                    obj.AddComponent<KnifeTracker>();

                MelonLogger.Msg(
                    "[ScratchLab] Added KnifeTracker to " +
                    obj.name);
            }

            KnifeDamageDetector detector =
                obj.GetComponent<KnifeDamageDetector>();

            if (detector == null)
            {
                detector =
                    obj.AddComponent<KnifeDamageDetector>();

                MelonLogger.Msg(
                    "[ScratchLab] Added KnifeDamageDetector to " +
                    obj.name);
            }

            detector.isKnife = true;
        }
    }

    public class ScratchSystem : MonoBehaviour
    {
        public static ScratchSystem Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void AddScratch(
            GameObject npc,
            Vector3 hitPosition,
            Vector3 hitNormal)
        {
            if (!Config.ModEnabled)
                return;

            if (npc == null)
                return;

            ScratchRenderer renderer =
                ScratchRenderer.Instance;

            if (renderer == null)
            {
                MelonLogger.Warning(
                    "[ScratchLab] ScratchRenderer is not initialized.");

                return;
            }

            renderer.SpawnScratch(
                npc.transform,
                hitPosition,
                hitNormal,
                1f);

            MelonLogger.Msg(
                "[ScratchLab] Scratch added to " +
                npc.name);
        }
    }

    public class KnifeDamageDetector : MonoBehaviour
    {
        public bool isKnife = true;

        private KnifeTracker tracker;
        private float lastHitTime;

        private void Awake()
        {
            tracker =
                GetComponent<KnifeTracker>();

            if (tracker == null)
            {
                tracker =
                    gameObject.AddComponent<KnifeTracker>();
            }
        }

        private void OnCollisionEnter(
            Collision collision)
        {
            if (!Config.ModEnabled)
                return;

            if (!isKnife)
                return;

            if (tracker == null)
                return;

            if (!tracker.CanScratch())
                return;

            if (Time.time - lastHitTime <
                Config.HitCooldown)
                return;

            if (collision == null)
                return;

            if (collision.contactCount <= 0)
                return;

            GameObject hitObject =
                collision.gameObject;

            if (hitObject == null)
                return;

            if (!NPCDetector.IsNPC(hitObject))
                return;

            ContactPoint hit =
                collision.contacts[0];

            if (ScratchSystem.Instance == null)
                return;

            ScratchSystem.Instance.AddScratch(
                hitObject,
                hit.point,
                hit.normal);

            lastHitTime = Time.time;
        }
    }
}
