using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using MSuhininTestovoe.Devgame;
using UnityEngine.Scripting;


namespace MSuhininTestovoe
{
    sealed class InventoryCallBackSystem : EcsUguiCallbackSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<BtnQuit> _quitBtnCommandPool;
        private EcsPool<IsInventory> _isInventoryPool;
        private EcsPool<IsBonusComponent> _isDropComponentPool;
        private EcsPool<ItemComponent> _itemComponentPool;
        private int _selectedEntity;
        private SlotView _selectedSlot;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsInventory>().End();
            _quitBtnCommandPool = _world.GetPool<BtnQuit>();
            _isInventoryPool = _world.GetPool<IsInventory>();
            _isDropComponentPool = _world.GetPool<IsBonusComponent>();
            _itemComponentPool = _world.GetPool<ItemComponent>();
        }


        [Preserve]
        [EcsUguiClickEvent(UIConstants.BTN_SHOW_INVENTORY, WorldsNamesConstants.EVENTS)]
        void OnClickShowInventory(in EcsUguiClickEvent e)
        {
            foreach (var entity in _filter)
            {
                ref IsInventory inv = ref _isInventoryPool.Get(entity);
                inv.Value.SetActive(!inv.Value.activeSelf);
            }
        }

      

        [Preserve]
        [EcsUguiClickEvent(UIConstants.BTN_SELECT_SLOT, WorldsNamesConstants.EVENTS)]
        void OnClickItem(in EcsUguiClickEvent e)
        {
            _selectedSlot = e.Sender.gameObject.GetComponent<SlotView>();
            _selectedEntity = _selectedSlot.Entity;
        }

        private void UpdateInventory(ref ItemComponent item)
        {
            item.Count -= 1;
            if (item.Count == 0)
            {
                item.Prefab = null;
                item.Sprite.sprite = null;
                item.DropType = DropType.EMPTY;
                item.CountText.text = "";
            }
            else
            {
                item.CountText.text = item.Count.ToString();
            }
        }
    }
}