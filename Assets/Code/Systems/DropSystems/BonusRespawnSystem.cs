using System;
using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using MSuhininTestovoe.Devgame;
using UniRx;
using UnityEngine;


namespace MSuhininTestovoe.B2B
{
    public class BonusRespawnSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private EcsWorld _world;
        private EcsFilter filter;
        private EcsPool<TransformComponent> _transformComponentPool;
        private IPoolService _poolService;
        private IDisposable _dispose;
        private PlayerSharedData _sharedData;
        private int _interval = 5000;
        private int _stepInterval = 100;
        private const int _minInterval = 500;
        private int _tick = 0;
        private bool start;
        private bool run;

        public void Init(IEcsSystems systems)
        {
            _sharedData = systems.GetShared<SharedData>().GetPlayerSharedData;
            _poolService = Service<IPoolService>.Get();

            _world = systems.GetWorld();
            filter = systems.GetWorld()
                .Filter<BonusComponent>()
                .Inc<TransformComponent>()
                .Exc<IsBonusComponent>()
                .End();

            _transformComponentPool = _world.GetPool<TransformComponent>();


            start = true;
            run = true;
            _dispose = Observable.Interval(TimeSpan.FromMilliseconds(_interval))
                .Where(_ => start).Subscribe(x => { RespawnBonus(1); });
        }


        private void RespawnBonus(int cnt)
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

                    var pooled = _poolService.Get(GameObjectsTypeId.Bonus);
                    var entity = pooled.gameObject.GetComponent<BonusActor>().Entity;
                    pooled.transform.position = Vector3.one;

                    ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
                    transformComponent.Value.position = pooled.transform.position;
                }

                return;
            }
        }

        public void Destroy(IEcsSystems systems)
        {
            _dispose.Dispose();
        }
    }
}