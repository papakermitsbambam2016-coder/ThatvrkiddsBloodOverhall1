using UnityEngine;

namespace ScratchLab
{
    public class BloodRenderer : MonoBehaviour
    {
        public static BloodRenderer Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void SpawnBlood(Vector3 point, Vector3 normal, float strength)
        {
            strength = Mathf.Clamp01(strength);

            GameObject blood = new GameObject("ScratchBlood");

            blood.transform.position = point + normal * 0.01f;
            blood.transform.rotation = Quaternion.LookRotation(normal);

            ParticleSystem particleSystem =
                blood.AddComponent<ParticleSystem>();

            ParticleSystem.MainModule main =
                particleSystem.main;

            main.loop = false;
            main.playOnAwake = false;
            main.simulationSpace =
                ParticleSystemSimulationSpace.World;

            main.startColor = Config.BloodColor;

            main.startLifetime = Mathf.Lerp(
                0.35f,
                Config.BloodLifetime,
                strength
            );

            main.startSpeed = Mathf.Lerp(
                Config.BloodMinSpeed,
                Config.BloodMaxSpeed,
                strength
            );

            main.startSize = Mathf.Lerp(
                Config.BloodMinSize,
                Config.BloodMaxSize,
                strength
            );

            int particleCount = Mathf.RoundToInt(
                Mathf.Lerp(
                    Config.BloodMinParticles,
                    Config.BloodMaxParticles,
                    strength
                )
            );

            main.maxParticles = particleCount;

            ParticleSystem.EmissionModule emission =
                particleSystem.emission;

            emission.rateOverTime = 0;

            ParticleSystem.Burst burst =
                new ParticleSystem.Burst(
                    0f,
                    (short)particleCount
                );

            emission.SetBursts(new ParticleSystem.Burst[]
            {
                burst
            });

            ParticleSystemRenderer renderer =
                blood.GetComponent<ParticleSystemRenderer>();

            if (renderer != null)
            {
                renderer.material =
                    MaterialManager.BloodMaterial;
            }

            particleSystem.Play();

            Destroy(
                blood,
                Config.BloodLifetime + 1f
            );
        }
    }
}

