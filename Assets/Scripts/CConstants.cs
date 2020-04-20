using UnityEngine;

public static class CConstants
{
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
