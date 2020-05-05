using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name){
		//Brick.breakableCount = 0;
		Application.LoadLevel (name);
		//Ball.started = false;
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

	public void GameOver(string name){
		Debug.Log ("Game Over");
		Application.LoadLevel (name);
		//Destroy (gameObject);
	}

	public void loadNextLevel(){
		Application.LoadLevel(Application.loadedLevel + 1);
	}
	
	/*public void brickDestroyed(){
		if(Brick.breakableCount <= 0){
			loadNextLevel();
			Ball.started = false;
		}
	}*/
}
