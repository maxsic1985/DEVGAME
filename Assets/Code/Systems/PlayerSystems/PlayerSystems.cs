using Leopotam.EcsLite;



namespace MSuhininTestovoe.Devgame
{
    public class PlayerSystems
    {
        public PlayerSystems(EcsSystems systems)
        {
            systems
                .Add(new PlayerLoadSystem())
                .Add(new PlayerInitSystem())
                .Add(new PlayerBuildSystem())
                .Add(new PlayerInputSystem())
                .Add(new PlayerRotateSystem())
                .Add(new PlayerRayCastSystem())
                .Add(new PlayerAtackSystem())
                .Add(new PlayerMoveSystem());

        }
    }
}