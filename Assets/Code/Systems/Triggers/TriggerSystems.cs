using LeoEcsPhysics;
using Leopotam.EcsLite;



namespace MSuhininTestovoe.Devgame
{
    public class TriggerSystems
    {
        public TriggerSystems(EcsSystems systems)
        {
            systems
                .Add(new TriggerSystem())
                .DelHerePhysics();
        }
    }
}