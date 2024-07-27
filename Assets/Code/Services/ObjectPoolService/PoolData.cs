using System.ComponentModel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace MSuhininTestovoe.Devgame
{
    [CreateAssetMenu(menuName = "Pool/PoolData", fileName = "New PoolData", order = 51)]
    public class PoolData : ScriptableObject
    {
        [FormerlySerializedAs("PooledObjectType")]
        public GameObjectsTypeId _gameObjectsTypeId;

        public AssetReferenceGameObject PooledObject;
        [Range(500, 500)] public int Capacity = 500;
    }
}