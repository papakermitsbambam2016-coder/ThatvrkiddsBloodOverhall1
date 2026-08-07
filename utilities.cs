using UnityEngine;

namespace Thatvrkidds.ScratchLab
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

        public static bool NameContains(string value, string[] keywords)
        {
            value = Normalize(value);

            foreach (string keyword in keywords)
            {
                if (value.Contains(keyword))
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

        public static Vector3 RandomDirection(Vector3 normal, float angle)
        {
            Quaternion rotation = Quaternion.AngleAxis(
                Random.Range(-angle, angle),
                normal);

            return rotation * Vector3.Cross(normal, Vector3.up);
        }

        public static Color Fade(Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }
    }
}
