using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaBehavior : MonoBehaviour {

    public GameObject efeitoDestruirObjeto;
    public PLayerController player;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	 private void OnTriggerEnter2D(Collider2D collision)
    {
		Collider2D obj = collision;
		Debug.Log(obj.gameObject.layer);
		if(obj.gameObject.tag=="colisor"){


            GameObject tempPrefab = Instantiate(efeitoDestruirObjeto, collision.transform.position, Quaternion.identity) as GameObject;
            Destroy(tempPrefab, 1.0f);

            player.gerenciadorDeAudio.PlayOneShot(player.arrayDeSons[10]);
            player.gerenciadorDeAudio.volume = 0.6f;

            Destroy(gameObject);
			Destroy(obj.gameObject);
		}
	}
}
