using Leopotam.EcsLite;
using MSuhininTestovoe.B2B;


namespace MSuhininTestovoe.Devgame
{
    public class BonusSystems
    {
        public BonusSystems(EcsSystems systems)
        {
            systems
                .Add(new BonusLoadSystem())
                .Add(new BonusRespawnSystem())
                .Add(new BonusInitSystem());
        }
    }
}