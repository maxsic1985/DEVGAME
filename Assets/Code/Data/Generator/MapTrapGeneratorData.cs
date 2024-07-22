using UnityEngine;
using UnityEngine.AddressableAssets;


namespace MSuhininTestovoe.Devgame
{
    [CreateAssetMenu(fileName = nameof(MapTrapGeneratorData),
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(MapTrapGeneratorData))]
    public class MapTrapGeneratorData : ScriptableObject
    {
        [Header("Prefabs:")]
        public AssetReferenceGameObject DeathTrap;

        [Header("Preference:")] public int Count;
    }
}

