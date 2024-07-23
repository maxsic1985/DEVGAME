using LeoEcsPhysics;
using Leopotam.EcsLite;
using UnityEngine;


namespace MSuhininTestovoe.Devgame
{
    public partial class TriggerSystem
    {
      

        private void TrapEnterToTrigger(IEcsSystems ecsSystems, EcsPool<OnTriggerEnter2DEvent> poolEnter)
        {
            foreach (var entity in _filterEnterToTrigger)
            {
                ref var eventData = ref poolEnter.Get(entity);
                var player = eventData.senderGameObject;
                var trapCollider = eventData.collider2D;

                if (player.GetComponent<PlayerActor>() != null
                    && trapCollider.GetComponent<TrapActor>() != null)
                {
                    var trapEntity = trapCollider.GetComponent<TrapActor>().Entity;
                    var playerEntity = player.GetComponent<PlayerActor>().Entity;

                    _sharedData.GetPlayerCharacteristic.GetLives.UpdateLives(0);

                    // if (item.DropType == drop.DropType)
                    // {
                    //     UpdateInventory(ref item, ref drop);
                    //    
                    //     Object.Destroy(dropCollider.gameObject);
                    //     _world.DelEntity(trapEntity);
                    //     poolEnter.Del(entity);
                    //     return;
                    // }


                    GameObject.Destroy(trapCollider.gameObject);
                    _world.DelEntity(trapEntity);
                    poolEnter.Del(entity);
                }
            }
        }
    }
}