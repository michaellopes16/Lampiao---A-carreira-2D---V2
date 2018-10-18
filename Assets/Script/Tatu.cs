using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tatu : MonoBehaviour {
	float speed = 1.0f;
	float x ;
	int i;
	bool mover = false;
	public PLayerController pLayerController;
	// Use this for initialization
	void Start () {
		x = this.transform.position.x;
		Invoke ("moverDireita",3);
		//camera = GameObject.Find("MainCamera").GetComponent<Camera>();
		pLayerController = GameObject.Find("Player").GetComponent<PLayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (mover) {
			x = this.transform.position.x;
			x += speed * Time.deltaTime * i;
			//x += pLayerController.getDifficultyLevel() * Time.deltaTime * i;
			transform.position = new Vector3 ( x, this.transform.position.y, this.transform.position.y);
		}
	}

	void moverDireita(){
		mover = true;
		//speed = 0.2f;
		gameObject.GetComponent<Animator> ().SetBool ("Walk", true );
		Invoke ("runAni", 2);
		transform.rotation = Quaternion.Euler (0,180,0);
		//x = 0;
		i = 1;
		//transform.rotation = Quaternion.Euler (0,0,30);
		//transform.position = new Vector3 (x,this.transform.position.y,this.transform.position.y);
		Invoke ("moverEsquerda",6);
	}

	void moverEsquerda(){
		//speed = 3.0f;
		Invoke ("runAni", 2);
		transform.rotation = Quaternion.Euler (0,0,0);
		//x = 0;
		i = -1; 
		//transform.rotation = Quaternion.Euler (0,0,30);

		Invoke ("moverDireita",6);
	}
		

	void runAni(){
	
		gameObject.GetComponent<Animator> ().SetBool ("Run", true );
		gameObject.GetComponent<Animator> ().SetBool ("Walk", false );
		Invoke ("runCancel", 3);
	}

	void runCancel(){
		gameObject.GetComponent<Animator> ().SetBool ("Run", false );
		gameObject.GetComponent<Animator> ().SetBool ("Walk", true );

	}
}
