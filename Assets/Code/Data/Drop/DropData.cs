using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MSuhininTestovoe.Devgame
{


    [CreateAssetMenu(fileName = nameof(DropData),
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(DropData))]
    public class DropData : ScriptableObject
    {
        [Header("Prefabs:")]
        public List<AssetReferenceGameObject> DropPrefab;
    }
}

