using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


namespace MSuhininTestovoe.Devgame
{
    public class TrapGeneratorInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ScriptableObjectComponent> _scriptableObjectPool;
        private EcsPool<LoadPrefabComponent> _loadPrefabPool;
        private EcsPool<TrapGeneratorComponent> _trapGeneratorComponentPool;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<TrapGeneratorComponent>().Inc<ScriptableObjectComponent>().End();
            _scriptableObjectPool = _world.GetPool<ScriptableObjectComponent>();
            _loadPrefabPool = _world.GetPool<LoadPrefabComponent>();
            _trapGeneratorComponentPool = _world.GetPool<TrapGeneratorComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                if (_scriptableObjectPool.Get(entity).Value is TrapGeneratorData dataInit)
                {
                    ref var trapComponent = ref _trapGeneratorComponentPool.Get(entity);
                    ref var loadPrefabFromPool = ref _loadPrefabPool.Add(entity);
                    loadPrefabFromPool.Value = dataInit.DeathTrap;
                    trapComponent.Count = dataInit.Count;
                }

                _scriptableObjectPool.Del(entity);
            }
        }
    }
}