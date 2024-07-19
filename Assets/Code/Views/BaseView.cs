using Leopotam.EcsLite;
using UnityEngine;



namespace MSuhininTestovoe.Devgame
{
    public abstract class BaseView : MonoBehaviour
    {
        protected EcsWorld World;
        protected int Entity;

        public virtual void Init(EcsWorld world, int entity)
        {
            World = world;
            Entity = entity;
        }

    }
}