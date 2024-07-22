using Leopotam.EcsLite;
using UnityEngine;

namespace MSuhininTestovoe.Devgame
{


    public class CameraBuildSystem: IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<PrefabComponent> _prefabPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<CameraPositionComponent> _cameraStartPositionComponentPool;
        private EcsPool<CameraRotationComponent> _cameraStartRotationComponentPool;
        private EcsPool<CameraComponent> _cameraComponent;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world.Filter<CameraComponent>().Inc<PrefabComponent>().End();
            _prefabPool = world.GetPool<PrefabComponent>();
            _transformComponentPool = world.GetPool<TransformComponent>();
            _cameraStartPositionComponentPool = world.GetPool<CameraPositionComponent>();
            _cameraStartRotationComponentPool = world.GetPool<CameraRotationComponent>();
            _cameraComponent = world.GetPool<CameraComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref PrefabComponent prefabComponent = ref _prefabPool.Get(entity);
                ref TransformComponent transformComponent = ref _transformComponentPool.Add(entity);
                ref CameraPositionComponent cameraPosition = ref _cameraStartPositionComponentPool.Get(entity);
                ref CameraRotationComponent cameraRotation = ref _cameraStartRotationComponentPool.Get(entity);
                ref CameraComponent cameraComponent = ref _cameraComponent.Get(entity);

                GameObject cameraObject = Object.Instantiate(prefabComponent.Value);
                transformComponent.Value =  cameraObject.GetComponent<TransformView>().Transform;
                cameraObject.transform.position = cameraPosition.Value;
                cameraObject.transform.eulerAngles = cameraRotation.Value;
               var camera= cameraObject.GetComponent<Camera>();
               camera.orthographicSize = cameraComponent.Size;
               _prefabPool.Del(entity);
            }
        }
    }
}