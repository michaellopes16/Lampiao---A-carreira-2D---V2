using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjeto : MonoBehaviour {

    // Use this for initialization
    public float speed;

    public PLayerController pLayerController;
  
    private float x;
    private int lastPrefabIndex = 0;
    GameObject camera;
   // public GameObject obj;

    void Start () {
        //criou = false;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        pLayerController = GameObject.Find("Player").GetComponent<PLayerController>();
    }
	
	// Update is called once per frame
	void Update () {

      if(speed!=0){
        if (camera.GetComponent<GameController>().emJogo)
        {
            x = transform.position.x;

            x += -pLayerController.getDifficultyLevel() * Time.deltaTime;
            transform.position = new Vector3(x, transform.position.y, transform.position.z);

            if (x <= -22)
            {
                //Debug.Log("Quantidade de prefabs no  array: "+ SpawnController.prefabsEmCena.Count);
              

                Destroy(transform.gameObject);
               
                if (SpawnController.indicesRemovidos.Count >= 1)
                {
                    
                    SpawnController.indicesRemovidos.RemoveAt(SpawnController.indicesRemovidos.Count - 1);
                    //Debug.Log("Removeu um indice: " + SpawnController.indicesRemovidos.Count);
                }

                if (SpawnController.prefabsEmCena.Count == 2)
                    SpawnController.prefabsEmCena.RemoveAt(SpawnController.prefabsEmCena.Count-1);
            }
        }
	}
    }
}
