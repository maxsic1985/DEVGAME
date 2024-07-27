using Leopotam.EcsLite;



namespace MSuhininTestovoe.Devgame
{
    public class BonusLoadSystem: IEcsInitSystem
    {
        private EcsPool<IsBonusComponent> _isEnemyPool;

        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            
            _isEnemyPool = world.GetPool<IsBonusComponent>();
            _isEnemyPool.Add(entity);

            var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
            ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
            loadFactoryDataComponent.AddressableName = AssetsNamesConstants.BONUS_DATA;
        }
    }
}