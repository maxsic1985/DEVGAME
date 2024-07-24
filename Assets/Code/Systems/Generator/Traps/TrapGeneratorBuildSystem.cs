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

                CreateTraps(mapGenerator, prefabComponent);
                _prefabPool.Del(entity);
            }
        }

        private void CreateTraps(TrapGeneratorComponent trapGenerator, PrefabComponent prefabComponent)
        {
            for (int i = 0; i < trapGenerator.DeathCount; i++)
            {
                var deathTrap = Object.Instantiate(prefabComponent.Value);
                var position = Extensions.GetRandomVector(-18,18,-14,14);
                deathTrap.transform.position =position;
                deathTrap.transform.localScale = new Vector3(trapGenerator.DeathSizeArea, trapGenerator.DeathSizeArea, 1);
                deathTrap.GetComponent<SpriteRenderer>().color=Color.red;
                deathTrap.GetComponent<TrapActor>().TrapType = TrapType.DEATH;
                trapGenerator.Type = TrapType.DEATH;
            }
            
            for (int i = 0; i < trapGenerator.SlowCount; i++)
            {
                var slowTrap = Object.Instantiate(prefabComponent.Value);
                var position = Extensions.GetRandomVector(-16,16,-12,12);
                slowTrap.transform.position =position;
                slowTrap.transform.localScale =
                    new Vector3(trapGenerator.SlowSizeArea, trapGenerator.SlowSizeArea, 1);
                slowTrap.GetComponent<SpriteRenderer>().color=Color.blue;
                slowTrap.GetComponent<TrapActor>().TrapType = TrapType.SLOW;
                trapGenerator.Type = TrapType.SLOW;
            }
        }
    }
}