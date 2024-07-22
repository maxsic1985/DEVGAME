using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;

namespace MSuhininTestovoe.Devgame
{
    public class CameraFollowSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _cameraFilter;
        private EcsFilter _playerFilter;
        private EcsPool<CameraComponent> _isCameraComponentPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private ITimeService _timeService;


        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _cameraFilter = world.Filter<CameraComponent>().Inc<TransformComponent>().End();
            _playerFilter = world.Filter<IsPlayerComponent>().End();
            _isCameraComponentPool = world.GetPool<CameraComponent>();
            _transformComponentPool = world.GetPool<TransformComponent>();
            _timeService = Service<ITimeService>.Get();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int cameraEntity in _cameraFilter)
            {
                ref CameraComponent cameraComponent = ref _isCameraComponentPool.Get(cameraEntity);
                ref TransformComponent cameraTransformComponent = ref _transformComponentPool.Get(cameraEntity);
                ref TransformComponent playerTransformComponent = ref _transformComponentPool.Get(_playerFilter.GetRawEntities()[0]);
                var position = cameraTransformComponent.Value.position;
                var currentPosition = new Vector3(position.x,position.y,GameConstants.CAMERA_Z_OFFSET);
                var playerPosition = playerTransformComponent.Value;
                Vector3 targetPoint = new Vector3(playerPosition.localPosition.x, playerPosition.position.y,GameConstants.CAMERA_Z_OFFSET);
     
                position = Vector3.SmoothDamp(currentPosition, targetPoint,
                    ref cameraComponent.CurrentVelocity , cameraComponent.CameraSmoothness);
                cameraTransformComponent.Value.position = position;
            }
        }
    }
}