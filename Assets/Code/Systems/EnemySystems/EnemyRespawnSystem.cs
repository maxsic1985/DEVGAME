using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using MSuhininTestovoe.Devgame;
using UniRx;
using UnityEngine;


namespace MSuhininTestovoe.B2B
{
    public class EnemyRespawnSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private EcsWorld _world;
        private EcsFilter filter;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<EnemyStartPositionComponent> _spawnPositionComponent;
        private IPoolService _poolService;
        private IDisposable _dispose;
        private PlayerSharedData _sharedData;
        private int _interval = 2000;
        private int _stepInterval = 100;
        private const int  _minInterval = 500;
        private int _tick = 0;
        private bool start;
        private bool run;

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


            start = true;
            run = true;
            _dispose = Observable.Interval(TimeSpan.FromMilliseconds(_interval))
                .Where(_ => start).Subscribe(x => { Respawn(1); });
        }


        private void Respawn(int cnt)
        {
            _tick += 1;
            foreach (var _ in filter)
            {
                for (int i = 0; i < cnt; i++)
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
            _dispose.Dispose();
        }


        public void Run(IEcsSystems systems)
        {
            if (_interval <= _minInterval) return;
            
            if (_tick == 10 && run)
            {
                start = false;
                run = false;

                _dispose.Dispose();
                _interval -= _stepInterval;
                _dispose = Observable.Interval(TimeSpan.FromMilliseconds(_interval))
                    .Where(_ => start).Subscribe(x => { Respawn(1); });
                _tick = 0;
                run = true;
                start = true;
                return;
            }
        }
    }
}