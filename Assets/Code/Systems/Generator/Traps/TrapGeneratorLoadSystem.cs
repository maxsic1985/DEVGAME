using Leopotam.EcsLite;
using UnityEngine;

namespace MSuhininTestovoe.Devgame
{
    public class TrapGeneratorLoadSystem: IEcsInitSystem
    {
        private EcsPool<TrapGeneratorComponent> _generatorPool;
        
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            
            _generatorPool = world.GetPool<TrapGeneratorComponent>();
            _generatorPool.Add(entity);

            var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
            ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
            loadFactoryDataComponent.AddressableName = AssetsNamesConstants.TRAPGENERATOR_DATA;
        }
    }
}