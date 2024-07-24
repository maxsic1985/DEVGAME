using Leopotam.EcsLite;

namespace MSuhininTestovoe.Devgame
{


    public sealed class SoundSystems
    {
        public SoundSystems(EcsSystems systems)
        {
            systems
                .Add(new SoundLoadSystem())
                .Add(new SoundInitSystem())
                .Add(new SoundCatchSystem())
                .Add(new SoundMusicSwitchSystem())
                .Add(new SoundBuildSystem());

        }
    }
}