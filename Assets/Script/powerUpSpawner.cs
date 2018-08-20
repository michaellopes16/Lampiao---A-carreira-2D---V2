using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpSpawner : MonoBehaviour {

	// Use this for initialization
	public GameObject[] powerUps;
	public float delay;
	private float innateDelay;
	private int QUARTINHO = 0;
	private int GARRAFA = 1;
	private int PEIXEIRA = 2;
	private int ESPINGARDA = 3;
	private int MANDACARU = 4;
	private int lastPowerUp;
	public PLayerController pLayerController;
	public GameObject player;

	void Start () {
		innateDelay = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameController.emPausa)
		{
		innateDelay += Time.deltaTime / delay;
		if(innateDelay >= 1)
		{
			
			int chosenPowerUp = Mathf.RoundToInt(Random.Range(0,4f));
			if((chosenPowerUp == ESPINGARDA && pLayerController.temEspingarda) || (chosenPowerUp == PEIXEIRA && pLayerController.temPeixeira))
			{
				chosenPowerUp = GARRAFA;
			}


			Vector3 pj = GameObject.Find("Colisor").transform.position;

			float x = pj.x + 13f;
			float y = pj.y + 5f;
			float speed = pLayerController.getDifficultyLevel();

			addPowerUp( x, y , chosenPowerUp, speed);
		
			innateDelay = 0;
		}
		}
	}

	private void addPowerUp(float x, float y, int powerUp, float speed){
		GameObject pUp = (GameObject) Instantiate(powerUps[powerUp]);
		pUp.transform.position = new Vector2(x,y);
		pUp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		pUp.GetComponent<MoveObjeto>().speed = -speed;
		pUp.name = powerUps[powerUp].name;
	}
}
