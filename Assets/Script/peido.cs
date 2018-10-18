using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peido : MonoBehaviour {
	[SerializeField]
	ParticleSystem partPeido;
	float speed = 3.0f;
	float x ;
	// Use this for initialization
	void Start () {
		ParticleSystem p = Instantiate (partPeido,new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.y), Quaternion.identity) as ParticleSystem;
		Destroy (p, 2);
		Destroy (this.gameObject,2);
	}
	
	// Update is called once per frame
	void Update () {
		 x = this.transform.position.x;
		 x += speed * Time.deltaTime;
		//transform.rotation = Quaternion.Euler (0,0,30);
		transform.position = new Vector3 (x,this.transform.position.y,this.transform.position.y);
		//transform.position += Vector3.forward * Time.deltaTime * speed;
	}
}
