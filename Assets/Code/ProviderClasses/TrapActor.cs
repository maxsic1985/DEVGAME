using UnityEngine;

namespace MSuhininTestovoe.Devgame
{
    public class TrapActor : Actor
    {
        [SerializeField] private TrapType trapType;
        public TrapType TrapType => trapType;
        public override void Handle()
        {
             
        }
        
    }
}