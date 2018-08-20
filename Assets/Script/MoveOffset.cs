using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffset : MonoBehaviour {

    private Material currentMaterial;
    public float speed;
    private float offset;
    GameObject camera;
    // Use this for initialization
    void Start () {
        currentMaterial = GetComponent<Renderer>().material;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }
	
	// Update is called once per frame
	void Update () {

        if (camera.GetComponent<GameController>() != null)
        {
            if (camera.GetComponent<GameController>().emJogo || Application.loadedLevelName == "TelaInicial")
            {

                offset += speed * Time.deltaTime;
                currentMaterial.SetTextureOffset("_MainTex", new Vector2(offset, 0));

            }
        }
	}
}
