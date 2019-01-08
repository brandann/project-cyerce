using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public static class SaveLoad
{
	public static Game SavedGame = new Game();
	private const string FileName = "/ProjectAegiresSavedGame.gd";
	
	public static void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + FileName);
		bf.Serialize(file, SaveLoad.SavedGame);
		file.Close();
		//Debug.Log("Game Saved");
	}
	
	public static void Load()
	{
        SavedGame = new Game();
		if(File.Exists(Application.persistentDataPath + FileName)) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + FileName, FileMode.Open);
			SaveLoad.SavedGame = (Game)bf.Deserialize(file);
			file.Close();
		}
	}
	
	public static void Reset()
	{
		NewGame();
		Save();
	}

    public static void NewGame()
    {
        SavedGame = new Game();
    }
}
