using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using MSuhininTestovoe.Devgame;
using UniRx;
using Vector3 = UnityEngine.Vector3;



namespace MSuhininTestovoe.B2B
{
<<<<<<< Updated upstream
    public class EnemyRespawnSystem : IEcsInitSystem, IEcsDestroySystem, IDisposable
=======
    public class EnemyRespawnSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem,IDisposable
>>>>>>> Stashed changes
    {
        private EcsWorld _world;
        private EcsFilter filter;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<EnemyStartPositionComponent> _spawnPositionComponent;
        private IPoolService _poolService;
<<<<<<< Updated upstream
=======
        private IDisposable _dispose;
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            
            Observable.Interval(TimeSpan.FromMilliseconds(7000))
                .Where(_ => true).Subscribe(x =>
                {
                    Respawn(1);
                })
                .AddTo(_disposables);
=======


            start = true;
            run = true;
            Observable.Interval(TimeSpan.FromMilliseconds(_interval))
                .Where(_ => start).Subscribe(x => { Respawn(1); }).AddTo(_disposables);
            
            Observable.Interval(TimeSpan.FromMilliseconds(1000))
                .Where(_ => start).Subscribe(x => { Tick(); }).AddTo(_disposables);
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
        public void Destroy(IEcsSystems systems)
        {
            Dispose();
        }
=======
>>>>>>> Stashed changes

        public void Dispose()
        {
<<<<<<< Updated upstream
            _disposables.Clear();
=======
            if (_interval <= _minInterval) return;
            
            if (_tick == 10 && run)
            {
                start = false;
                run = false;
                Dispose();

                _interval -= _stepInterval;
                _dispose = Observable.Interval(TimeSpan.FromMilliseconds(_interval))
                    .Where(_ => start).Subscribe(x => { Respawn(1); });
                _tick = 0;
                run = true;
                start = true;
                return;
            }
>>>>>>> Stashed changes
        }

        private void Tick()
        { 
            _tick += 1;
        }

        public void Destroy(IEcsSystems systems)
        {
            Dispose();
        }

        public void Dispose()
        {
            for (int i = 0; i < _disposables.Count; i++)
            {
                _disposables[i].Dispose();
            }
        }
    }
}
