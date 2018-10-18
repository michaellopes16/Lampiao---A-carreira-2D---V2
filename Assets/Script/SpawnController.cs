using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public GameObject [] barreiraPrefab;
    
    public static List<GameObject> prefabsEmCena;
   // public float intervaloBarreira;
    private float currentTime;
    private int posicao;
    private float y;
    public static int tamanhoDonome;

    public static List<int> indicesRemovidos;
    int indicePrefab;


    /// <summary>
    ///  Variável pra chamar o delegado
    /// </summary>
     public static bool chamarDelegado;

    bool criou;
    int indiceAux;
	int indiceAnterior;
    public static int quantColisoes;
    private int lastPrefabIndex = 0;
    // Use this for initialization
    void Start () {
        //Modificado em 21/11/17 --> 19:44
        chamarDelegado = false;

        quantColisoes = 0;
        indiceAux = 1;
        currentTime = 0;
        tamanhoDonome = 6;
        prefabsEmCena = new List<GameObject>();
        indicesRemovidos = new List<int>();
	}
	// Ajeitar criação do chão...
	// Update is called once per frame
	void Update () {
     
        if (PLayerController.colidiuComOBonus && quantColisoes == 0)
        {
          //  Debug.Log("Quantidade de colisoes antes: " + quantColisoes);
            removerPrefabe();
           
          //  Debug.Log("Quantidade de colisoes depois: "+quantColisoes);
            
        }
    }

   public void criarObj(float velocidade)
    {
        
        //  Debug.Log("Dentro do criar objeto: "+ prefabsEmCena.Count);
        if (prefabsEmCena.Count < 2)
        {
            if(!PLayerController.colidiuComOBonus)
                quantColisoes = 0;
			indiceAnterior = indicePrefab;
            indicePrefab = RamdomPrefabIndex();

			if (indiceAnterior == indicePrefab) {
				Debug.Log ("Indices iguais");
				indicePrefab += 1;
			} 
				indicesRemovidos.Add (indicePrefab);
				// Debug.Log("Adicionou um indice: " + indicesRemovidos.Count+" -- Indice adicionado:"+indicePrefab);
				barreiraPrefab [indicePrefab].GetComponent<MoveObjeto> ().speed = velocidade;

				GameObject tempPrefab = Instantiate (barreiraPrefab [indicePrefab]) as GameObject;
				prefabsEmCena.Add (tempPrefab);
			
		}
    }
    public void removerPrefabe()
    {
       // Debug.Log("Quantidade de Indices removidos: "+ indicesRemovidos.Count);
         //   foreach (int i in indicesRemovidos) {
            //    Debug.Log("Indices do remover indice:"+i+"Colidiu"+ PLayerController.colidiuComOBonus);
        //if (i >= 16 && PLayerController.colidiuComOBonus )
        if(PLayerController.colidiuComOBonus)
        {
                    if (indiceAux <= 5)
                    {
                        quantColisoes++;
                        PLayerController.colidiuComOBonus = false;
                       // Debug.Log("Tamanho do array:" + barreiraPrefab.Length + "Indice i" + indicePrefab + "nome" + barreiraPrefab[indicePrefab].gameObject.name);
                        // Debug.Log("Ultimo prefabe do array:"+ barreiraPrefab[barreiraPrefab.Length - 1].gameObject.name);
                        barreiraPrefab[indicePrefab] = barreiraPrefab[barreiraPrefab.Length - indiceAux];
                        indiceAux++;
                        // barreiraPrefab[barreiraPrefab.Length - 1] = null;
                        // indicesRemovidos.Add(indicePrefab);
                        //tamanhoDonome--;
                        Debug.Log("Novo prefab da posição: " + indicePrefab + " :" + barreiraPrefab[indicePrefab].gameObject.name);
                       // Debug.Log("Tamanho do Nome:" + tamanhoDonome);
                       
                    }
                    else
                    {
                        PLayerController.colidiuComOBonus = false;
                        tamanhoDonome =5;
                    }

        }

          //  }
          

    }
    private int RamdomPrefabIndex()
    {
        if (barreiraPrefab.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;

        while (randomIndex == lastPrefabIndex)
        {
            if (PLayerController.difficultyLevel >= 3.0f && PLayerController.difficultyLevel < 4.0f)
            {

                randomIndex = Random.Range(3, barreiraPrefab.Length - tamanhoDonome + 1);
				//randomIndex = Random.Range(0,4);
              //  randomIndex = barreiraPrefab.Length - tamanhoDonome;
                Debug.Log("Entrou no Difficulty Level>3: Indice: " + randomIndex + " ---- Limite Superior:" + (barreiraPrefab.Length - tamanhoDonome + 1));
            }
            else if (PLayerController.difficultyLevel >= 4.0f)
            {
                if (!chamarDelegado && !PLayerController.TaComJumento)
                {
                    chamarDelegado = true;
                }
                randomIndex = Random.Range(10, barreiraPrefab.Length - tamanhoDonome + 1);
				//randomIndex = Random.Range(0,4);
				// randomIndex = barreiraPrefab.Length - tamanhoDonome;+
                Debug.Log("Entrou no Difficulty Level>4: Indice: " + randomIndex + " ---- Limite Superior:" + (barreiraPrefab.Length - tamanhoDonome + 1));
            }
            else
            {
                randomIndex = Random.Range(0, barreiraPrefab.Length - tamanhoDonome);
				//randomIndex = Random.Range(0,4);
				Debug.Log("Entrou no Difficulty < 3 -- Indice: " + randomIndex + " ---- Limite Superior:" + (barreiraPrefab.Length - tamanhoDonome ));
            }
        }
          
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
