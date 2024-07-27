using Leopotam.EcsLite;


namespace MSuhininTestovoe.Devgame
{
    public class EnemyAtackSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter filterTrigger;
        private EcsWorld _world;
        private EcsPool<AIPathComponent> _isReachedComponentPool;
        private EcsPool<AIDestanationComponent> _aiDestanationComponenPool;
        private PlayerSharedData _sharedData;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _sharedData = systems.GetShared<SharedData>().GetPlayerSharedData;
    


            filterTrigger = systems.GetWorld()
                .Filter<AIPathComponent>()
                .Inc<AIDestanationComponent>()
                .End();
            
            _isReachedComponentPool = _world.GetPool<AIPathComponent>();
            _aiDestanationComponenPool = _world.GetPool<AIDestanationComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filterTrigger)
            {
                ref AIPathComponent path = ref _isReachedComponentPool.Get(entity);
                ref AIDestanationComponent target = ref _aiDestanationComponenPool.Get(entity);


                if (target.AIDestinationSetter.target.GetComponent<PlayerActor>())
                {
                    if (path.AIPath.reachedEndOfPath)
                    {
                        _sharedData.GetPlayerCharacteristic.GetLives.UpdateLives(0);
                    }
                }
            }
        }
    }
}