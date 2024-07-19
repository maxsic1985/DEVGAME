namespace MSuhininTestovoe.Devgame
{
    public interface IActor
    {
        int Entity { get; }
        void Handle();
        void AddEntity(int entity);
    }
}