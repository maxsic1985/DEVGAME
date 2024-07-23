using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using LeoEcsPhysics;
using LeopotamGroup.Globals;
using Pathfinding;
using UnityEngine;


namespace MSuhininTestovoe.Devgame
{
    public sealed class EcsStartup : MonoBehaviour
    {
        private EcsSystems _systems;
        private bool _hasInitCompleted;
        private ITimeService _timeService;
        [SerializeField] private EcsUguiEmitter uguiEmitter;
        [SerializeField] private PathfinderScan AI;
        
        
        private async void Start()
        {
            SharedData shared = new();
            await shared.Init();
            
            Application.targetFrameRate = 60;
            
            shared.GetPlayerSharedData.GetPlayerCharacteristic.LoadInitValue();
            
            IPoolService poolService = new PoolService();
            await poolService.Initialize();

            var world = new EcsWorld();
            _systems = new EcsSystems(world, shared);
            EcsPhysicsEvents.ecsWorld = world;

            new InitializeAllSystem(_systems, poolService);

            _systems
                .AddWorld(new EcsWorld(), WorldsNamesConstants.EVENTS)
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(WorldsNamesConstants.EVENTS))
#endif
                .InjectUgui(uguiEmitter, WorldsNamesConstants.EVENTS)
                .Inject(AI,WorldsNamesConstants.EVENTS)
                .Init();

            _hasInitCompleted = true;
        }

        private void Update()
        {
            if (_hasInitCompleted)
                _systems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                foreach (var worlds in _systems.GetAllNamedWorlds())
                {
                    worlds.Value.Destroy();
                }
                EcsPhysicsEvents.ecsWorld = null;
                _systems.GetWorld().Destroy();
                _systems = null;
            }
        }
    }
}