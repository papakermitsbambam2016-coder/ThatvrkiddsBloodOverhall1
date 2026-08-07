using System;
using UnityEngine;
using MelonLoader;

namespace ThatvrkiddsBloodOverhall
{
    public class Main : MelonMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("ThatvrkiddsBloodOverhall Loaded!");
        }

        public override void OnUpdate()
        {
            // Main update loop
            // Damage detection hooks can be added here depending on SDK version
        }
    }

    public class ScratchSystem : MonoBehaviour
    {
        public static ScratchSystem Instance;

        public GameObject scratchEffect;

        void Awake()
        {
            Instance = this;
        }

        public void AddScratch(GameObject npc, Vector3 hitPosition, Vector3 hitNormal)
        {
            if (scratchEffect == null)
            {
                MelonLogger.Warning("Scratch effect not assigned!");
                return;
            }

            // Create scratch mark on NPC
            GameObject mark = Instantiate(
                scratchEffect,
                hitPosition,
                Quaternion.LookRotation(hitNormal)
            );

            mark.transform.SetParent(npc.transform);

            MelonLogger.Msg(
                "Scratch added to NPC: " + npc.ford
            );
        }
    }

    public class KnifeDamageDetector : MonoBehaviour
    {
        public bool isKnife;

        private void OnCollisionEnter(Collision collision)
        {
            if (!isKnife)
                return;

            GameObject hitObject = collision.gameObject;

            // Check if object is an NPC
            if (hitObject.CompareTag("NPC"))
            {
                ContactPoint hit = collision.contacts[0];

                ScratchSystem.Instance.AddScratch(
                    hitObject,
                    hit.point,
                    hit.normal
                );
            }
        }
    }
}
