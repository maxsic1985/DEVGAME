using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;


namespace MSuhininTestovoe.Devgame
{
    public class PlayerRayCastSystem : EcsUguiCallbackSystem, IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsFilter _enemyFilter;
        private EcsPool<IsPlayerCanAttackComponent> _isCanAttackComponentPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<HealthViewComponent> _enemyHealthViewComponentPool;
        private PlayerSharedData _sharedData;
        private int _enemyEntity;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world
                .Filter<IsPlayerComponent>()
                .Inc<TransformComponent>()
                .End();

            _transformComponentPool = world.GetPool<TransformComponent>();
            _isCanAttackComponentPool = world.GetPool<IsPlayerCanAttackComponent>();
            _enemyHealthViewComponentPool = world.GetPool<HealthViewComponent>();
            _sharedData = systems.GetShared<SharedData>().GetPlayerSharedData;
        }


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
                LayerMask mask = LayerMask.GetMask(GameConstants.ENEMY_LAYER);
                RaycastHit2D hit = Physics2D.Raycast(
                    transformComponent.Value.position,
                    transformComponent.Value.right,
                    _sharedData.GetPlayerCharacteristic.RayDistance,
                    mask);

                if (hit)
                {
                    if (_isCanAttackComponentPool.Has(entity) == false)
                    {
                        _enemyEntity = hit.collider.GetComponent<EnemyActor>().Entity;
                        if (_enemyHealthViewComponentPool.Has(_enemyEntity) == false)
                        {
                            ref HealthViewComponent enemyHealthView = ref _enemyHealthViewComponentPool.Add(hit.collider.GetComponent<EnemyActor>().Entity);
                            enemyHealthView.Value = hit.collider.GetComponent<HealthView>().Value;
                            enemyHealthView.Value.color = Color.red;
                        }
                        _isCanAttackComponentPool.Add(entity);
                    }
                }
                else
                {
                    if (_isCanAttackComponentPool.Has(entity))
                    {
                        _isCanAttackComponentPool.Del(entity);
                    }

                    if (_enemyHealthViewComponentPool.Has(_enemyEntity))
                    {
                        ref HealthViewComponent enemyHealthView = ref _enemyHealthViewComponentPool.Get(_enemyEntity);
                        enemyHealthView.Value.color = Color.green;
                        _enemyHealthViewComponentPool.Del(_enemyEntity);
                    }
                }
            }
        }
    }
}