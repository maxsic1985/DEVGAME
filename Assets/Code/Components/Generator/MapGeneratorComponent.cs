using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace MSuhininTestovoe.Devgame
{
    public struct MapGeneratorComponent
    {
        public int Height;
        public int Weight;

        public PathfinderScan PathfinderScan;
    }
}