using System;
using UnityEngine;
using MelonLoader;

namespace ScratchLab
{
    public class Main : MelonMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg(VersionInfo.FullName + " Loaded!");
        }

        public override void OnUpdate()
        {
            if (!Config.ModEnabled)
                return;

            // Gameplay systems will be updated here later.
        }
    }

    public class ScratchSystem : MonoBehaviour
    {
        public static ScratchSystem Instance;

        public GameObject scratchEffect;

        private void Awake()
        {
            Instance = this;
        }

        public void AddScratch(GameObject npc, Vector3 hitPosition, Vector3 hitNormal)
        {
            if (!Config.ModEnabled)
                return;

            if (npc == null)
                return;

            if (scratchEffect == null)
            {
                MelonLogger.Warning("[ScratchLab] Scratch Effect is not assigned.");
                return;
            }

            GameObject mark = Instantiate(
                scratchEffect,
                hitPosition,
                Quaternion.LookRotation(hitNormal)
            );

            mark.transform.SetParent(npc.transform, true);

            MelonLogger.Msg("[ScratchLab] Scratch added to " + npc.name);
        }
    }

    public class KnifeDamageDetector : MonoBehaviour
    {
        public bool isKnife = true;

        private float lastHitTime;

        private void OnCollisionEnter(Collision collision)
        {
            if (!Config.ModEnabled)
                return;

            if (!isKnife)
                return;

            if (Time.time - lastHitTime < Config.HitCooldown)
                return;

            GameObject hitObject = collision.gameObject;

            if (hitObject == null)
                return;

            if (!hitObject.CompareTag("NPC"))
                return;

            if (ScratchSystem.Instance == null)
                return;

            if (collision.contactCount == 0)
                return;

            ContactPoint hit = collision.contacts[0];

            ScratchSystem.Instance.AddScratch(
                hitObject,
                hit.point,
                hit.normal
            );

            lastHitTime = Time.time;
        }
    }
}
