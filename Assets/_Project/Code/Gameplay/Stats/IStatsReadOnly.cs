namespace Gameplay.Stats
{
    public interface IStatsReadOnly
    {
        float Get(StatId stat);
        event System.Action<StatId> Changed;
    }
}