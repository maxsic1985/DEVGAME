using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;


namespace MSuhininTestovoe.Devgame
{
    public class DeathMenuSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _isPlayerDeathPool;
        private EcsFilter _filterToMainMenuPool;
        private EcsFilter _filterQuitMenuPool;
        private EcsFilter _filterRestartPool;
        private EcsFilter _isDeathMenu;
        private EcsWorld _world;

        private EcsPool<BtnToMainMenu> _toMainMenuPool;
        private EcsPool<BtnQuit> _quitMenuPool;
        private EcsPool<BtnRestart> _menuRestartpool;
        private EcsPool<IsMenu> _isDeathMenuPool;
        private EcsPool<IsRestartComponent> _isRestartPool;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _isPlayerDeathPool = _world.Filter<IsPlayerDeathComponent>().End();
            _isDeathMenu = _world.Filter<IsMenu>().End();
            _filterToMainMenuPool = _world.Filter<BtnToMainMenu>().Inc<IsMenu>().End();
            _filterQuitMenuPool = _world.Filter<BtnQuit>().Inc<IsMenu>().End();
            _filterRestartPool = _world.Filter<BtnRestart>().Inc<IsMenu>().End();

            _toMainMenuPool = _world.GetPool<BtnToMainMenu>();
            _quitMenuPool = _world.GetPool<BtnQuit>();
            _menuRestartpool = _world.GetPool<BtnRestart>();
            _isDeathMenuPool = _world.GetPool<IsMenu>();
            _isRestartPool = _world.GetPool<IsRestartComponent>();
        }


        public void Run(IEcsSystems systems)
        {
            foreach (var e in _isPlayerDeathPool)
            {
                foreach (var entity in _isDeathMenu)
                {
                    ref var menu = ref _isDeathMenuPool.Get(entity);
                    if (_isDeathMenuPool.Has(entity))
                    {
                        menu.MenuValue.SetActive(true);
                    }
                }
            }

            ToMainMenu();

            Quit();

            Restart();
        }

        private void Restart()
        {
            foreach (var entity in _filterRestartPool)
            {
                var menuPool = _world.GetPool<IsMenu>();
                ref var menu = ref menuPool.Get(entity);
                if (_quitMenuPool.Has(entity))
                {
                    menu.MenuValue.SetActive(false);
                }

                _isRestartPool.Add(entity);
                var timeServise = Service<ITimeService>.Get();
            //    timeServise.Resume();
                _quitMenuPool.Del(entity);
                _menuRestartpool.Del(entity);
            }
        }

        private void Quit()
        {
            foreach (var entity in _filterQuitMenuPool)
            {
                if (_quitMenuPool.Has(entity))
                {
                    Application.Quit();
                }
            }
        }

        private void ToMainMenu()
        {
            foreach (var entity in _filterToMainMenuPool)
            {
                if (_toMainMenuPool.Has(entity))
                {
                    Debug.Log($"Go To main menu");
                }
            }
        }
    }
}