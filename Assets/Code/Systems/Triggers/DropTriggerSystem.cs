using LeoEcsPhysics;
using Leopotam.EcsLite;
using UnityEngine;


namespace MSuhininTestovoe.Devgame
{
    public partial class TriggerSystem
    {
        private EcsPool<BonusComponent> _dropPool;
        private EcsPool<ItemComponent> _itemSlotPool;
        private EcsFilter _filterItem;


        private void DropEnterToTrigger(IEcsSystems ecsSystems, EcsPool<OnTriggerEnter2DEvent> poolEnter)
        {
            foreach (var entity in _filterEnterToTrigger)
            {
                ref var eventData = ref poolEnter.Get(entity);
                var player = eventData.senderGameObject;
                var dropCollider = eventData.collider2D;

                if (player.GetComponent<PlayerActor>() != null
                    && dropCollider.GetComponent<BonusActor>() != null)
                {
                    var dropEntity = dropCollider.GetComponent<BonusActor>().Entity;
                    var playerEntity = player.GetComponent<PlayerActor>().Entity;


                    foreach (var itemEntity in _filterItem)
                    {
                        ref ItemComponent item = ref _itemSlotPool.Get(itemEntity);
                        ref BonusComponent bonus = ref _dropPool.Get(dropEntity);

                        if (item.Count > 0)
                        {
                            if (item.DropType == bonus.DropType)
                            {
                           
                               
                                Object.Destroy(dropCollider.gameObject);
                                _world.DelEntity(dropEntity);
                                poolEnter.Del(entity);
                                return;
                            }
                        }
                        else
                        {
                       
                            
                            Object.Destroy(dropCollider.gameObject);
                            _world.DelEntity(dropEntity);
                            poolEnter.Del(entity);
                            return;
                        }
                    }

                    GameObject.Destroy(dropCollider.gameObject);
                    _world.DelEntity(dropEntity);
                    poolEnter.Del(entity);
                }
            }
        }

     
    }

   
}