using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiranha : MonoBehaviour {
    private float posicaoY = 0f;
    // Use this for initialization
    void Start () {
        Destroy(gameObject, 4f);
        float randomY = Random.Range(350f,550f);
        float randomX = Random.Range(-2f,2f);
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(randomX, randomY));
        posicaoY = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        if(!GameController.emPausa)
        {
            if(gameObject.GetComponent<Rigidbody2D>().bodyType==RigidbodyType2D.Static)
            {
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        if (posicaoY > transform.position.y)
        { //Descendo
            transform.eulerAngles = new Vector3(0, 0, -270);
        }
        }else{
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        // posicaoY = transform.position.y;

    }
}
