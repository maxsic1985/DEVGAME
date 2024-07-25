﻿using LeoEcsPhysics;
using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MSuhininTestovoe.Devgame
{
    public class RestartSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsPool<IsRestartComponent> _isRestartPool;
        private EcsPool<OnTriggerEnter2DEvent> trig;
        private EcsFilter _filter;
        private EcsFilter _filterTriggerEnter;
        private PlayerSharedData _sharedData;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _sharedData = systems.GetShared<SharedData>().GetPlayerSharedData;
            _filter = world.Filter<IsRestartComponent>().End();
            _filterTriggerEnter = world.Filter<OnTriggerEnter2DEvent>().End();
            _isRestartPool = world.GetPool<IsRestartComponent>();
            trig = world.GetPool<OnTriggerEnter2DEvent>();
        }


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                var timeServise = Service<ITimeService>.Get();
              timeServise.Resume();
                _sharedData.GetPlayerCharacteristic.LoadInitValue();
                
                systems.GetWorld().GetPool<IsManagePlayerStatsComponent>().Add(entity)
                    .dataAction = DataManageEnumType.Load;
                
               Application.LoadLevelAsync((int)SceeneType.MAIN);
               _isRestartPool.Del(entity);
           
                foreach (var trigEntity in _filterTriggerEnter)
                {
                    trig.Del(trigEntity);
                }
            }
        }

     
    }
}