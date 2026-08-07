using UnityEngine;

namespace ScratchLab
{
    internal static class MaterialManager
    {
        private static Material scratchMaterial;
        private static Material bloodMaterial;

        public static Material ScratchMaterial
        {
            get
            {
                if (scratchMaterial == null)
                {
                    Shader shader = Shader.Find("Sprites/Default");

                    scratchMaterial = new Material(shader);
                    scratchMaterial.color = Config.ScratchColor;
                }

                return scratchMaterial;
            }
        }

        public static Material BloodMaterial
        {
            get
            {
                if (bloodMaterial == null)
                {
                    Shader shader = Shader.Find("Sprites/Default");

                    bloodMaterial = new Material(shader);
                    bloodMaterial.color = Config.BloodColor;
                }

                return bloodMaterial;
            }
        }

        public static void Dispose()
        {
            if (scratchMaterial != null)
            {
                Object.Destroy(scratchMaterial);
                scratchMaterial = null;
            }

            if (bloodMaterial != null)
            {
                Object.Destroy(bloodMaterial);
                bloodMaterial = null;
            }
        }
    }
}
