using LeoEcsPhysics;
using Leopotam.EcsLite;
using Pathfinding;
using UnityEngine;


namespace MSuhininTestovoe.Devgame
{
    public class EnemyCastSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filterEnemy;
        private EcsPool<IsEnemyFollowingComponent> _enemyIsFollowComponentPool;
        private EcsPool<TransformComponent> _transformComponent;
        private EcsPool<AIPathComponent> _isEnemyCanAtackComponenPool;
        private EcsPool<AIDestanationComponent> _aiDestanationComponenPool;
        private EcsPool<HealthViewComponent> _enemyHealthViewComponentPool;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filterEnemy = _world
                .Filter<AIDestanationComponent>()
                .Inc<TransformComponent>()
                .Exc<IsEnemyFollowingComponent>()
                .End();
            
            _enemyIsFollowComponentPool = _world.GetPool<IsEnemyFollowingComponent>();
            _isEnemyCanAtackComponenPool = _world.GetPool<AIPathComponent>();
            _enemyHealthViewComponentPool = _world.GetPool<HealthViewComponent>();
            _transformComponent = _world.GetPool<TransformComponent>();
            _aiDestanationComponenPool = _world.GetPool<AIDestanationComponent>();
        }

        public void Run(IEcsSystems ecsSystems)
        {
            foreach (var entity in _filterEnemy)
            {   
                LayerMask mask = LayerMask.GetMask(GameConstants.PLAYER_LAYER);
                ref AIDestanationComponent aiDestanationComponent = ref _aiDestanationComponenPool.Get(entity);
                ref TransformComponent transform = ref _transformComponent.Get(entity);
                var aiDestinationSetter = transform.Value.GetComponent<AIDestinationSetter>();
                RaycastHit2D hit = Physics2D.CircleCast(transform.Value.position,
                    aiDestanationComponent.CastDistance,
                    transform.Value.forward,
                    0f,
                    mask);
                if (hit)
                {
                    var player = hit.collider.gameObject.GetComponent<PlayerActor>();
                    if(player==null) return;
                    
                    
                    var reached = transform.Value.GetComponent<AIPath>();
                    
                    ref AIPathComponent isReacheded = ref _isEnemyCanAtackComponenPool.Add(entity);
                    var target = player.transform;
                    aiDestinationSetter.target = target;
                    isReacheded.AIPath = reached;
                    reached.endReachedDistance = aiDestanationComponent.EndReachedDistance;
                    
                    _enemyIsFollowComponentPool.Add(entity);
                }
            }
        }
    }
}
