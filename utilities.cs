using UnityEngine;

namespace ScratchLab
{
    internal static class Utilities
    {
        public static string Normalize(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value
                .ToLowerInvariant()
                .Replace(" ", "")
                .Replace("_", "")
                .Replace("-", "");
        }

        public static bool NameContains(
            string value,
            string[] keywords)
        {
            value = Normalize(value);

            if (string.IsNullOrEmpty(value) || keywords == null)
                return false;

            foreach (string keyword in keywords)
            {
                if (string.IsNullOrEmpty(keyword))
                    continue;

                if (value.Contains(
                    Normalize(keyword)))
                    return true;
            }

            return false;
        }

        public static float StrengthFromSpeed(float speed)
        {
            return Mathf.InverseLerp(
                Config.MinimumSwipeSpeed,
                Config.MaximumSwipeSpeed,
                speed);
        }

        public static Vector3 RandomDirection(
            Vector3 normal,
            float angle)
        {
            if (normal.sqrMagnitude < 0.0001f)
                normal = Vector3.up;

            normal.Normalize();

            Vector3 tangent =
                Vector3.Cross(normal, Vector3.up);

            if (tangent.sqrMagnitude < 0.0001f)
                tangent =
                    Vector3.Cross(normal, Vector3.right);

            tangent.Normalize();

            Quaternion rotation =
                Quaternion.AngleAxis(
                    Random.Range(-angle, angle),
                    normal);

            return rotation * tangent;
        }

        public static Color Fade(
            Color color,
            float alpha)
        {
            color.a = alpha;
            return color;
        }
    }
}
