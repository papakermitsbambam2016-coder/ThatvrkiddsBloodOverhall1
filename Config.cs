
using UnityEngine;

namespace ScratchLab
{
    internal static class Config
    {
        // =========================
        // General
        // =========================

        public static bool ModEnabled = true;
        public static bool DebugMode = false;

        // =========================
        // Weapon Detection
        // =========================

        public static float PickupDelay = 0.50f;
        public static float ScanInterval = 1.25f;

        public static float MinimumSwipeDistance = 0.035f;
        public static float MinimumSwipeSpeed = 0.75f;
        public static float MaximumSwipeSpeed = 10.0f;

        public static float HitCooldown = 0.20f;

        // =========================
        // Damage
        // =========================

        public static float MinimumDamage = 1.0f;
        public static float MaximumDamage = 15.0f;

        // =========================
        // Scratch Marks
        // =========================

        public static int ScratchCountMin = 4;
        public static int ScratchCountMax = 7;

        public static float ScratchMinLength = 0.05f;
        public static float ScratchMaxLength = 0.16f;

        public static float ScratchMinWidth = 0.0035f;
        public static float ScratchMaxWidth = 0.0065f;

        public static float ScratchLifetime = 18f;

        // =========================
        // Blood
        // =========================

        public static int BloodMinParticles = 8;
        public static int BloodMaxParticles = 40;

        public static float BloodMinSize = 0.008f;
        public static float BloodMaxSize = 0.035f;

        public static float BloodMinSpeed = 0.30f;
        public static float BloodMaxSpeed = 2.30f;

        public static float BloodLifetime = 1.0f;

        // =========================
        // Audio
        // =========================

        public static float MinVolume = 0.15f;
        public static float MaxVolume = 0.40f;

        // =========================
        // Performance
        // =========================

        public static int MaxScratchMarks = 150;
        public static int MaxBloodEffects = 80;

        public static bool UseObjectPooling = true;

        // =========================
        // Supported Weapons
        // =========================

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

        // =========================
        // NPC Detection
        // =========================

        public static readonly string[] NPCKeywords =
        {
            "ford",
            "nullbody",
            "securitygourd",
            "agent",
            "peasant"
        };

        // =========================
        // Colors
        // =========================

        public static Color BloodColor =
            new Color(0.46f, 0.01f, 0.01f, 0.90f);

        public static Color ScratchColor =
            new Color(0.20f, 0.01f, 0.01f, 0.95f);
    }
}
