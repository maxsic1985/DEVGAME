using Leopotam.EcsLite;

namespace MSuhininTestovoe.Devgame
{


    public sealed class TrapGeneratorSystems
    {
        public TrapGeneratorSystems(EcsSystems systems)
        {
            systems
                .Add(new TrapGeneratorLoadSystem())
                .Add(new TrapGeneratorInitSystem())
                .Add(new TrapGeneratorBuildSystem());
        }
    }
}