using System;
using System.IO;
using UnityEngine;


namespace Livia
{
    public class SaveGameDataWriter
    {
        public string saveDataDirectoryPath = "";

        public string dataSaveFileName = "";

        public CharacterSaveData LoadCharacterDataFromJson()
        {
            string savePath = Path.Combine(saveDataDirectoryPath, dataSaveFileName);
            CharacterSaveData loadedSaveData = null;
            if (File.Exists(savePath))
            {
                try
                {
                    string saveDataToLoad = "";
                    using (FileStream stream = new FileStream(savePath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            saveDataToLoad = reader.ReadToEnd();
                        }
                    }
                    loadedSaveData=JsonUtility.FromJson<CharacterSaveData>(saveDataToLoad);

                }
                catch(Exception ex)
                {
                    Debug.Log("the file is blank, probably : " +ex.Message);
                }
            }
            else
            {
                Debug.Log("File Does Not Exist");
            }

            return loadedSaveData;
        }
        public void WriteCharacterDataToSaveFile(CharacterSaveData characterData)
        {
            //Creates a path to save our file
            string savePath = Path.Combine(saveDataDirectoryPath,dataSaveFileName);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                Debug.Log("SAVE PATH = "+ savePath);

                //serialize to json
                string dataToStore = JsonUtility.ToJson(characterData,true);

                //write file
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.Log("ERROR  WHILE TRYING TO SAVE DATA GAME COULD NOT BE SAVED: "+ ex.Message);
            }

        }

        public void DeleteSaveFile()
        {
            File.Delete(Path.Combine(saveDataDirectoryPath, dataSaveFileName));
        }

        public bool CheckIfSaveFileExists()
        {
            if (File.Exists(Path.Combine(saveDataDirectoryPath, dataSaveFileName))) return true;
            else return false;
        }
    }
}
