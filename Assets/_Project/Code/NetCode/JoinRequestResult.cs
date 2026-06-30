namespace NetCode
{
    public enum JoinRequestResult
    {
        Accepted = 0,
        DeniedFullRoom = 1,
        DeniedNicknameDuplicate = 2,
        DeniedNicknameBanned = 3,
    }
}