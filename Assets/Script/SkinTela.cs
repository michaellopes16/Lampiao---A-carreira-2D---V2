using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SkinTela : MonoBehaviour {

    public GUISkin skinBotao;
    public GUI botaoSair;
    public UnityEngine.UI.Text txtRecorde;
    public UnityEngine.UI.Text txtPontuacao;
    public UnityEngine.UI.Text txtMedalhas;
    public Texture2D[] texturasLetras;
    public UnityEngine.UI.Image[] objLetras;
    public UnityEngine.UI.Image[] objCartas;

    // Use this for initialization
    void Start () {
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            txtRecorde.text = PlayerPrefs.GetInt("recorde").ToString();
            txtPontuacao.text = PlayerPrefs.GetInt("pontuacao").ToString();
            txtMedalhas.text = PlayerPrefs.GetInt("quantMedalhas").ToString();
            int indiceGeral = PlayerPrefs.GetInt("indiceGeral");
            //int indiceGeral = 3;
            //Melhoras isso... Muita gambiarrado
            if (PlayerPrefs.GetInt("recordeMedalhas") >= 150)
            {
                objCartas[0].canvasRenderer.SetColor(new Color(255,255,255,255));
                if (PlayerPrefs.GetInt("recordeMedalhas") >= 250)
                {
                    objCartas[1].canvasRenderer.SetColor(new Color(255, 255, 255, 255));
                    if (PlayerPrefs.GetInt("recordeMedalhas") >= 300)
                    {
                        objCartas[2].canvasRenderer.SetColor(new Color(255, 255, 255, 255));

                        if (PlayerPrefs.GetInt("recordeMedalhas") >= 350)
                        {
                            objCartas[3].canvasRenderer.SetColor(new Color(255, 255, 255, 255));

                            if (PlayerPrefs.GetInt("recordeMedalhas") >= 400)
                            {
                                objCartas[4].canvasRenderer.SetColor(new Color(255, 255, 255, 255));
                                if (PlayerPrefs.GetInt("recordeMedalhas") >= 450)
                                {
                                    objCartas[5].canvasRenderer.SetColor(new Color(255, 255, 255, 255));
                                }
                            }
                        }
                    }
                }
            }
           

            for (int i=0;i<indiceGeral;i++) {
                     objLetras[i].canvasRenderer.SetTexture(texturasLetras[i]); 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            // int indiceGeral = 5;
            int indiceGeral = PlayerPrefs.GetInt("indiceGeral");
            for (int i = 0; i < indiceGeral; i++)
            {
                objLetras[i].canvasRenderer.SetTexture(texturasLetras[i]);
            }
        }
    }
    private void OnGUI()
    {
        GUI.skin = skinBotao;
            
   }
}
