using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;

public class MoveDelagado : MonoBehaviour {
    public float speed;
    public Animator anime;

    private float x;
    float timeToBack;
    // Use this for initialization
    void Start () {
        timeToBack = 0;
        x = transform.position.x;
    }
	
	// Update is called once per frame
	void Update () {

        if (PLayerController.introducao)
        {
            chamarDelegado();
        }
        else
        {
            if (!SpawnController.chamarDelegado) {
                recuarDelegado();
             }
        }

        if (SpawnController.chamarDelegado)
        {
            chamarDelegado();

        }
         if (SpawnController.chamarDelegado && PLayerController.TaComJumento)
        {
            recuarDelegado();
            SpawnController.chamarDelegado = false;
        }



    }

    public void chamarDelegado()
    {
        anime.SetBool("Shoot", true);
        //Debug.Log("O delegado está vindo..");
        x = transform.position.x;

        if (x <= -6)
        {
            x += speed * Time.deltaTime;
            this.transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

    }

    public void recuarDelegado()
    {
        anime.SetBool("Shoot",false);
       // Debug.Log("O delegado está voltando..");
        if (x >= -9)
        {
            x -= speed * Time.deltaTime;
            this.transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

    }
}
