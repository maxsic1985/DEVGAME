using UnityEngine;
using UnityEngine.AddressableAssets;


namespace MSuhininTestovoe.Devgame
{
    [CreateAssetMenu(fileName = nameof(InventoryData),
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(SlotData))]
    public class SlotData : ScriptableObject
    {
        [Header("Slot")] 
        public AssetReferenceGameObject SlotItem; 
     

    }
}