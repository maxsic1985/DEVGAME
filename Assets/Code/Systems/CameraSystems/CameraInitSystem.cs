using Leopotam.EcsLite;
using UnityEngine;

namespace MSuhininTestovoe.Devgame
{
    public class CameraInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ScriptableObjectComponent> _scriptableObjectPool;
        private EcsPool<LoadPrefabComponent> _loadPrefabPool;
        private EcsPool<CameraPositionComponent> _cameraStartPositionComponentPool;
        private EcsPool<CameraRotationComponent> _cameraStartRotationComponentPool;
        private EcsPool<CameraComponent> _isCameraComponentPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<CameraComponent>().Inc<ScriptableObjectComponent>().End();
            _scriptableObjectPool = _world.GetPool<ScriptableObjectComponent>();
            _loadPrefabPool = _world.GetPool<LoadPrefabComponent>();
            _cameraStartPositionComponentPool = _world.GetPool<CameraPositionComponent>();
            _cameraStartRotationComponentPool = _world.GetPool<CameraRotationComponent>();
            _isCameraComponentPool = _world.GetPool<CameraComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                if (_scriptableObjectPool.Get(entity).Value is CameraLoadData dataInit)
                {
                    ref LoadPrefabComponent loadPrefabFromPool = ref _loadPrefabPool.Add(entity);
                    loadPrefabFromPool.Value = dataInit.Camera;

                    ref CameraPositionComponent cameraPositionComponent =
                        ref _cameraStartPositionComponentPool.Add(entity);
                    cameraPositionComponent.Value = dataInit.StartPosition;

                    ref CameraRotationComponent cameraRotationComponent =
                        ref _cameraStartRotationComponentPool.Add(entity);
                    cameraRotationComponent.Value = dataInit.StartRotation;

                    ref CameraComponent cameraComponent = ref _isCameraComponentPool.Get(entity);
                    cameraComponent.CameraSmoothness = dataInit.CameraSmoothness;
                    cameraComponent.Size = dataInit.Size;
                    cameraComponent.Offset = dataInit.StartPosition;
                    cameraComponent.CurrentVelocity = Vector3.zero;
                }

                _scriptableObjectPool.Del(entity);
            }
        }
    }
}