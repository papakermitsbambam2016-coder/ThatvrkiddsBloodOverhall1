using UnityEngine;

namespace ScratchLab
{
    internal static class Config
    {
        public static bool ModEnabled = true;
        public static bool DebugMode = true;

        public static float PickupDelay = 0.20f;
        public static float ScanInterval = 1.00f;

        public static float MinimumSwipeDistance = 0.035f;
        public static float MinimumSwipeSpeed = 0.35f;
        public static float MaximumSwipeSpeed = 12.0f;

        public static float HitCooldown = 0.15f;

        public static float MinimumDamage = 1.0f;
        public static float MaximumDamage = 15.0f;

        public static int ScratchCountMin = 2;
        public static int ScratchCountMax = 4;

        public static float ScratchMinLength = 0.045f;
        public static float ScratchMaxLength = 0.13f;

        public static float ScratchMinWidth = 0.0012f;
        public static float ScratchMaxWidth = 0.0028f;

        public static float ScratchLifetime = 18f;
        public static float ScratchSurfaceOffset = 0.003f;
        public static float ScratchSpread = 0.012f;
        public static float ScratchAngle = 12f;

        public static int BloodMinParticles = 8;
        public static int BloodMaxParticles = 40;

        public static float BloodMinSize = 0.008f;
        public static float BloodMaxSize = 0.035f;

        public static float BloodMinSpeed = 0.30f;
        public static float BloodMaxSpeed = 2.30f;

        public static float BloodLifetime = 1.0f;

        public static float MinVolume = 0.15f;
        public static float MaxVolume = 0.40f;

        public static int MaxScratchMarks = 150;
        public static int MaxBloodEffects = 80;

        public static bool UseObjectPooling = false;

        public static readonly string[] WeaponKeywords =
        {
            "knife",
            "blade",
            "dagger",
            "katana",
            "machete",
            "axe",
            "hatchet",
            "claw",
            "shiv",
            "scalpel",
            "sword",
            "bayonet",
            "kukri",
            "cleaver"
        };

        public static readonly string[] NPCKeywords =
        {
            "ford",
            "nullbody",
            "securitygourd",
            "agent",
            "peasant"
        };

        public static Color BloodColor =
            new Color(0.46f, 0.01f, 0.01f, 0.90f);

        public static Color ScratchColor =
            new Color(0.20f, 0.01f, 0.01f, 0.95f);
    }
}
