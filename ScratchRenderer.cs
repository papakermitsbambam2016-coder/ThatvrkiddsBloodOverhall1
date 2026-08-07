using UnityEngine;

namespace ScratchLab
{
    public class ScratchRenderer : MonoBehaviour
    {
        public static ScratchRenderer Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Object.Destroy(this);
                return;
            }

            Instance = this;
            MelonLogger.Msg("[ScratchLab] ScratchRenderer initialized.");
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        public void SpawnScratch(
            Transform target,
            Vector3 point,
            Vector3 normal,
            float strength)
        {
            if (target == null)
                return;

            if (normal.sqrMagnitude < 0.0001f)
                normal = Vector3.up;

            normal.Normalize();

            int count = Random.Range(
                Config.ScratchCountMin,
                Config.ScratchCountMax + 1);

            for (int i = 0; i < count; i++)
            {
                GameObject scratch = new GameObject("ScratchMark");

                scratch.transform.position =
                    point + normal * Config.ScratchSurfaceOffset;

                scratch.transform.rotation =
                    Quaternion.LookRotation(normal);

                scratch.transform.SetParent(target, true);

                LineRenderer line =
                    scratch.AddComponent<LineRenderer>();

                line.useWorldSpace = false;
                line.positionCount = 2;
                line.loop = false;
                line.alignment = LineAlignment.TransformZ;
                line.textureMode = LineTextureMode.Stretch;
                line.numCapVertices = 4;
                line.numCornerVertices = 2;

                float width = Random.Range(
                    Config.ScratchMinWidth,
                    Config.ScratchMaxWidth);

                line.startWidth = width;
                line.endWidth = width * 0.45f;

                line.material = MaterialManager.ScratchMaterial;

                float length = Random.Range(
                    Config.ScratchMinLength,
                    Config.ScratchMaxLength);

                float angle = Random.Range(
                    -Config.ScratchAngle,
                    Config.ScratchAngle);

                Quaternion localRotation =
                    Quaternion.Euler(0f, 0f, angle);

                Vector3 direction =
                    localRotation * Vector3.right;

                float offset = Random.Range(
                    -Config.ScratchSpread,
                    Config.ScratchSpread);

                Vector3 sideOffset =
                    Vector3.up * offset;

                line.SetPosition(
                    0,
                    sideOffset - direction * (length * 0.5f));

                line.SetPosition(
                    1,
                    sideOffset + direction * (length * 0.5f));

                ScratchFade fade =
                    scratch.AddComponent<ScratchFade>();

                fade.line = line;
                fade.lifetime =
                    Random.Range(
                        Config.ScratchLifetime * 0.75f,
                        Config.ScratchLifetime);
            }
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
                Object.Destroy(gameObject);
                return;
            }

            timer += Time.deltaTime;

            float t = lifetime <= 0f
                ? 1f
                : Mathf.Clamp01(timer / lifetime);

            Color color = Config.ScratchColor;
            color.a = Mathf.Lerp(1f, 0f, t);

            line.startColor = color;
            line.endColor = color;

            if (timer >= lifetime)
                Object.Destroy(gameObject);
        }
    }
}
