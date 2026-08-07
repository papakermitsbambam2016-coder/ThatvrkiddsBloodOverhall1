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
                    Shader shader =
                        Shader.Find("Unlit/Color");

                    if (shader == null)
                        shader = Shader.Find("Sprites/Default");

                    if (shader == null)
                    {
                        Debug.LogWarning(
                            "[ScratchLab] Could not find a usable scratch shader.");
                        return null;
                    }

                    scratchMaterial = new Material(shader);
                    scratchMaterial.name = "ScratchLab_ScratchMaterial";
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
                    Shader shader =
                        Shader.Find("Particles/Standard Unlit");

                    if (shader == null)
                        shader = Shader.Find("Sprites/Default");

                    if (shader == null)
                        return null;

                    bloodMaterial = new Material(shader);
                    bloodMaterial.name = "ScratchLab_BloodMaterial";
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
