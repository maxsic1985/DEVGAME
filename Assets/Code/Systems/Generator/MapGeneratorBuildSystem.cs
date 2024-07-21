using Leopotam.EcsLite;
using UnityEngine;

namespace MSuhininTestovoe.Devgame
{
    public class MapGeneratorBuildSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<PrefabComponent> _prefabPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<MapGeneratorComponent> _mapGeneratorComponentPool;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world.Filter<MapGeneratorComponent>().Inc<PrefabComponent>().End();
            _prefabPool = world.GetPool<PrefabComponent>();
            _transformComponentPool = world.GetPool<TransformComponent>();
            _mapGeneratorComponentPool = world.GetPool<MapGeneratorComponent>();

        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var prefabComponent = ref _prefabPool.Get(entity);
                ref var mapGenerator = ref _mapGeneratorComponentPool.Get(entity);
               
                for (int i = 0; i <= mapGenerator.Height; i++)
                {
                    for (int j = 0; j <= mapGenerator.Weight; j++)
                    {
                        if (i==0 || i==mapGenerator.Height)
                        {
                            var gameObject = Object.Instantiate(prefabComponent.Value);
                            gameObject.transform.position = new Vector3(mapGenerator.Weight / 2 - j,
                                mapGenerator.Height/2-i , 0);
                        }
                      else  if (j==0 || j==mapGenerator.Weight)
                        {
                            var gameObject = Object.Instantiate(prefabComponent.Value);
                            gameObject.transform.position = new Vector3(mapGenerator.Weight / 2 - j,
                                mapGenerator.Height/2-i , 0);
                        }
                    }
                }
                
                _prefabPool.Del(entity);
            }
        }
    }
}