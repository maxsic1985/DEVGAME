using System;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace MSuhininTestovoe.B2B
{
    [Serializable]
    public struct Resource
    {
        public string ID;
        public string Name;
        public AssetReferenceSprite Sprite;
        public ScriptableObject AdditionalData;
    }
}