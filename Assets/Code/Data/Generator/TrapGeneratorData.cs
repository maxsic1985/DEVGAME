using UnityEngine;
using UnityEngine.AddressableAssets;


namespace MSuhininTestovoe.Devgame
{
    [CreateAssetMenu(fileName = nameof(TrapGeneratorData),
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(TrapGeneratorData))]
    public class TrapGeneratorData : ScriptableObject
    {
        [Header("Prefabs:")]
        public AssetReferenceGameObject DeathTrap;

        [Header("Preference:")] public int Count;
    }
}

