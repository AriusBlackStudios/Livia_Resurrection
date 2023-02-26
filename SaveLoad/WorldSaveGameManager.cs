using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Livia{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager instance;
        public PlayerManager player;
        //public GameObject loadingPanel;
        //public Slider loadingProgressBar;

      

        [Header("Save Data Writer")]
        SaveGameDataWriter saveGameDataWriter;

        [Header("Current Character Data")]

        //character slot A
        public CharacterSaveData currentCharacterData;
        [SerializeField] private string fileName="SaveGameFile";

        [Header("SAVE/LOAD")]
        [SerializeField] bool saveGame;
        [SerializeField] bool loadGame;


        private void Awake()
        {
            if (instance == null){

                instance = this;
            }
            else{
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            //load all possible character slots

        }

        private void Update()
        {
            if (saveGame)
            {
                saveGame = false;
                SaveGame();
            }
            else if (loadGame)
            {
                loadGame = false;
                LoadGame();
            }
        }

        //new game


        //save game
        public void SaveGame()
        {
            saveGameDataWriter = new SaveGameDataWriter();
            saveGameDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveGameDataWriter.dataSaveFileName = fileName;

            player.SaveCharacterDataToCurrentSaveData(ref currentCharacterData);

            saveGameDataWriter.WriteCharacterDataToSaveFile(currentCharacterData);

            Debug.Log("Saving Game...");
            Debug.Log("File Saved As: "+ fileName);
        }

        //load game

        public void LoadGame()
        {
            saveGameDataWriter = new SaveGameDataWriter();
            saveGameDataWriter.saveDataDirectoryPath=Application.persistentDataPath;
            saveGameDataWriter.dataSaveFileName = fileName;
            currentCharacterData = saveGameDataWriter.LoadCharacterDataFromJson();
            StartCoroutine(LoadWorldSceneAsync());
        }
        IEnumerator LoadWorldSceneAsync()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(0);
            //if (loadingPanel != null)
              //  loadingPanel.SetActive(true);

            while (!loadOperation.isDone)
            {
                float loadingProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);
               // if (loadingProgressBar != null)
               //     loadingProgressBar.value = loadingProgress;
                yield return null;

            }
            if (player == null)
                player = FindObjectOfType<PlayerManager>();

            player.LoadCharacterDataFromCurrentCharacterSaveData(ref currentCharacterData);

        }
    }
}
