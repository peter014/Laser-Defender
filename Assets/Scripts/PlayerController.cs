using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public GameObject projectile;
	public float speed;
	public float projectileSpeed;
	public float shootingRate;
	public float health;
	public AudioClip laserShoot;
	public AudioClip death;

	private float xMin;
	private float xMax;
	private float yMin; 
	private float yMax;
	
	// Use this for initialization
	void Start () {
		float distance = transform.position.z-Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		Vector3 topMost = Camera.main.ViewportToWorldPoint(new Vector3(0,1,distance));
		Vector3 bottomMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xMin = leftMost.x + 1.2f;
		xMax = rightMost.x - 1.2f;
		yMax = topMost.y * 0.2f;
		yMin = bottomMost.y + 1.5f;
	}
	
	void OnTriggerEnter2D(Collider2D col){
		Projectile fireball = col.gameObject.GetComponent<Projectile>();
		if(fireball != null){
			health -= fireball.getDamage();
			fireball.hit();
			if(health <= 0){
				AudioSource.PlayClipAtPoint(death,transform.position);
				Destroy(gameObject);
				LevelManager manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
				manager.LoadLevel("Loose Screen");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		moveShip();
		if (Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("shoot",0.0000001f,shootingRate);
			//shoot();
		}
		if (Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("shoot");
			//shoot();
		}
	}
	
	void shoot(){
		Vector3 position = transform.position + (Vector3.right * 0.9f);
		GameObject fireball = Instantiate(projectile,position,Quaternion.identity) as GameObject;
		
		fireball.rigidbody2D.velocity = new Vector3(0,projectileSpeed,0);
		AudioSource.PlayClipAtPoint(laserShoot,transform.position);
	}
	
	void moveShip(){
		if (Input.GetKey(KeyCode.RightArrow)){
			//this.transform.position += new Vector3(speed * Time.deltaTime,0f,0f);
			this.transform.position += Vector3.right *speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.LeftArrow)){
			//this.transform.position += new Vector3(-speed * Time.deltaTime,0f,0f);
			this.transform.position += Vector3.left *speed * Time.deltaTime;
		}
		
		if (Input.GetKey(KeyCode.DownArrow)){
			//this.transform.position += new Vector3(0f,-speed * Time.deltaTime,0f);
			this.transform.position += Vector3.down *speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.UpArrow)){
			//this.transform.position += new Vector3(0f,speed * Time.deltaTime,0f);
			this.transform.position += Vector3.up *speed * Time.deltaTime;
		}
		//restrict to the gamespace
		float newX = Mathf.Clamp(this.transform.position.x,xMin,xMax);
		float newY = Mathf.Clamp(this.transform.position.y,yMin,yMax);
		transform.position = new Vector3(newX,newY,this.transform.position.z);
	}

}
