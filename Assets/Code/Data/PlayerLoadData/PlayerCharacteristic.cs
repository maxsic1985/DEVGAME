using System;
using UnityEngine;



namespace MSuhininTestovoe.Devgame
{
    [Serializable]
    public sealed class PlayerCharacteristic
    {
        [SerializeField] private int _id;
        [SerializeField] private string _playerName;
        [SerializeField] private Sprite _icon;

        [SerializeField] private float _baseSpeed;
        [SerializeField] private float _currentSpeed;
        [SerializeField] private float _baseRotateSpeed;
        [SerializeField] private int _baseScore;
        [SerializeField] private int _currentScore;
        [SerializeField] private float _rayDistance;
        [SerializeField] private Transform _transform;
        

        [SerializeField] private PlayerLivesCharacteristic _playerLivesCharacteristic;

        public PlayerLivesCharacteristic GetLives => _playerLivesCharacteristic;

        public float Speed => _currentSpeed;
        public int CurrentCoins => _currentScore;
        public float RotateSpeed => _baseRotateSpeed;
        public float RayDistance => _rayDistance;

        public Transform Transform=>_transform;
        
        public PlayerCharacteristic(PlayerCharacteristic playerCharacteristic)
        {
            _id = playerCharacteristic._id;
            _playerName = playerCharacteristic._playerName;
            _icon = playerCharacteristic._icon;
            _baseSpeed = playerCharacteristic._baseSpeed;
            _playerLivesCharacteristic = playerCharacteristic._playerLivesCharacteristic;
        }

        public int UpdateScore(int value)
        {
            return _currentScore = value;
        }
        public int AddScore(int value)
        {
            return UpdateScore(_currentScore + value);
        }

        public float SetSpeed(float value)
        {
            return _currentSpeed = value;
        }
        
        


        public void LoadInitValue()
        {
            _currentScore = _baseScore;
            _currentSpeed = _baseSpeed;
            _playerLivesCharacteristic.LoadInitValue();
        }
    }
}