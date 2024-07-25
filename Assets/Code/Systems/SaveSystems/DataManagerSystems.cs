using Leopotam.EcsLite;
using System.IO;
using UnityEngine;


namespace MSuhininTestovoe.Devgame
{
    public class DataManagerSystems
    {
        public DataManagerSystems(EcsSystems systems)
        {
            systems.Add(new DataManagerSystem())
                .Add(new DEBUG_TestDataManagerSystems());
        }
    }
}