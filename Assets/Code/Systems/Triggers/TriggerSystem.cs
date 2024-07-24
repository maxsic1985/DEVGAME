using LeoEcsPhysics;
using Leopotam.EcsLite;



namespace MSuhininTestovoe.Devgame
{
    public  partial class TriggerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filterEnterToTrigger;
        private EcsFilter _filterExitFromTrigger;
        private PlayerSharedData _sharedData;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _sharedData = systems.GetShared<SharedData>().GetPlayerSharedData;

            _filterEnterToTrigger = _world
                .Filter<OnTriggerEnter2DEvent>()
                .Exc<AIDestanationComponent>()
                .Exc<IsEnemyFollowingComponent>()
                .End();
            
            
            _filterItem = _world
                .Filter<ItemComponent>()
                .End();
            
            _itemSlotPool = _world.GetPool<ItemComponent>();
            _dropPool = _world.GetPool<DropComponent>();
        }

        public  void Run(IEcsSystems ecsSystems)
        {
            var poolEnter = _world.GetPool<OnTriggerEnter2DEvent>();
            DropEnterToTrigger(ecsSystems, poolEnter);
            TrapEnterToTrigger(ecsSystems, poolEnter);
        }
    }
}