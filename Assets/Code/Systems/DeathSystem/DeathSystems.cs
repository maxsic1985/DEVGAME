using Leopotam.EcsLite;


namespace MSuhininTestovoe.Devgame
{
    public class DeathSystems
    {
        public DeathSystems(EcsSystems systems)
        {
            systems
                .Add(new DeathSystem());
        }
    }
}