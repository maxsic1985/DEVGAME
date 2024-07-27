using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MSuhininTestovoe.Devgame
{


    [CreateAssetMenu(fileName = nameof(BonusData),
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(BonusData))]
    public class BonusData : ScriptableObject
    {
        [Header("Prefabs:")]
        public List<AssetReferenceGameObject> DropPrefab;
    }
}

