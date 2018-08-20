using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUITelaInicial : MonoBehaviour {
    public GUISkin skinBT;
    public UnityEngine.UI.Button btPlay;
    public AudioSource gerenciador;
    public AudioClip soundClique;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void funJogar()
    {
        gerenciador.PlayOneShot(soundClique);
        gerenciador.volume = 0.08f;
        SceneManager.LoadScene("Loading");
    }
    public void funCredito()
    {
        gerenciador.PlayOneShot(soundClique);
        gerenciador.volume = 0.08f;
        SceneManager.LoadScene("TelaCreditos");
    }

    public void funSair()
    {
        Application.Quit();
    }
}
