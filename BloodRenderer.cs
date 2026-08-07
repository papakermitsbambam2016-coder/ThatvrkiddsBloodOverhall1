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

            ParticleSystem ps = blood.AddComponent<ParticleSystem>();

            ps.loop = false;
            ps.playOnAwake = false;

            int particleCount = Mathf.RoundToInt(
                Mathf.Lerp(
                    Config.BloodMinParticles,
                    Config.BloodMaxParticles,
                    strength));

            particleCount = Mathf.Max(1, particleCount);

            ParticleSystemRenderer renderer =
                blood.GetComponent<ParticleSystemRenderer>();

            if (renderer != null)
            {
                renderer.material = MaterialManager.BloodMaterial;
            }

            ps.Emit(particleCount);

            ParticleSystem.Particle[] particles =
                new ParticleSystem.Particle[particleCount];

            int count = ps.GetParticles(particles);

            float lifetime = Mathf.Lerp(
                0.35f,
                Config.BloodLifetime,
                strength);

            float speed = Mathf.Lerp(
                Config.BloodMinSpeed,
                Config.BloodMaxSpeed,
                strength);

            float size = Mathf.Lerp(
                Config.BloodMinSize,
                Config.BloodMaxSize,
                strength);

            for (int i = 0; i < count; i++)
            {
                particles[i].remainingLifetime = lifetime;
                particles[i].startLifetime = lifetime;
                particles[i].startSize = size;

                Vector3 direction =
                    normal +
                    Random.insideUnitSphere * 0.35f;

                if (direction.sqrMagnitude > 0.001f)
                    direction.Normalize();

                particles[i].velocity =
                    direction * speed;

                particles[i].startColor =
                    Config.BloodColor;
            }

            ps.SetParticles(particles, count);

            Destroy(
                blood,
                Config.BloodLifetime + 1f);
        }
    }
}
