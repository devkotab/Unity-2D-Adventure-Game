﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public class GlobalController : MonoBehaviour {

	public static GlobalController Instance;
	public GameObject player;

	public PlayerStatistics savedPlayerData = new PlayerStatistics();
	public PlayerStatistics LocalCopyOfData;

	public bool IsSceneBeingLoaded = false;

	public int SceneID;
	public float PositionX, PositionY, PositionZ;
	public float HP;

	public void Save() { //note that Application.persistentDataPath is the default path location of save files for Unity3d. Calling on this allows this code to be multiplatform without worrying about special paths
		if (!Directory.Exists(Application.persistentDataPath + "Saves"))
			Directory.CreateDirectory(Application.persistentDataPath + "Saves");
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream saveFile = File.Create(Application.persistentDataPath + "Saves/save.gd");
		LocalCopyOfData = PlayerState.Instance.localPlayerData;
		formatter.Serialize(saveFile, LocalCopyOfData);
		saveFile.Close();
	}

	public void Load()
	{
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream saveFile = File.Open(Application.persistentDataPath + "Saves/save.gd", FileMode.Open);
		LocalCopyOfData = (PlayerStatistics)formatter.Deserialize(saveFile);
		saveFile.Close();
	}

	void Awake () //This singleton keeps the object this script is attached to from being destroyed when switching scenes
	{
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy (gameObject);
		}
	}
}