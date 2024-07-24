using UnityEngine;

namespace MSuhininTestovoe.Devgame
{
    public class InjectActor : MonoBehaviour
    {
        private void Awake()
        {
            IActor actor = GetComponent<IActor>();

            IHaveActor[] needActor = GetComponentsInChildren<IHaveActor>(true);

            foreach (IHaveActor na in needActor)
            {
                na.Actor = actor;
            }
        }
    }
}