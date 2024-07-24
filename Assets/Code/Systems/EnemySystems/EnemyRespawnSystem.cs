using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using MSuhininTestovoe.Devgame;
using UniRx;
using Vector3 = UnityEngine.Vector3;



namespace MSuhininTestovoe.B2B
{
    public class EnemyRespawnSystem : IEcsInitSystem, IEcsDestroySystem, IDisposable
    {
        private EcsWorld _world;
        private EcsFilter filter;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<EnemyStartPositionComponent> _spawnPositionComponent;
        private IPoolService _poolService;
        private List<IDisposable> _disposables = new List<IDisposable>();
        private PlayerSharedData _sharedData;


        public void Init(IEcsSystems systems)
        {
            _sharedData = systems.GetShared<SharedData>().GetPlayerSharedData;
            _poolService = Service<IPoolService>.Get();

            _world = systems.GetWorld();
            filter = systems.GetWorld()
                .Filter<EnemyStartPositionComponent>()
                .Inc<TransformComponent>()
                .Exc<IsEnemyComponent>()
                .End();
            _spawnPositionComponent = _world.GetPool<EnemyStartPositionComponent>();
            _transformComponentPool = _world.GetPool<TransformComponent>();
            
            Observable.Interval(TimeSpan.FromMilliseconds(7000))
                .Where(_ => true).Subscribe(x =>
                {
                    Respawn(1);
                })
                .AddTo(_disposables);
        }


        private void Respawn(int cnt)
        {
            foreach (var _ in filter)
            {
                for (int i = 0; i <cnt; i++)
                {
                    if (_poolService == null)
                    {
                        _poolService = Service<IPoolService>.Get();
                    }

                    var pooled = _poolService.Get(GameObjectsTypeId.Enemy);
                    var entity = pooled.gameObject.GetComponent<EnemyActor>().Entity;
                    

                    ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
                    ref EnemyStartPositionComponent position = ref _spawnPositionComponent.Get(entity);
                    transformComponent.Value.position = position.Value;
                }

                return;
            }
        }

        public void Destroy(IEcsSystems systems)
        {
            Dispose();
        }

        public void Dispose()
        {
            _disposables.Clear();
        }
    }
}