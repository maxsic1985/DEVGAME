using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MSuhininTestovoe.Devgame
{
    [CreateAssetMenu(fileName = nameof(PlayerControlData),
        menuName = EditorMenuConstants.CREATE_PLAYER + nameof(PlayerControlData))]
    public class PlayerControlData : ScriptableObject
    {
        [Header("Prefabs:")]
        public AssetReferenceGameObject Player;

        [Header("Position:")] public Vector3 StartPosition;
    }
}