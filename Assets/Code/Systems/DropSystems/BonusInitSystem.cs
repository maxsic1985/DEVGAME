using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using Pathfinding;
using UnityEngine;


namespace MSuhininTestovoe.Devgame
{
    public class BonusInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ScriptableObjectComponent> _scriptableObjectPool;
        private EcsPool<LoadPrefabComponent> _loadPrefabPool;
        private EcsPool<IsBonusComponent> _isBonusPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<BoxColliderComponent> _enemyBoxColliderComponentPool;
        private EcsPool<BonusComponent> _bonusComponentPool;
        private IPoolService _poolService;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _poolService = Service<IPoolService>.Get();
            _filter = _world
                .Filter<IsBonusComponent>()
                .Inc<ScriptableObjectComponent>()
                .End();

            _isBonusPool = _world.GetPool<IsBonusComponent>();
            _scriptableObjectPool = _world.GetPool<ScriptableObjectComponent>();
            _loadPrefabPool = _world.GetPool<LoadPrefabComponent>();
            _transformComponentPool = _world.GetPool<TransformComponent>();
            _enemyBoxColliderComponentPool = _world.GetPool<BoxColliderComponent>();
            _bonusComponentPool = _world.GetPool<BonusComponent>();
        }


        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                if (_scriptableObjectPool.Get(entity).Value is BonusData dataInit)
                {
                    ref LoadPrefabComponent loadPrefabFromPool = ref _loadPrefabPool.Add(entity);

                    CreateBonus(entity, dataInit);
                }
                _scriptableObjectPool.Del(entity);
            }
        }


        private void CreateBonus(int entity, BonusData dataInit)
        {
            for (int i = 0; i < _poolService.Capacity; i++)
            {
                var newEntity = _world.NewEntity();
                GameObject pooled = _poolService.Get(GameObjectsTypeId.Bonus);
                pooled.gameObject.GetComponent<BonusActor>().AddEntity(newEntity);

                ref TransformComponent transformComponent = ref _transformComponentPool.Add(newEntity);
                ref BoxColliderComponent enemyBoxColliderComponent = ref _enemyBoxColliderComponentPool.Add(newEntity);
                ref BonusComponent bonus = ref _bonusComponentPool.Add(newEntity);
                transformComponent.Value = pooled.gameObject.GetComponent<TransformView>().Transform;
                enemyBoxColliderComponent.ColliderValue = pooled.GetComponent<BoxCollider>();
                _poolService.Return(pooled);
            }

            _isBonusPool.Del(entity);
            _loadPrefabPool.Del(entity);
        }
    }
}