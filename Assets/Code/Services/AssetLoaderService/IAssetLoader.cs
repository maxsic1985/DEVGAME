namespace MSuhininTestovoe.Devgame
{
    public interface IAssetLoader
    {
        void LoadAsset(object address, Leopotam.EcsLite.EcsWorld ecsWorld, int entity);
    }
}
