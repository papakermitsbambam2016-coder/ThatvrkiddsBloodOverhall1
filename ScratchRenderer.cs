using UnityEngine;
using MelonLoader;

namespace ScratchLab
{
    public class ScratchRenderer : MonoBehaviour
    {
        public static ScratchRenderer Instance;

        private void Awake()
        {
            Instance = this;

            MelonLogger.Msg("[ScratchLab] ScratchRenderer initialized.");
        }

        public void SpawnScratch(
            Transform target,
            Vector3 point,
            Vector3 normal,
            float strength)
        {
            if (target == null)
            {
                MelonLogger.Warning(
                    "[ScratchLab] SpawnScratch failed: target is null.");

                return;
            }

            if (normal.sqrMagnitude < 0.001f)
            {
                MelonLogger.Warning(
                    "[ScratchLab] SpawnScratch failed: invalid hit normal.");

                return;
            }

            normal.Normalize();

            int count = Random.Range(
                Config.ScratchCountMin,
                Config.ScratchCountMax + 1);

            Vector3 tangent =
                Vector3.Cross(normal, Vector3.up);

            if (tangent.sqrMagnitude < 0.01f)
            {
                tangent =
                    Vector3.Cross(normal, Vector3.right);
            }

            tangent.Normalize();

            Vector3 across =
                Vector3.Cross(normal, tangent);

            across.Normalize();

            for (int i = 0; i < count; i++)
            {
                GameObject scratch =
                    new GameObject("ScratchMark");

                scratch.transform.SetParent(target, true);

                // Put the scratch slightly above the surface
                // to prevent it from being hidden by the NPC.
                scratch.transform.position =
                    point + normal * 0.0015f;

                LineRenderer line =
                    scratch.AddComponent<LineRenderer>();

                line.useWorldSpace = false;
                line.positionCount = 2;
                line.loop = false;

                float width =
                    Random.Range(
                        Config.ScratchMinWidth,
                        Config.ScratchMaxWidth);

                line.startWidth = width;
                line.endWidth = width * 0.55f;

                line.numCapVertices = 8;
                line.numCornerVertices = 8;

                Shader shader =
                    Shader.Find("Sprites/Default");

                if (shader == null)
                {
                    MelonLogger.Warning(
                        "[ScratchLab] Could not find Sprites/Default shader.");

                    Destroy(scratch);
                    continue;
                }

                Material material =
                    new Material(shader);

                material.color =
                    Config.ScratchColor;

                line.material = material;

                float length =
                    Random.Range(
                        Config.ScratchMinLength,
                        Config.ScratchMaxLength);

                float angle =
                    Random.Range(-15f, 15f);

                Quaternion rotation =
                    Quaternion.AngleAxis(
                        angle,
                        normal);

                Vector3 direction =
                    rotation * tangent;

                direction.Normalize();

                Vector3 offset =
                    across *
                    Random.Range(-0.02f, 0.02f);

                Vector3 start =
                    offset -
                    direction *
                    (length * 0.5f);

                Vector3 end =
                    offset +
                    direction *
                    (length * 0.5f);

                line.SetPosition(0, start);
                line.SetPosition(1, end);

                ScratchFade fade =
                    scratch.AddComponent<ScratchFade>();

                fade.line = line;
                fade.lifetime =
                    Config.ScratchLifetime;
            }

            MelonLogger.Msg(
                "[ScratchLab] Spawned " +
                count +
                " scratch mark(s).");
        }
    }

    public class ScratchFade : MonoBehaviour
    {
        public LineRenderer line;
        public float lifetime;

        private float timer;

        private void Update()
        {
            if (line == null)
            {
                Destroy(gameObject);
                return;
            }

            timer += Time.deltaTime;

            float progress =
                Mathf.Clamp01(timer / lifetime);

            float alpha =
                Mathf.Lerp(
                    1f,
                    0f,
                    progress);

            Color color =
                Config.ScratchColor;

            color.a = alpha;

            line.startColor = color;
            line.endColor = color;

            if (timer >= lifetime)
            {
                if (line.material != null)
                {
                    Destroy(line.material);
                }

                Destroy(gameObject);
            }
        }
    }
}
