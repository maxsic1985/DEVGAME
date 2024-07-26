using System.IO;
using TMPro;
using UnityEngine;


namespace MSuhininTestovoe.Devgame
{
    public class StartGame : MonoBehaviour
    {
        public TMP_Text ScoreText;
        private PlayerSaveData _playerSaveData;

        private void Start()
        {
            string path = Application.persistentDataPath + "/PlayerStats.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                _playerSaveData = JsonUtility.FromJson<PlayerSaveData>(json);
                ScoreText.text = _playerSaveData.BestResultRead.ToString();
            }
        }

        public void OnClickStart(int sceene)
        {
            Application.LoadLevelAsync(sceene);
        }
    }
}