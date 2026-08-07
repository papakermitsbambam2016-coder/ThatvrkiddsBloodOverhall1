using System.Collections.Generic;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(ScratchLab.Main), "ScratchLab", "1.1.0", "Thatvrkidds")]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]

namespace ScratchLab
{
    public class Main : MelonMod
    {
        private static readonly HashSet<int> scannedRoots = new HashSet<int>();
        private float nextScanTime;

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg(VersionInfo.FullName + " Loaded!");

            GameObject manager = GameObject.Find("ScratchLab");

            if (manager == null)
            {
                manager = new GameObject("ScratchLab");
                Object.DontDestroyOnLoad(manager);
            }

            if (manager.GetComponent<ScratchSystem>() == null)
                manager.AddComponent<ScratchSystem>();

            if (manager.GetComponent<ScratchRenderer>() == null)
                manager.AddComponent<ScratchRenderer>();

            if (manager.GetComponent<BloodRenderer>() == null)
                manager.AddComponent<BloodRenderer>();

            MelonLogger.Msg("[ScratchLab] Systems initialized.");
            MelonLogger.Msg("[ScratchLab] Knife scanner starting.");
        }

        public override void OnUpdate()
        {
            if (!Config.ModEnabled)
                return;

            if (Time.time < nextScanTime)
                return;

            nextScanTime = Time.time + Config.ScanInterval;
            ScanForKnives();
        }

        private void ScanForKnives()
        {
            GameObject[] objects = Object.FindObjectsOfType<GameObject>();
            int found = 0;

            foreach (GameObject obj in objects)
            {
                if (obj == null)
                    continue;

                if (!LooksLikeKnifeHierarchy(obj))
                    continue;

                GameObject root = GetWeaponRoot(obj);

                if (root == null)
                    root = obj;

                int id = root.GetInstanceID();

                if (scannedRoots.Contains(id))
                    continue;

                scannedRoots.Add(id);
                AttachKnifeComponents(root);
                found++;
            }

            if (found > 0)
                MelonLogger.Msg("[ScratchLab] Initialized " + found + " knife root(s).");
        }

        private static bool LooksLikeKnifeHierarchy(GameObject obj)
        {
            if (obj == null)
                return false;

            if (Utilities.NameContains(obj.name, Config.WeaponKeywords))
                return true;

            Transform child = obj.transform;

            while (child != null)
            {
                if (Utilities.NameContains(child.name, Config.WeaponKeywords))
                    return true;

                child = child.parent;
            }

            return false;
        }

        private static GameObject GetWeaponRoot(GameObject obj)
        {
            if (obj == null)
                return null;

            Rigidbody body = obj.GetComponentInParent<Rigidbody>();

            if (body != null)
                return body.gameObject;

            return obj.transform.root != null
                ? obj.transform.root.gameObject
                : obj;
        }

        private static void AttachKnifeComponents(GameObject root)
        {
            KnifeTracker tracker = root.GetComponent<KnifeTracker>();

            if (tracker == null)
            {
                tracker = root.AddComponent<KnifeTracker>();
                MelonLogger.Msg("[ScratchLab] Added KnifeTracker to " + root.name);
            }

            Collider[] colliders = root.GetComponentsInChildren<Collider>(true);
            int detectorsAdded = 0;

            foreach (Collider collider in colliders)
            {
                if (collider == null)
                    continue;

                KnifeDamageDetector detector =
                    collider.gameObject.GetComponent<KnifeDamageDetector>();

                if (detector == null)
                {
                    detector = collider.gameObject.AddComponent<KnifeDamageDetector>();
                    detectorsAdded++;
                }

                detector.SetTracker(tracker);
                detector.isKnife = true;
            }

            if (detectorsAdded > 0)
            {
                MelonLogger.Msg(
                    "[ScratchLab] Added " + detectorsAdded +
                    " collision detector(s) to " + root.name);
            }
        }
    }

    public class ScratchSystem : MonoBehaviour
    {
        public static ScratchSystem Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Object.Destroy(this);
                return;
            }

            Instance = this;
            MelonLogger.Msg("[ScratchLab] ScratchSystem initialized.");
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        public void AddScratch(
            GameObject npc,
            Vector3 hitPosition,
            Vector3 hitNormal,
            float strength)
        {
            if (!Config.ModEnabled || npc == null)
                return;

            ScratchRenderer renderer = ScratchRenderer.Instance;

            if (renderer == null)
            {
                MelonLogger.Warning(
                    "[ScratchLab] AddScratch failed: ScratchRenderer is null.");
                return;
            }

            renderer.SpawnScratch(
                npc.transform,
                hitPosition,
                hitNormal,
                Mathf.Clamp01(strength));

            MelonLogger.Msg(
                "[ScratchLab] Scratch added to " + npc.name);
        }
    }

    public class KnifeDamageDetector : MonoBehaviour
    {
        public bool isKnife = true;

        private KnifeTracker tracker;
        private float lastHitTime = -999f;

        public void SetTracker(KnifeTracker value)
        {
            tracker = value;
        }

        private void Awake()
        {
            if (tracker == null)
                tracker = GetComponentInParent<KnifeTracker>();

            if (tracker == null)
                tracker = GetComponent<KnifeTracker>();

            if (tracker == null)
                tracker = gameObject.AddComponent<KnifeTracker>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!Config.ModEnabled || !isKnife)
                return;

            if (tracker == null)
                tracker = GetComponentInParent<KnifeTracker>();

            if (tracker == null)
                return;

            if (!tracker.CanScratch())
                return;

            if (Time.time - lastHitTime < Config.HitCooldown)
                return;

            if (collision == null || collision.contactCount <= 0)
                return;

            GameObject hitObject = collision.gameObject;

            if (hitObject == null)
                return;

            GameObject npcObject = FindNPCRoot(hitObject);

            if (npcObject == null)
                return;

            ContactPoint hit = collision.contacts[0];

            if (ScratchSystem.Instance == null)
            {
                MelonLogger.Warning(
                    "[ScratchLab] Collision reached NPC, but ScratchSystem.Instance is null.");
                return;
            }

            float strength = tracker.Strength();

            ScratchSystem.Instance.AddScratch(
                npcObject,
                hit.point,
                hit.normal,
                strength);

            lastHitTime = Time.time;
        }

        private static GameObject FindNPCRoot(GameObject hitObject)
        {
            if (hitObject == null)
                return null;

            if (NPCDetector.IsNPC(hitObject))
                return hitObject;

            Transform parent = hitObject.transform.parent;

            while (parent != null)
            {
                GameObject candidate = parent.gameObject;

                if (NPCDetector.IsNPC(candidate))
                    return candidate;

                parent = parent.parent;
            }

            return null;
        }
    }
}
