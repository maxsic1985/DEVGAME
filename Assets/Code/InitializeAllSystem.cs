using LeoEcsPhysics;
using Leopotam.EcsLite;

namespace MSuhininTestovoe.Devgame
{
    internal class InitializeAllSystem
    {
        public InitializeAllSystem(EcsSystems systems, IPoolService poolService)
        {
            systems
                .Add(new InitializeServiceSystem(poolService))
                .Add(new LoadPrefabSystem())
                .Add(new LoadDataByNameSystem());

            new CommonSystems(systems);
            new DataManagerSystems(systems);
            new MapGeneratorSystems(systems);
           new TrapGeneratorSystems(systems);
           new TriggerSystems(systems);
            new PlayerSystems(systems);
           new BonusSystems(systems);
            new EnemySystems(systems);
            new MenuSystems(systems);
            new DeathSystems(systems);
          
            
           new CameraSystems(systems);
            new SoundSystems(systems);
        }
    }
}