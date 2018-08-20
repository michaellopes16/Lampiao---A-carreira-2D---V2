using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;


public class GameController : MonoBehaviour {

    public bool emJogo;
    public static bool emPausa;
    public GUISkin skinBTNao;
    public GUISkin skinBTSim;
    public GameObject player;
    public int vidas;
    public UnityEngine.UI.Text txtVidas;
    public int larguraBt;
    public int alturaBt;
   // public Animator anime;

    public AudioSource gerenciador;
    public AudioClip soundClique;
    float timeFim;
    // Use this for initialization

	//PowerUps//
	public PostProcessingProfile perfil;
	public DepthOfFieldModel.Settings blurProfile;
	public BloomModel.Settings bloomProfile;
	public float blurTimer;
	public int delay;
	public int contador;
    public GameObject TtelaBorrada;

    void Start () {
       // emJogo = true;
        vidas = 3;
        timeFim = 0.0f;
        larguraBt = 150;
        alturaBt = 100;
        if (SceneManager.GetActiveScene().name == "Fase1")
            txtVidas.text = vidas.ToString();

        if (SceneManager.GetActiveScene().name != "TelaInicial" && SceneManager.GetActiveScene().name != "GameOver")
        {
            //PowerUps//
            blurProfile = perfil.depthOfField.settings;
            blurProfile.kernelSize = DepthOfFieldModel.KernelSize.VeryLarge;
            blurProfile.aperture = 32;
            blurProfile.focalLength = 0.1f;
            blurTimer = 0f;
            perfil.depthOfField.settings = blurProfile;
            bloomProfile = perfil.bloom.settings;
            bloomProfile.bloom.threshold = 1f;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().name != "TelaInicial" && SceneManager.GetActiveScene().name != "GameOver")
        {

            if (!GameController.emPausa)
            {
                if (blurTimer >= 0)
                {
                    blurProfile = perfil.depthOfField.settings;
                    blurTimer -= Time.deltaTime / 3f;
                    float startAngle = Mathf.PI * 0.5f;
                    float progress = Mathf.Pow(((1f - blurTimer) * Mathf.PI), 2);
                    if (progress > Mathf.PI)
                        progress = Mathf.PI;
                    blurProfile.focalLength = Mathf.Sin(startAngle + progress) * 22.5f + 22.5f;
                    bloomProfile.bloom.intensity = blurTimer * 5;
                    perfil.depthOfField.settings = blurProfile;
                    perfil.bloom.settings = bloomProfile;
                }
            }
        }
     }

    private void OnGUI()
    {
        if (!emJogo && SceneManager.GetActiveScene().name != "TelaInicial")
        {

            GUI.skin = skinBTSim;
            if (!emPausa)
            {
                if (!PLayerController.introducao)
                {
                        if (SceneManager.GetActiveScene().name == "Fase1")
                        {
                                if (vidas <= 0)
                                {
                                    txtVidas.text = "" + 0;
                                    timeFim += Time.deltaTime;

                                        if (timeFim >= 3.0f)
                                        {
                                            SceneManager.LoadScene("GameOver");
                                            timeFim = 0.0f;
                                        }
                                }
                                else if(vidas!=0)
                                {
                                    txtVidas.text = vidas.ToString();
                                    timeFim += Time.deltaTime;
                                    if (timeFim >= 3.0f)
                                    {
                                        exibirPopUp();                                     
                                    }
                                }                      
                        }else
                        if (SceneManager.GetActiveScene().name == "Fase2")
                        {
                            if (vidas <= 0)
                            {
                                txtVidas.text = "" + 0;
                                timeFim += Time.deltaTime;

                                if (timeFim >= 3.0f)
                                {
                                    SceneManager.LoadScene("GameOver");
                                    timeFim = 0.0f;
                                }
                            }
                            else if (vidas != 0)
                            {
                                txtVidas.text = vidas.ToString();
                                timeFim += Time.deltaTime;
                                if (timeFim >= 3.0f)
                                {
                                    exibirPopUp();
                                }
                            }
                        }
                }
            }
            else
            {
                emJogo = false;
                GUI.TextArea(new Rect((Screen.width / 2) -100, (Screen.height / 2)-50, 200,100), "JOGO EM PAUSA");
            }
        }
       
    }
    public void exibirPopUp()
    {
        GUI.Box(new Rect((Screen.width / 2) - 300, (Screen.height / 2) - 100, 600, 350), "");

        if (GUI.Button(new Rect((Screen.width / 2) - ((larguraBt) / 2) - 100, (Screen.height / 2) - (alturaBt - 270) / 2, larguraBt, alturaBt), ""))
        {
            timeFim = 0.0f;
            gerenciador.PlayOneShot(soundClique);
            gerenciador.volume = 0.08f;
            player.transform.position = new Vector3(player.transform.position.x, 1.8f, player.transform.position.z);
            emJogo = true;
            PLayerController.IsImune = true;
            Debug.Log("Está imune?" + PLayerController.IsImune);
            vidas--;
            if (vidas <= 0)
            {
                txtVidas.text = "" + 0;
            }
            else
            {
                txtVidas.text = vidas.ToString();
            }
        }
        GUI.skin = skinBTNao;
        if (GUI.Button(new Rect((Screen.width / 2) - ((larguraBt) / 2) + 100, (Screen.height / 2) - (alturaBt - 270) / 2, larguraBt, alturaBt), ""))
        {
            gerenciador.PlayOneShot(soundClique);
            gerenciador.volume = 0.08f;
            SceneManager.LoadScene("TelaInicial");
        }
    }

	//PowerUps//

	public void aumentarVida(){
		vidas += 1;
		txtVidas.text = vidas.ToString ();	
	}

	public void garrafaUp(){
        //blurTimer += 1f;
        GameObject tempPrefab = Instantiate(TtelaBorrada, TtelaBorrada.transform.position, Quaternion.identity) as GameObject;
        tempPrefab.transform.localEulerAngles = new Vector3(90, 0, 0);
        Destroy(tempPrefab, 5.3f);
    }

    public void mudarFase2()
    {
        SceneManager.LoadScene("Fase2");
    }
}
