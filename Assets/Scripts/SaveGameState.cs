using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaveGameFree
{

	public class SaveGameState : MonoBehaviour {

		public GameState gameState;
		public string fileName = "gameData";

		/// <summary>
		/// Use awake function to initialize our game save and load.
		/// </summary>
		void Awake ()
		{
			Saver.OnSaved += Saver_OnSaved;
			Saver.OnLoaded += Saver_OnLoaded;

			// Initialize our game data
			gameState = new GameState ();

			// Initialize the Saver with the default configurations
			Saver.Initialize (FormatType.JSON);

			// Load game data after initialization
			//Load();
		}

		void Saver_OnLoaded (object obj)
		{
			Debug.Log ("Loaded Successfully: " + obj.ToString ());
		}

		void Saver_OnSaved (object obj)
		{
			Debug.Log ("Saved Succesfully: " + obj.ToString ());
		}

		public void SaveScene() {
			Saver.Save (gameState, fileName);
		}

		public void LoadScene() {
			gameState = Saver.Load<GameState> (fileName);
			SceneManager.LoadScene(gameState.sceneIndex);
		}

		public void LoadFirstScene() {
			Vector3 spawnPoint = new Vector3(100f,10f,90f);
			Quaternion rotation = Quaternion.identity;
			gameState = Saver.Load<GameState> (fileName);
			GameObject go = Instantiate(Resources.Load("personagem3animada"),spawnPoint,rotation) as GameObject;
			SceneManager.LoadScene(gameState.sceneIndex);
		}
	}
}