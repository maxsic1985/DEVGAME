using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;



namespace MSuhininTestovoe.Devgame
{
    public class PlayerRotateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _playerFilter;
        private EcsFilter _cameraFilter;
        private EcsPool<PlayerInputComponent> _playerInputComponentPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private ITimeService _timeService;
        private   PlayerSharedData _sharedData;
        private Vector3 playerPosition;

        
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _sharedData = systems.GetShared<SharedData>().GetPlayerSharedData;
            _playerFilter = world
                .Filter<IsPlayerComponent>()
                .Inc<PlayerInputComponent>()
                .Inc<TransformComponent>()
                .End();
            _cameraFilter = world.Filter<IsCameraComponent>().End();
            
            _playerInputComponentPool = world.GetPool<PlayerInputComponent>();
            _transformComponentPool = world.GetPool<TransformComponent>();
            _timeService = Service<ITimeService>.Get();
        }

        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _playerFilter)
            {
                ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
                ref PlayerInputComponent playerInputComponent = ref _playerInputComponentPool.Get(entity);

                if (playerInputComponent.Rotate)
                {
                    Vector3 mousePosition =GameObject.Find("Camera(Clone)").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
                    var position = transformComponent.Value;
                    Vector2 direction = mousePosition - position.position;
                    float angle = Vector2.SignedAngle(Vector2.right, direction);
                    Vector3 targetRotation = new Vector3(0, 0, angle);
                    position.rotation = Quaternion.RotateTowards(
                        position.rotation,
                        Quaternion.Euler(targetRotation),
                        _sharedData.GetPlayerCharacteristic.RotateSpeed * _timeService.DeltaTime);
                }
            }
        }
    }
}