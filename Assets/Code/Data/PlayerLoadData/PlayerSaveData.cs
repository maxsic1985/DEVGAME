using System;
using UnityEngine;


namespace MSuhininTestovoe.Devgame
{


    [Serializable]
    public sealed class PlayerSaveData
    {
        public int coins;

        public PlayerSaveData()
        {
            coins = 0;
        }
    }
}