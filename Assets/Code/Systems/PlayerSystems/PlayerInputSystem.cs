using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace MSuhininTestovoe.B2B
{
    public class PlayerInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsPool<PlayerInputComponent> _playerInputComponentPool;
        private int _entity;
        private EcsFilter _filter;

        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsPlayerComponent>()
                .Inc<TransformComponent>()
                .End();
            _playerInputComponentPool = _world.GetPool<PlayerInputComponent>();
        }

        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref PlayerInputComponent playerInputComponent = ref _playerInputComponentPool.Get(entity);
                playerInputComponent.Horizontal = Input.GetAxisRaw("Horizontal");// Input.GetAxis("Horizontal"); 
                playerInputComponent.Vertical =Input.GetAxis("Vertical");

               
            }
        }
    }
}