using Leopotam.EcsLite;

namespace MSuhininTestovoe.Devgame
{


    public sealed class MapGeneratorSystems
    {
        public MapGeneratorSystems(EcsSystems systems)
        {
            systems
                .Add(new MapGeneratorLoadSystem())
                .Add(new MapGeneratorInitSystem())
                .Add(new MapGeneratorBuildSystem());
        }
    }
}