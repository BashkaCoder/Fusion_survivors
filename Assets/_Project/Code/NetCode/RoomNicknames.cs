using System.Collections.Generic;

namespace NetCode
{
    public class RoomNicknames
    {
        public HashSet<string> ALiveNicknames { get; private set; } = new();
        public HashSet<string> BannedNicknames { get; private set; } = new();
    }
}