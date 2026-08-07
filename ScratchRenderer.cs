using UnityEngine;
using System.Collections;

namespace ScratchLab
{
    public class ScratchRenderer : MonoBehaviour
    {
        public static ScratchRenderer Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void SpawnScratch(
            Transform target,
            Vector3 point,
            Vector3 normal,
            float strength)
        {
            int count = Random.Range(
                Config.ScratchCountMin,
                Config.ScratchCountMax + 1);

            Vector3 tangent = Vector3.Cross(normal, Vector3.up);

            if (tangent.sqrMagnitude < 0.01f)
                tangent = Vector3.Cross(normal, Vector3.right);

            tangent.Normalize();

            Vector3 across = Vector3.Cross(normal, tangent);

            for (int i = 0; i < count; i++)
            {
                GameObject scratch = new GameObject("ScratchMark");

                scratch.transform.SetParent(target, true);
                scratch.transform.position = point;

                LineRenderer line =
                    scratch.AddComponent<LineRenderer>();

                line.useWorldSpace = false;

                line.positionCount = 2;

                float width =
                    Random.Range(
                        Config.ScratchMinWidth,
                        Config.ScratchMaxWidth);

                line.startWidth = width;
                line.endWidth = width * 0.55f;

                line.numCapVertices = 8;
                line.numCornerVertices = 8;

                line.material = new Material(
                    Shader.Find("Sprites/Default"));

                line.material.color =
                    Config.ScratchColor;

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

                StartCoroutine(
                    FadeScratch(
                        line,
                        scratch));
            }
        }

        private IEnumerator FadeScratch(
            LineRenderer line,
            GameObject obj)
        {
            Color color = Config.ScratchColor;

            float timer = 0f;

            while (timer < Config.ScratchLifetime)
            {
                timer += Time.deltaTime;

                float alpha =
                    Mathf.Lerp(
                        1f,
                        0f,
                        timer /
                        Config.ScratchLifetime);

                color.a = alpha;

                line.startColor = color;
                line.endColor = color;

                yield return null;
            }

            Destroy(obj);
        }
    }
}
