using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NomeController : MonoBehaviour {
    private float timeAuxNome;
    bool colidiuNome2;
    bool colidiuNome;
    private static float TEMPO_NOME = 4.0f;
    //List<Texture2D> letras;
    public Sprite []  texturasLetras;
    public GameObject[]  objLetras;
    public ParticleSystem particulaNome;

    int indiceGeral;
    // Use this for initialization
    void Start () {
        //letras = new List<Texture2D>();
        indiceGeral = 0;
        PlayerPrefs.SetInt("indiceGeral",indiceGeral);
        colidiuNome = false;
        colidiuNome2 = true;
        timeAuxNome = 0;
    }
	
	// Update is called once per frame
	void Update () {
        EfeitoNomeSertao();
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.gameObject.tag == "ColisorNome")
        {
            InstantiateParticula(particulaNome, coll.transform.position);
            colidiuNome = true;
            colidiuNome2 = false;
           // Debug.Log("Nome Colidiu: " + coll.gameObject.tag+ "Colidiu nome: "+colidiuNome);
        }
        else
        if (coll.gameObject.tag == "ColisorNome2")
        {
            colidiuNome2 = true;
            
          //  Debug.Log("Nome2 Colidiu: " + coll.gameObject.tag + " --- Colidiu nome2: " + colidiuNome);
        }
    }
    public void InstantiateParticula(ParticleSystem particula, Vector3 position)
    {
        ParticleSystem novaParticula = Instantiate(particula, position, Quaternion.identity);
        Destroy(novaParticula.gameObject, novaParticula.startLifetime);

    }
    public void EfeitoNomeSertao()
    {
    if (PLayerController.colidiuComOBonus2) {
            if (!colidiuNome)
            {
                // Descer Nome
                gameObject.GetComponent<Rigidbody2D>().mass = 0.1f;
                gameObject.GetComponent<Rigidbody2D>().gravityScale = -0.5f;
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -10));
            }
            else
            {
               

                timeAuxNome += Time.deltaTime;
                // Mudar sprite aqui --- Tentar melhor isso, pq ficar mudando sempre sem necessidade.
                if (indiceGeral < 6 )
                {
                    objLetras[indiceGeral].GetComponent<SpriteRenderer>().sprite = texturasLetras[indiceGeral];
                   
                }
               
                if (timeAuxNome >= TEMPO_NOME)
                {
                    gameObject.GetComponent<Rigidbody2D>().mass = 0.1f;
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = -0.5f;
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10));

                    if (colidiuNome2)
                    {
                        gameObject.GetComponent<Rigidbody2D>().mass = 0.0001f;
                        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                       // gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 0));
                        timeAuxNome = 0.0f;
                        colidiuNome = false;
                        PLayerController.colidiuComOBonus2 = false;
                        indiceGeral++;
                        PlayerPrefs.SetInt("indiceGeral", indiceGeral);
                    }
                }
                else
                {
                    // Debug.Log("Tempo Corrido" + timeAuxNome);
                    //gameObject.GetComponent<Rigidbody2D>().mass = 1f;
                    //gameObject.GetComponent<Rigidbody2D>().gravityScale = 8f;
                    gameObject.GetComponent<Rigidbody2D>().mass = 0.0001f;
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                }
            }
      }
    }
}
