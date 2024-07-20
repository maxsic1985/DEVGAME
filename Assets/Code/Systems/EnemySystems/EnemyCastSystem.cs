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
        }

        public void Run(IEcsSystems ecsSystems)
        {
            foreach (var entity in _filterEnemy)
            {   
                LayerMask mask = LayerMask.GetMask("Player");
                ref TransformComponent transform = ref _transformComponent.Get(entity);
                var aiDestinationSetter = transform.Value.GetComponent<AIDestinationSetter>();
                RaycastHit2D hit = Physics2D.CircleCast(transform.Value.position, 3f, transform.Value.forward,0f,mask);
                if (hit)
                {
                    var player = hit.collider.gameObject.GetComponent<PlayerActor>();
                    if(player==null) return;
                    
                    
                    var reached = transform.Value.GetComponent<AIPath>();
                    
                    ref AIPathComponent isReacheded = ref _isEnemyCanAtackComponenPool.Add(entity);
                    var target = player.transform;
                    aiDestinationSetter.target = target;
                    isReacheded.AIPath = reached;
                    reached.endReachedDistance = 0.5f;

                    /*Extensions.AddPool<HealthViewComponent>(ecsSystems, entity);
                    ref HealthViewComponent enemyHealthView = ref _enemyHealthViewComponentPool.Get(entity);
                    enemyHealthView.Value = transform.Value.GetComponent<EnemyActor>().GetComponent<HealthView>().Value;*/

              
                    _enemyIsFollowComponentPool.Add(entity);
                }
            }
        }
    }
}
