using UnityEngine;
using UnityEngine.AddressableAssets;


namespace MSuhininTestovoe.Devgame
{
    [CreateAssetMenu(fileName = nameof(MapGeneratorData),
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(MapGeneratorData))]
    public class MapGeneratorData : ScriptableObject
    {
        [Header("Prefabs:")]
        public AssetReferenceGameObject Unit;

        [Header("Preference:")]
        public int Height;
        public int Weight;
    }
}

