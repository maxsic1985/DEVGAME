﻿using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using Pathfinding;
using UnityEngine;


namespace MSuhininTestovoe.Devgame
{
    public class EnemyInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ScriptableObjectComponent> _scriptableObjectPool;
        private EcsPool<LoadPrefabComponent> _loadPrefabPool;
        private EcsPool<IsEnemyComponent> _isEnemyPool;
        private EcsPool<EnemyStartPositionComponent> _enemyStartPositionComponentPool;
        private EcsPool<EnemyStartRotationComponent> _enemyStartRotationComponentPool;
        private EcsPool<EnemySpawnComponent> _enemySpawnComponentPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<EnemyHealthComponent> _enemyHealthComponentPool;
        private EcsPool<HealthViewComponent> _enemyHealthViewComponentPool;
        private EcsPool<AIDestanationComponent> _aiDestanationComponenPool;
        private EcsPool<AIPathComponent> _aIpathComponenPool;
        private EcsPool<BoxColliderComponent> _enemyBoxColliderComponentPool;
        private EcsPool<DropAssetComponent> _dropComponentPool;
        private IPoolService _poolService;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _poolService = Service<IPoolService>.Get();
            _filter = _world
                .Filter<IsEnemyComponent>()
                .Inc<ScriptableObjectComponent>()
                .End();

            _isEnemyPool = _world.GetPool<IsEnemyComponent>();
            _scriptableObjectPool = _world.GetPool<ScriptableObjectComponent>();
            _loadPrefabPool = _world.GetPool<LoadPrefabComponent>();
            _enemySpawnComponentPool = _world.GetPool<EnemySpawnComponent>();
            _enemyStartRotationComponentPool = _world.GetPool<EnemyStartRotationComponent>();
            _enemyStartPositionComponentPool = _world.GetPool<EnemyStartPositionComponent>();
            _transformComponentPool = _world.GetPool<TransformComponent>();
            _enemyHealthComponentPool = _world.GetPool<EnemyHealthComponent>();
            _enemyHealthViewComponentPool = _world.GetPool<HealthViewComponent>();
            _enemyBoxColliderComponentPool = _world.GetPool<BoxColliderComponent>();
            _aiDestanationComponenPool = _world.GetPool<AIDestanationComponent>();
            _dropComponentPool = _world.GetPool<DropAssetComponent>();
        }


        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                if (_scriptableObjectPool.Get(entity).Value is EnemyData dataInit)
                {
                    ref LoadPrefabComponent loadPrefabFromPool = ref _loadPrefabPool.Add(entity);

                    CreateEnemy(entity, dataInit);
                }
                _scriptableObjectPool.Del(entity);
            }
        }


        private void CreateEnemy(int entity, EnemyData dataInit)
        {
            for (int i = 0; i < _poolService.Capacity; i++)
            {
                var newEntity = _world.NewEntity();
                GameObject pooled = _poolService.Get(GameObjectsTypeId.Enemy);
                pooled.gameObject.GetComponent<EnemyActor>().AddEntity(newEntity);

                ref EnemySpawnComponent spawn = ref _enemySpawnComponentPool.Add(newEntity);
                ref TransformComponent transformComponent = ref _transformComponentPool.Add(newEntity);
                ref EnemyHealthComponent enemyHealth = ref _enemyHealthComponentPool.Add(newEntity);
                ref BoxColliderComponent enemyBoxColliderComponent = ref _enemyBoxColliderComponentPool.Add(newEntity);
                ref EnemyStartPositionComponent enemyStartPositionComponent =
                    ref _enemyStartPositionComponentPool.Add(newEntity);
                ref EnemyStartRotationComponent enemyStartRotationComponent =
                    ref _enemyStartRotationComponentPool.Add(newEntity);
                ref AIDestanationComponent aiDestanationComponent = ref _aiDestanationComponenPool.Add(newEntity);
                ref DropAssetComponent dropAssetComponent = ref _dropComponentPool.Add(newEntity);

                spawn.SpawnLenght = dataInit.CountForInstantiate;
                transformComponent.Value = pooled.gameObject.GetComponent<TransformView>().Transform;
                
                enemyHealth.HealthValue = dataInit.Lives;
                pooled.GetComponent<HealthView>().Value.size = new Vector2(enemyHealth.HealthValue, 1);

                enemyBoxColliderComponent.ColliderValue = pooled.GetComponent<BoxCollider>();

                aiDestanationComponent.AIDestinationSetter = pooled.gameObject.GetComponent<AIDestinationSetter>();

                var index = Extensions.GetRandomInt(0, dataInit.DropPrefabs.Count);
                dropAssetComponent.Drop = dataInit.DropPrefabs[index];

                enemyStartPositionComponent.Value = dataInit.StartPositions[index];
                enemyStartRotationComponent.Value = dataInit.StartRotation.FirstOrDefault();

                _poolService.Return(pooled);
            }

            _isEnemyPool.Del(entity);
            _loadPrefabPool.Del(entity);
        }
    }
}