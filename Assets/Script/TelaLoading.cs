using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using DG.Tweening;
public class TelaLoading : MonoBehaviour {
    public MeshRenderer mesh;
    float timeAux;
    private void Start()
    {
        timeAux = 0.0f;
    }
    // Use this for initialization
	void Update () {

        timeAux += Time.deltaTime;
        if (timeAux>= 3.5) {
          //  mesh.material.DOFade(3, "", 3);
            SceneManager.LoadScene("Fase1");
        }
	}
}
