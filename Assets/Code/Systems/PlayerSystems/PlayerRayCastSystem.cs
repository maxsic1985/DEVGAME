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


        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world
                .Filter<IsPlayerComponent>()
                .Inc<TransformComponent>()
                .End();

            _transformComponentPool = world.GetPool<TransformComponent>();
            _isCanAttackComponentPool = world.GetPool<IsPlayerCanAttackComponent>();
        }


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref TransformComponent transformComponent = ref _transformComponentPool.Get(entity);
                LayerMask mask = LayerMask.GetMask("Enemy");
                RaycastHit2D hit = Physics2D.Raycast(transformComponent.Value.position, transformComponent.Value.right,
                    5f, mask);
                if (hit)
                {
                    if (_isCanAttackComponentPool.Has(entity) == false)
                    {
                        _isCanAttackComponentPool.Add(entity);
                    }
                }
                else
                {
                    _isCanAttackComponentPool.Del(entity);
                }
            }
        }
    }
}