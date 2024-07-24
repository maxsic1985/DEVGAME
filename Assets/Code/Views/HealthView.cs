using UnityEngine;



namespace MSuhininTestovoe.Devgame
{
    public sealed class HealthView : BaseView
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
     

        public SpriteRenderer Value => _spriteRenderer;
       
    }
}