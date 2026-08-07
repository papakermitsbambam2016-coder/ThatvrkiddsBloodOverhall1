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
            GameObject blood = new GameObject("ScratchBlood");

            blood.transform.position = point + normal * 0.01f;
            blood.transform.rotation = Quaternion.LookRotation(normal);

            ParticleSystem ps = blood.AddComponent<ParticleSystem>();

            var main = ps.main;
            main.loop = false;
            main.playOnAwake = false;
            main.simulationSpace = ParticleSystemSimulationSpace.World;

            main.startColor = Config.BloodColor;

            main.startLifetime = Mathf.Lerp(
                0.35f,
                Config.BloodLifetime,
                strength);

            main.startSpeed = Mathf.Lerp(
                Config.BloodMinSpeed,
                Config.BloodMaxSpeed,
                strength);

            main.startSize = Mathf.Lerp(
                Config.BloodMinSize,
                Config.BloodMaxSize,
                strength);

            main.maxParticles = Mathf.RoundToInt(
                Mathf.Lerp(
                    Config.BloodMinParticles,
                    Config.BloodMaxParticles,
                    strength));

            var emission = ps.emission;
            emission.rateOverTime = 0;

            emission.SetBursts(new[]
            {
                new ParticleSystem.Burst(
                    0f,
                    (short)Mathf.RoundToInt(
                        Mathf.Lerp(
                            Config.BloodMinParticles,
                            Config.BloodMaxParticles,
                            strength)))
            });

            ParticleSystemRenderer renderer =
                blood.GetComponent<ParticleSystemRenderer>();

            renderer.material = MaterialManager.BloodMaterial;

            ps.Play();

            Destroy(
                blood,
                Config.BloodLifetime + 1f);
        }
    }
}
Why this is better
Compared to the original version, this:

✅ Uses your shared MaterialManager

✅ Uses values from Config.cs

✅ Scales the effect based on slash strength

✅ Is easier to optimize later with ObjectPool

After this
We're finally ready to rewrite Main.cs one last time so it uses:

KnifeTracker

NPCDetector

ScratchRenderer

BloodRenderer

MaterialManager

instead of the old scratch and blood code.

Once that's done, we'll have the first complete version of ScratchLab v2, and from there we can add BoneMenu, saved settings, and extra injury effects without having to reorganize the whole project again.


