using System;
using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using MSuhininTestovoe.Devgame;
using UniRx;
using UnityEngine;
using Random = System.Random;


namespace MSuhininTestovoe.B2B
{
    public class BonusRespawnSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private EcsWorld _world;
        private EcsFilter _bonusFilter;
        private EcsFilter _playerFilter;
        private EcsFilter _cameraFilter;
        private EcsFilter _mapFilter;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<CameraComponent> _cameraComponentPool;
        private EcsPool<MapGeneratorComponent> _mapComponentPool;
        private IPoolService _poolService;
        private IDisposable _dispose;
        private int _interval = 10000;
        private int _cameraSize;
        private int[] _mapSize;
      

        public void Init(IEcsSystems systems)
        {
            _poolService = Service<IPoolService>.Get();
            _mapSize = new int[2];
            _world = systems.GetWorld();
            _bonusFilter = systems.GetWorld()
                .Filter<BonusComponent>()
                .Inc<TransformComponent>()
                .Exc<IsBonusComponent>()
                .End();
            
            _playerFilter= systems.GetWorld()
                .Filter<IsPlayerComponent>()
                .Inc<TransformComponent>()
                .End();

            _cameraFilter = systems.GetWorld().Filter<CameraComponent>().End();
          
            _mapFilter = systems.GetWorld().Filter<MapGeneratorComponent>().End();
            

            _transformComponentPool = _world.GetPool<TransformComponent>();
            _cameraComponentPool = _world.GetPool<CameraComponent>();
            _mapComponentPool = _world.GetPool<MapGeneratorComponent>();
            
            _dispose = Observable.Interval(TimeSpan.FromMilliseconds(_interval))
                .Where(_ => true).Subscribe(x => { RespawnBonus(1); });
        }


        private void RespawnBonus(int cnt)
        {
            foreach (var cameraEntity in _cameraFilter)
            {
                ref CameraComponent cameraComponent = ref _cameraComponentPool.Get(cameraEntity);
                _cameraSize = (int)cameraComponent.Size;
            }
            
            foreach (var mapEntity in _mapFilter)
            {
                ref MapGeneratorComponent mapGenerator = ref _mapComponentPool.Get(mapEntity);
                _mapSize[0] = (int)mapGenerator.Height;
                _mapSize[1] = (int)mapGenerator.Weight;
            }
            
            foreach (var _ in _bonusFilter)
            {
                for (int i = 0; i < cnt; i++)
                {
                    if (_poolService == null)
                    {
                        _poolService = Service<IPoolService>.Get();
                    }

                    foreach (var playerEntity in _playerFilter)
                    {
                        var pooled = _poolService.Get(GameObjectsTypeId.Bonus);
                        var entity = pooled.gameObject.GetComponent<BonusActor>().Entity;
                        GetBonusPosition(playerEntity, pooled);

                        ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
                        transformComponent.Value.position = pooled.transform.position;
                    }
                }

                return;
            }
        }

        private void GetBonusPosition(int playerEntity, GameObject pooled)
        {
            ref TransformComponent playerTransform = ref _transformComponentPool.Get(playerEntity);
            var playerPosition = playerTransform.Value.position;

            var rnd = new System.Random();

            var dX = _cameraSize * 2;
            var dy = _cameraSize;

            var borderX = _mapSize[0] / 2;
            var borderY = _mapSize[1] / 2;

            var minX = playerPosition.x - dX > -borderX ? playerPosition.x - dX : -borderX - 1;
            var maxX = playerPosition.x + dX > borderX ? borderX - 1 : playerPosition.x + dX;

            var minY = playerPosition.y - dy > -borderY ? playerPosition.y - dy : -borderY - 1;
            var maxY = playerPosition.y + dy > borderY ? borderY - 1 : playerPosition.y + dy;

            pooled.transform.position = new Vector3(rnd.Next((int)minX, (int)maxX), rnd.Next((int)minY, (int)maxY), 0);
        }

        public void Destroy(IEcsSystems systems)
        {
            _dispose.Dispose();
        }
    }
}