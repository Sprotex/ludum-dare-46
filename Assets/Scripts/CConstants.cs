using UnityEngine;

public static class CConstants
{
    public static class Animator
    {
        public const string CrowEating = "CrowEating";
        public const string CrowWalk = "CrowWalk";
        public const string CrowFly = "CrowFly";
        public const string CrowStand = "CrowStand";
        public const string CrowAttackHit = "CrowAttackHit";

        public const string PlayerAttack = "Punch";
        public const string PlayerPickup = "Peck";
        public const string PlayerIsFlying = "IsFlying";
        public const string PlayerSpeed = "Speed";
    }

    public static class PPrefs
    {
        public static class Strings
        {
            public const string Tutorial = "Tutorial";
            public const string SoundVolume = "SoundVolume";
            public const string MusicVolume = "MusicVolume";
        }
        public static class DefaultValues
        {
            public const int Tutorial = 1;
            public const float SoundVolume = 0.7f;
            public const float MusicVolume = 0.7f;
        }
    }

    public static class Input
    {
        public const string Attack = "Attack";
        public const string Cancel = "Cancel";
        public const string MouseX = "Mouse X";
        public const string MouseY = "Mouse Y";
        public const string HorizontalAxis = "Horizontal";
        public const string VerticalAxis = "Vertical";
        public const string Fly = "Fly";
        public const string FeedChildren = "Feed Children";
        public const string FeedSelf = "Feed Self";
        public const string Pickup = "Pickup";
    }
}
