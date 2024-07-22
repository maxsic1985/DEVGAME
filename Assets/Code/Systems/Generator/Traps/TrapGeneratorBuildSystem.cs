using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Pathfinding;
using UnityEngine;

namespace MSuhininTestovoe.Devgame
{
    public class TrapGeneratorBuildSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<PrefabComponent> _prefabPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<TrapGeneratorComponent> _mapGeneratorComponentPool;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world.Filter<TrapGeneratorComponent>().Inc<PrefabComponent>().End();
            _prefabPool = world.GetPool<PrefabComponent>();
            _transformComponentPool = world.GetPool<TransformComponent>();
            _mapGeneratorComponentPool = world.GetPool<TrapGeneratorComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var prefabComponent = ref _prefabPool.Get(entity);
                ref var mapGenerator = ref _mapGeneratorComponentPool.Get(entity);

                CreateBorder(mapGenerator, prefabComponent);
                _prefabPool.Del(entity);
            }
        }

        private void CreateBorder(TrapGeneratorComponent trapGenerator, PrefabComponent prefabComponent)
        {
            for (int i = 0; i < trapGenerator.Count; i++)
            {
                var gameObject = Object.Instantiate(prefabComponent.Value);
                gameObject.transform.position = new Vector3(0, i+2, 0);
            }
        }
    }
}