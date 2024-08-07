﻿using Leopotam.EcsLite;
using UnityEngine;



namespace MSuhininTestovoe.Devgame
{
    public class PlayerInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ScriptableObjectComponent> _scriptableObjectPool;
        private EcsPool<LoadPrefabComponent> _loadPrefabPool;
        private EcsPool<PlayerStartPositionComponent> _playerStartPositionComponentPool;
        private EcsPool<SpeedVectorComponent> _speedVectorComponentPool;
        private EcsPool<PlayerInputComponent> _playerInputComponentPool;

        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsPlayerComponent>().Inc<ScriptableObjectComponent>().End();
            _scriptableObjectPool = _world.GetPool<ScriptableObjectComponent>();
            _loadPrefabPool = _world.GetPool<LoadPrefabComponent>();
            _playerStartPositionComponentPool = _world.GetPool<PlayerStartPositionComponent>();
            _speedVectorComponentPool = _world.GetPool<SpeedVectorComponent>();
            _playerInputComponentPool = _world.GetPool<PlayerInputComponent>();
        }

        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                if (_scriptableObjectPool.Get(entity).Value is PlayerControlData dataInit)
                {
                    ref LoadPrefabComponent loadPrefabFromPool = ref _loadPrefabPool.Add(entity);
                    loadPrefabFromPool.Value = dataInit.Player;

                    ref PlayerStartPositionComponent playerStartPositionComponent =
                        ref _playerStartPositionComponentPool.Add(entity);
                    playerStartPositionComponent.Value = dataInit.StartPosition;

                    ref SpeedVectorComponent speedVectorComponent = ref _speedVectorComponentPool.Add(entity);
                    _playerInputComponentPool.Add(entity);
                }
                _scriptableObjectPool.Del(entity);
            }
        }
    }
}