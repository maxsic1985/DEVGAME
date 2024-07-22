using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MSuhininTestovoe.Devgame
{


    [CreateAssetMenu(fileName = nameof(CameraLoadData),
        menuName = EditorMenuConstants.CREATE_DATA_MENU_NAME + nameof(CameraLoadData))]
    public class CameraLoadData : ScriptableObject
    {
        [Header("Prefabs:")] public AssetReferenceGameObject Camera;
        [Header("Positions:")] public Vector3 StartPosition;
        [Header("Rotations:")] public Vector3 StartRotation;
        [Header("Ortographic Size:")] public float Size;
        [Range(0f, 1f)] public float CameraSmoothness;  
    }
}

