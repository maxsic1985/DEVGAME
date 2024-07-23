using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace MSuhininTestovoe.Devgame
{
    public struct TrapGeneratorComponent
    {
        public TrapType Type;
        public int DeathCount;
        public int DeathSizeArea;
        public int SlowCount;
        public int SlowSizeArea;
    }
}