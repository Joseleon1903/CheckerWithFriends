using Assets.Scripts.Json;
using System;

namespace Assets.Scripts.Utils
{
    public static class FacebookPermissions {
        public static string PublicProfile = "public_profile";
        public static string Email = "email";
        public static string UserFriends = "user_friends";
    }

    public enum GameType {
        CHECKER
    }

    public static class EnumHelper
    {
        public static string TRUE = "TRUE";
        public static string FALSE = "FALSE";

        public static T GetEnumValue<T>(string str) where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            T val;
            return Enum.TryParse<T>(str, true, out val) ? val : default(T);
        }

        public static T GetEnumValue<T>(int intValue) where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }

            return (T)Enum.ToObject(enumType, intValue);
        }

        public static string FindKeybyIdInDictionary(int intValue, AvatarJson[] array)
        {
            foreach (AvatarJson js in array) {

                if (js.avatarId == intValue) {

                    return js.avatarKey;
                }
            }
            return string.Empty;
        }

        public static string FindKeybyIdInDictionary(int intValue, FrameJson[] array)
        {
            foreach (FrameJson js in array)
            {

                if (js.frameId == intValue)
                {

                    return js.frameKey;
                }
            }
            return string.Empty;
        }
    }
}
