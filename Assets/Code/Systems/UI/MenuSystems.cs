using Leopotam.EcsLite;

namespace MSuhininTestovoe.Devgame
{
    public class MenuSystems
    {
        public MenuSystems(EcsSystems systems)
        {
            systems
               
                .Add(new DeathMenuLoadSystem())
                .Add(new DeathMenuInitSystem())
                .Add(new DeathMenuSystem())
                .Add(new DeathMenuCallBackSystem())
                .Add(new DeathMenuBuildSystem());
            
            systems
                .Add(new InventoryLoadSystem())
               .Add(new SlotsLoadSystem())
                .Add(new SlotsInitSystem())
                .Add(new InventoryInitSystem())
                .Add(new InventoryCallBackSystem())
                .Add(new InventoryBuildSystem());
        }
    }
}