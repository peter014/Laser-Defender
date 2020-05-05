using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float damage;
	
	public float getDamage(){
		return damage;
	}
	
	public void hit(){
		Destroy(gameObject);
	}

}
