using System;
using UnityEngine;


namespace MSuhininTestovoe.Devgame
{
    [Serializable]
    public sealed class PlayerSaveData
    {
        private int _coins;
        public int _bestResult;

        public PlayerSaveData()
        {
            _coins = 0;
          //  _bestResult = 0;
        }

        public int Coins
        {
            get => _coins;
            set => _coins = value;
        }

        public int BestResultRead => _bestResult;

        public int BestResult()
        {
            _bestResult = _coins > _bestResult ? _coins : _bestResult;
            Debug.Log(_bestResult);
            return _bestResult;
        }
    }
}