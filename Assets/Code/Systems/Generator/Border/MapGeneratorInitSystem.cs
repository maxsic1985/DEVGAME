using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


namespace MSuhininTestovoe.Devgame
{


    public class MapGeneratorInitSystem: IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ScriptableObjectComponent> _scriptableObjectPool;
        private EcsPool<LoadPrefabComponent> _loadPrefabPool;
        private EcsPool<MapGeneratorComponent> _mapGeneratorComponentPool;
        private readonly  EcsCustomInject<PathfinderScan> _scan=default;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<MapGeneratorComponent>().Inc<ScriptableObjectComponent>().End();
            _scriptableObjectPool = _world.GetPool<ScriptableObjectComponent>();
            _loadPrefabPool = _world.GetPool<LoadPrefabComponent>();
            _mapGeneratorComponentPool = _world.GetPool<MapGeneratorComponent>();
       
        }

        public void Run(IEcsSystems systems)
        {
            
            foreach (var entity in _filter)
            {
                if (_scriptableObjectPool.Get(entity).Value is MapGeneratorData dataInit)
                {
                    ref var mapGeneratorComponent = ref _mapGeneratorComponentPool.Get(entity);
                    ref var loadPrefabFromPool = ref _loadPrefabPool.Add(entity);
                    loadPrefabFromPool.Value = dataInit.Unit;
                    mapGeneratorComponent.Height = dataInit.Height;
                    mapGeneratorComponent.Weight = dataInit.Weight;
                    mapGeneratorComponent.PathfinderScan = _scan.Value;
                }
                _scriptableObjectPool.Del(entity);
            }
        }
    }
}