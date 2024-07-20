using Leopotam.EcsLite;



namespace MSuhininTestovoe.Devgame
{
    public class EnemySystems
    {
        public EnemySystems(EcsSystems systems)
        {
            systems
                .Add(new EnemyLoadSystem())
                .Add(new EnemyInitSystem())
                .Add(new EnemyAtackSystem())
                .Add(new EnemyCastSystem())
                .Add(new EnemyDeathSystem());

        }
    }
}