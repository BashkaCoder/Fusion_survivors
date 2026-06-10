using System;

namespace NetCode
{
    public static class NicknameSerializer
    {
        private const char Separator = ',';
        
        public static string[] DeserializeNicknames(string serializedString)
        {
            if (string.IsNullOrWhiteSpace(serializedString))
            {
                return Array.Empty<string>();
            }
            
            return serializedString.Split(Separator);
        }

        public static string SerializeNicknames(string[] nicknames)
        {
            return string.Join(Separator, nicknames);
        }
    }
}