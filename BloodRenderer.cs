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

            float lifetime = Mathf.Lerp(
                0.35f,
                Config.BloodLifetime,
                strength
            );

            float speed = Mathf.Lerp(
                Config.BloodMinSpeed,
                Config.BloodMaxSpeed,
                strength
            );

            float size = Mathf.Lerp(
                Config.BloodMinSize,
                Config.BloodMaxSize,
                strength
            );

            main.startLifetime =
                new ParticleSystem.MinMaxCurve(lifetime);

            main.startSpeed =
                new ParticleSystem.MinMaxCurve(speed);

            main.startSize =
                new ParticleSystem.MinMaxCurve(size);

            int particleCount = Mathf.RoundToInt(
                Mathf.Lerp(
                    Config.BloodMinParticles,
                    Config.BloodMaxParticles,
                    strength
                )
            );

            main.maxParticles = particleCount;

            ParticleSystemRenderer renderer =
                blood.GetComponent<ParticleSystemRenderer>();

            if (renderer != null)
            {
                renderer.material =
                    MaterialManager.BloodMaterial;
            }

            particleSystem.Play();

            // Emit the particles directly instead of using
            // ParticleSystem.Burst / EmissionModule.SetBursts,
            // which are unavailable in the referenced API.
            particleSystem.Emit(particleCount);

            Destroy(
                blood,
                Config.BloodLifetime + 1f
            );
        }
    }
}
