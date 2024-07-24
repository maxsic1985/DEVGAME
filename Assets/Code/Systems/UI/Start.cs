using UnityEngine;


namespace MSuhininTestovoe.Devgame
{
    public class Start :MonoBehaviour
    {
        public  void OnClickStart(int sceene)
        {
            Application.LoadLevelAsync(sceene);
        }
    }
}