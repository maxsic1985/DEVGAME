using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using UnityEngine.Scripting;


namespace MSuhininTestovoe.Devgame
{
    public class PlayerAtackSystem : EcsUguiCallbackSystem, IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsFilter _enemyFilter;
        private EcsPool<IsPlayerCanAttackComponent> _isCanAttackComponentPool;
        private EcsPool<HealthViewComponent> _enemyHealthViewComponentPool;
        private EcsPool<EnemyHealthComponent> _enemyHealthComponentPool;
        private EcsPool<PlayerInputComponent> _playerInputComponentPool;
        private int _entity;
        private IEcsSystems _systems;



        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world
                .Filter<IsPlayerCanAttackComponent>()
                .Inc<PlayerInputComponent>()
                .End();

            _enemyFilter = world
                .Filter<EnemyHealthComponent>()
                .Inc<HealthViewComponent>()
                .End();
            _enemyHealthViewComponentPool = world.GetPool<HealthViewComponent>();
            _enemyHealthComponentPool = world.GetPool<EnemyHealthComponent>();
            _isCanAttackComponentPool = world.GetPool<IsPlayerCanAttackComponent>();
            _playerInputComponentPool = world.GetPool<PlayerInputComponent>();
            _systems = systems;
        }


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                foreach (var enemyEntity in _enemyFilter)
                {
                    ref HealthViewComponent healthView = ref _enemyHealthViewComponentPool.Get(enemyEntity);
                    ref EnemyHealthComponent healthValue = ref _enemyHealthComponentPool.Get(enemyEntity);
                    if(healthView.Value==null) return;
                    
                    var currentHealh = healthValue.HealthValue;

                    healthView.Value.size = new Vector2(currentHealh, 1);
                    _entity = enemyEntity;
                }

                ref PlayerInputComponent playerInputComponent = ref _playerInputComponentPool.Get(entity);
                if (playerInputComponent.Fire) Attack();
            }
        }


        private void Attack()
        {
            ref EnemyHealthComponent healthValue = ref _enemyHealthComponentPool.Get(_entity);
            healthValue.HealthValue -= 1;
            AddHitSoundComponent(ref _systems, SoundsEnumType.FIRE);
            Debug.Log("fire");
        }

        private void AddHitSoundComponent(ref IEcsSystems systems, SoundsEnumType type)
        {
            var entity = SoundCatchSystem.sounEffectsSourceEntity;
            var sound = systems.GetWorld().GetPool<IsPlaySoundComponent>();
            if (sound.Has(entity)) return;

            ref var isHitSoundComponent = ref systems.GetWorld()
                .GetPool<IsPlaySoundComponent>()
                .Add(entity);
            isHitSoundComponent.SoundType = type;
        }
    }
}