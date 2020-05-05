using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	public GameObject basicEnemy;
	public float width = 10f;
	public float height = 5f;
	public float speed;
	public float spawnDelay;
	
	private float xMin;
	private float xMax;
	private float yMin; 
	private float yMax;
	private float directionX = 1f; //positivo=derecha. negativo=izquierda

	// Use this for initialization
	void Start () {
		spawnEnemies();
		float distance = transform.position.z-Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		Vector3 topMost = Camera.main.ViewportToWorldPoint(new Vector3(0,1,distance));
		Vector3 bottomMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xMin = leftMost.x;
		xMax = rightMost.x;
		yMax = topMost.y;
		yMin = bottomMost.y;
	}
	
	// Update is called once per frame
	void Update () {
		moveEnemies();
		if(allMembersDead()){
			print("muertos");
			spawnUntilFull();
		}
	}
	
	void moveEnemies(){
		this.transform.position += directionX * (Vector3.right *speed * Time.deltaTime);
		if(this.transform.position.x + (0.5*width) >= xMax){
			directionX = -1;
		}
		else if(this.transform.position.x - (0.5*width) <= xMin){
			directionX = 1;
		}
		float newX = Mathf.Clamp(this.transform.position.x,xMin,xMax);
		float newY = Mathf.Clamp(this.transform.position.y,yMin,yMax);
		transform.position = new Vector3(newX,newY,this.transform.position.z);
	}
	
	public void spawnEnemies(){
		foreach (Transform row in transform){
			foreach (Transform enemyPosition in row.transform){
				GameObject newEnemy = Instantiate(basicEnemy,enemyPosition.transform.position,Quaternion.identity) as GameObject;
				newEnemy.transform.parent = enemyPosition.transform;
			}
		}
	}
	
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width,height,0));
	}
	
	bool allMembersDead(){
		foreach(Transform rowGameObject in transform){
			foreach(Transform positionGameObject in rowGameObject.transform){
				if(positionGameObject.childCount > 0){
					return false;
				}
			}
		}
		return true;
	}
	
	void spawnUntilFull(){
		Transform freePosition = nextFreePosition();
		if(freePosition){
			GameObject newEnemy = Instantiate(basicEnemy,freePosition.transform.position,Quaternion.identity) as GameObject;
			newEnemy.transform.parent = freePosition.transform;
		}
		if(nextFreePosition()){
			Invoke ("spawnUntilFull",spawnDelay);
		}
	}
	
	Transform nextFreePosition(){
		foreach(Transform rowGameObject in transform){
			foreach(Transform positionGameObject in rowGameObject.transform){
				if(positionGameObject.childCount == 0){
					return positionGameObject;
				}
			}
		}
		return null;
	}
}
