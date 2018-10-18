using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cassaco : MonoBehaviour {

	bool peido = false;

	[SerializeField]
	GameObject localPeido;
	[SerializeField]
	GameObject peidoCollider;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("peidoCassaco",7,7);
	}



	void peidoCassaco(){


		gameObject.GetComponent<Animator> ().SetBool ("Peido", true );
		Invoke("CriarPeido",0.5f);
		Invoke("peidoCancel",1.5f);
	
		//if(gameObject.GetComponent<Animator>().a){}

		//gameObject.transform.position
	
	}
	void peidoCancel(){
		gameObject.GetComponent<Animator> ().SetBool ("Peido", false );
	
	}
	void CriarPeido(){
		
		Instantiate (peidoCollider, new Vector3(localPeido.transform.position.x,localPeido.transform.position.y, localPeido.transform.position.z),Quaternion.identity);
		//Destroy (partPeido,2);
		//partPeido.transform.rotation = Quaternion.Euler (0,0,30);
	}
}
