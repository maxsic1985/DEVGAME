using Leopotam.EcsLite;



namespace MSuhininTestovoe.Devgame
{
    public class DropSystems
    {
        public DropSystems(EcsSystems systems)
        {
            systems
                .Add(new DropCreateSystem());
            
        }
    }
}