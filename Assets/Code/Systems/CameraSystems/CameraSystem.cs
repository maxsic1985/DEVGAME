using Leopotam.EcsLite;

namespace MSuhininTestovoe.Devgame
{
    public class CameraSystem: IEcsInitSystem
    {
        private EcsPool<CameraComponent> _isCameraPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            
            _isCameraPool = world.GetPool<CameraComponent>();
            _isCameraPool.Add(entity);

            var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
            ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
            loadFactoryDataComponent.AddressableName = AssetsNamesConstants.CAMERA_PREFAB_NAME;
        }
    }
}