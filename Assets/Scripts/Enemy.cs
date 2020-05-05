using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject projectile;
	public float health;
	public float shootingRate;
	public float projectileSpeed;
	public float shotsPerSeconds;
	public AudioClip destroy;
	public AudioClip enemyShoot;

	private int scoreValue = 100;
	private ScoreKeeper scoreKeeper;

	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	void OnTriggerEnter2D(Collider2D col){
		Projectile fireball = col.gameObject.GetComponent<Projectile>();
		if(fireball != null){
			health -= fireball.getDamage();
			fireball.hit();
			scoreKeeper.Score(scoreValue);
			if(health <= 0){
				Destroy(gameObject);
				scoreKeeper.Score(scoreValue+50);
				AudioSource.PlayClipAtPoint(destroy,transform.position);
			}
		}
	}
	
	void Update () {
		float probability = Time.deltaTime * shotsPerSeconds;
		if(Random.value < probability){
			shoot();
		}
		
	}
	
	void shoot(){
		Vector3 position = transform.position + (Vector3.right * 0.9f);
		GameObject fireball = Instantiate(projectile,position,Quaternion.identity) as GameObject;
		
		fireball.rigidbody2D.velocity = new Vector3(0,projectileSpeed,0);
		AudioSource.PlayClipAtPoint(enemyShoot,transform.position);
	}
	
}




