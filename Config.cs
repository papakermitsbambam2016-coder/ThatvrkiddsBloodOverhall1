using UnityEngine;

namespace Thatvrkidds.ScratchLab
{
    /// <summary>
    /// ScratchLab configuration.
    /// All tweakable values live here.
    /// </summary>
    internal static class Config
    {
        // =========================
        // General
        // =========================

        public const bool ModEnabled = true;
        public const bool DebugMode = false;

        // =========================
        // Weapon Detection
        // =========================

        public const float PickupDelay = 0.50f;
        public const float ScanInterval = 1.25f;

        public const float MinimumSwipeDistance = 0.035f;
        public const float MinimumSwipeSpeed = 0.75f;
        public const float MaximumSwipeSpeed = 10.0f;

        public const float HitCooldown = 0.20f;

        // =========================
        // Damage
        // =========================

        public const float MinimumDamage = 1.0f;
        public const float MaximumDamage = 15.0f;

        // =========================
        // Scratch Marks
        // =========================

        public const int ScratchCountMin = 4;
        public const int ScratchCountMax = 7;

        public const float ScratchMinLength = 0.05f;
        public const float ScratchMaxLength = 0.16f;

        public const float ScratchMinWidth = 0.0035f;
        public const float ScratchMaxWidth = 0.0065f;

        public const float ScratchLifetime = 18f;

        // =========================
        // Blood
        // =========================

        public const int BloodMinParticles = 8;
        public const int BloodMaxParticles = 40;

        public const float BloodMinSize = 0.008f;
        public const float BloodMaxSize = 0.035f;

        public const float BloodMinSpeed = 0.30f;
        public const float BloodMaxSpeed = 2.30f;

        public const float BloodLifetime = 1.0f;

        // =========================
        // Audio
        // =========================

        public const float MinVolume = 0.15f;
        public const float MaxVolume = 0.40f;

        // =========================
        // Performance
        // =========================

        public const int MaxScratchMarks = 150;
        public const int MaxBloodEffects = 80;

        public const bool UseObjectPooling = true;

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

        public static readonly Color BloodColor =
            new Color(0.46f, 0.01f, 0.01f, 0.90f);

        public static readonly Color ScratchColor =
            new Color(0.20f, 0.01f, 0.01f, 0.95f);
    }
}
