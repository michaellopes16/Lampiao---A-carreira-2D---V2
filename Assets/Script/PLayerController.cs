using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.EventSystems;

public class PLayerController : MonoBehaviour
{

    public Animator anime;
    public Animation animacao;
    public Rigidbody2D playerRigidBody;
    public int forceJump;

    public bool slide;
    public bool pulou;
    public static bool introducao;
    bool moverMedalhas;

    public GameObject jumentoExplosion;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    public bool grounded;

    public float tempoSlide;
    public float timeTemp;

    public float tempoPulo;
    public float timeTemp2;

    public Transform colisor;

    private int normalTime;


    //Modificações 21/11/2017 as 00:23
    float tempImune, tempImuneAux;
    public Animator animPeixeira;


    public static int qntMedalhas;
    public static float qntPontuacao;

    public UnityEngine.UI.Text txtPontos;
    public UnityEngine.UI.Text txtMedalhas;
    // public UnityEngine.UI.Text txtLevelDificult;

    public static bool colidiuComOBonus, colidiuComOBonus2;
    public static float difficultyLevel = 2;
    private int maxDifficultyLevel = 12;
    private int scoreToNextlevel = 10;
    public bool colidiu;
    public bool tiro, carrera;
    GameObject camera;

    public AudioSource gerenciadorDeAudio;
    public AudioClip[] arrayDeSons;

    public ParticleSystem particulaMedalha;

    private float posicaoY = 0f;
    public BoxCollider2D coliderPLayer;
    // public GameObject newPrefabe;
    float tempIncio;
    float tempAni;
    //NomeController nomeController;
    // Use this for initialization

    // Filipe Mendes Mariz //
    // Util para PowerUps //

    public GameController gC;
    public float garrafaBonus;
    public float duracaoInvencibilidade;
    public bool temEspingarda;
    public bool temPeixeira;
    private Vector2 startPosition;
    private float startTime;
    private bool tocouLampiao;
    public GameObject botaoEspingarda;
    public GameObject botaoPeixeira;
    public GameObject bala;

    //Modificações 21/11/2017
    public static bool IsImune;
    public static bool TaComJumento;
    public ParticleSystem efeitoInvensibilidade;
    public ParticleSystem efeitoImunidade;
    public ParticleSystem efeitoVida;
    public GameObject efeitoDestruirObjeto;
    float auxTimePeixeira;

    private LineRenderer line;
    public Material materialLine;
    // public bool tiro, carrera;

    public float getDifficultyLevel(){
        return difficultyLevel;
    }


    void Start()
    {
        //Modificações 21/11/2017 as 00:23
        IsImune = false;
        tempIncio = 0.0f;
        tempImune = 4.0f;
        auxTimePeixeira = 0.0f;
        // efeitoJogador = null;
        //

        tiro = false;
        animacao = new Animation();
        carrera = false;
        tempIncio = 0.0f;
        difficultyLevel = 2.5f;
        colidiuComOBonus = false;
        tempAni = anime.GetCurrentAnimatorStateInfo(0).length;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        qntMedalhas = 0;
        qntPontuacao = 0.0f;
        colidiu = false;

        PlayerPrefs.SetInt("pontuacao", 0);
        PlayerPrefs.SetInt("quantMedalhas", 0);
        PlayerPrefs.SetInt("indiceGeral", 0);
        introducao = anime.GetBool("introducao");
        //  txtLevelDificult.text = "0";

        //PowerUp//
        gC = camera.GetComponent<GameController>();
        tocouLampiao = false;
        temPeixeira = false;
        temEspingarda = false;
        botaoEspingarda.SetActive(false);
        botaoPeixeira.SetActive(false);
        

        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            Transform obj = this.gameObject.transform.GetChild(i);
            if (obj.name == "PlayerCosmeticEspingarda" || obj.name == "PlayerCosmeticPeixeira")
            {
                obj.GetComponent<Renderer>().enabled = !obj.GetComponent<Renderer>().enabled;
            }
        }

    }


    // Update is called once per frame
    void Update()
    {
        tempIncio += Time.deltaTime;

        if (introducao)
        {

            if (!tiro && tempIncio >= 2.3f)
            {
                tiro = true;
                gerenciadorDeAudio.PlayOneShot(arrayDeSons[5]);
                gerenciadorDeAudio.volume = 1f;
            }
            if (!carrera && tempIncio >= 3f)
            {
                carrera = true;
                gerenciadorDeAudio.PlayOneShot(arrayDeSons[8]);
                gerenciadorDeAudio.volume = 1f;
            }
            if (tempIncio >= tempAni)
            {
                introducao = false;
                anime.SetBool("introducao", false);
                anime.SetBool("viveu", true);
                camera.GetComponent<GameController>().emJogo = true;
            }

        }
        else if (camera.GetComponent<GameController>().emJogo)
        {
            // gameObject.GetComponent<Animator>().Play("PlayerRuning", 0);
            anime.SetBool("pausa", false);
            anime.SetBool("colidiu", false);
            anime.SetBool("viveu", true);
            anime.SetBool("caiuAgua", false);
            gameObject.GetComponent<Rigidbody2D>().simulated = true;


           
            // Cria o tempo de imunidade do personagem
            if (animPeixeira.GetBool("IsAttack"))
            {
                auxTimePeixeira += Time.deltaTime;

                if (auxTimePeixeira >= 0.7f)
                {
                    animPeixeira.SetBool("IsAttack", false);
                    auxTimePeixeira = 0.0f;
                }
            }



            if (IsImune)
            {
              
                if (tempImuneAux == 0.0f)
                {
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
                  // transform.position = GameObject.FindWithTag("grounded").transform.position;
                    Debug.Log("Entrou na imunidade");
                    InstantiateParticulaPlayer(efeitoImunidade);
                    //anime.SetBool("IsImune", IsImune);
                }
                tempImuneAux += Time.deltaTime;
                //Debug.Log("Tempo Imune Aux:" + tempImuneAux);

                if (tempImuneAux >= tempImune)
                {

                    Debug.Log("setando isImune");
                    tempImuneAux = 0.0f;
                    IsImune = false;        
                   gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.8f;
                    // Debug.Log("Tempo imune acabou!!!" + tempImuneAux);
                }
                anime.SetBool("IsImune", IsImune);
            }
            // Fim do tempo de imunidade

            //gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1.47f, gameObject.transform.position.z);

            txtMedalhas.text = qntMedalhas.ToString();

            // normalTime += (int) Time.deltaTime *2;

            if (qntPontuacao >= scoreToNextlevel)
                LevelUp();

            qntPontuacao += Time.deltaTime * difficultyLevel;
            txtPontos.text = ((int)qntPontuacao).ToString();
            // Debug.Log("Tempo:" + Time.deltaTime +"qntPOntuacao:"+qntPontuacao);

            txtPontos.text = ((int)qntPontuacao).ToString();

            if (pulou)
            {
                timeTemp2 += Time.deltaTime;
                if (timeTemp2 >= tempoPulo)
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    // Debug.Log("Acabou o pulo?" + pulou);
                    pulou = false;
                }
            }
            grounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, whatIsGround);
            anime.SetBool("jump", !grounded);
            anime.SetBool("slide", slide);
        }
        else if (GameController.emPausa)
        {
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
            anime.SetBool("pausa", true);
            anime.SetBool("jump", false);
            anime.SetBool("colidiu", false);
            anime.SetBool("viveu", false);
            anime.SetBool("caiuAgua", false);
        }
        else if (anime.GetBool("introducao"))
        {
            anime.SetBool("pausa", false);
            anime.SetBool("jump", false);
            anime.SetBool("colidiu", false);
            anime.SetBool("viveu", false);
            anime.SetBool("caiuAgua", false);
        }

        //PowerUp//
        if (!GameController.emPausa)
        {
            if (garrafaBonus > 0)
            {
                garrafaBonus -= Time.deltaTime / 10f;
                updateDiff();
            }
            if (duracaoInvencibilidade > 0)
            {
                duracaoInvencibilidade -= Time.deltaTime / 7;
            }
            else
            {
                duracaoInvencibilidade = 0;
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }

            //Controle de Toque//
            //Espingarda, Peixeira e Pulo para baixo.//	
            //Mudar Input.GetMouseButtonDown(0) para Input.Touch depois de usar
            // if (Input.GetTouch (0).phase == TouchPhase.Ended) {
            if (Input.GetMouseButtonUp(0) && espingardaOut)
            {
                espingardaOut = false;
                atirarEspingarda();
            }

            if(espingardaOut){
                Vector2 endPositionAim = Input.mousePosition;
                Vector2 delta = endPositionAim - initialPositionAim;
                float angleRadians = Mathf.Atan2(delta.y,delta.x);
                GameObject tiro = (GameObject) Instantiate(bala);
                GameObject posicao = GameObject.Find("Colisor");
                float x = posicao.transform.position.x;
                float y = posicao.transform.position.y - 0.5f;    
                Debug.DrawLine(new Vector2(x,y), (new Vector2(Mathf.Cos(angleRadians),Mathf.Sin(angleRadians))) * 6);
            }
        
        }
    }
    public bool espingardaOut;    
    public Vector2 initialPositionAim, endPositionAim;
	
	public void iniciarEspingarda(){
        Debug.Log("Espingarda atirando");
       // createLine();
        espingardaOut = true;
        initialPositionAim = Input.mousePosition;
        Time.timeScale = 0.5f;
	}
    private void createLine()
    {
        //create a new empty gameobject and line renderer component
        line = new GameObject("Line" + line).AddComponent<LineRenderer>();
        //assign the material to the line
        line.material = materialLine;
        //set the number of points to the line
        line.SetVertexCount(2);
        //set the width
        line.SetWidth(0.15f, 0.15f);
        //render line to the world origin and not to the object's position
        line.useWorldSpace = true;

    }


    public void atirarEspingarda()
    {   
        Debug.Log("Espingarda terminou");
        Time.timeScale = 1.0f;
        espingardaOut = false;
        Vector2 endPositionAim = Input.mousePosition;
        Vector2 delta = endPositionAim - initialPositionAim;
        float angleRadians = Mathf.Atan2(delta.y,delta.x);
        GameObject tiro = (GameObject) Instantiate(bala);
        GameObject posicao = GameObject.Find("Player");
        float x = posicao.transform.position.x;
        float y = posicao.transform.position.y + 0.5f;    
        tiro.GetComponent<Rigidbody2D>().position = new Vector2(x,y);
        tiro.GetComponent<Rigidbody2D>().velocity = (new Vector2(Mathf.Cos(angleRadians),Mathf.Sin(angleRadians))) * 6 ;
        Destroy(tiro, 2.5f);
    }



    public void saltarBaixo(){
        Debug.Log("Saltou para baixo.");
    }
    public void usarPeixeira()
    {
        animPeixeira.SetBool("IsAttack",true);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 1.5f);
        foreach(Collider2D hitObj in hitColliders)
        {
            if(hitObj.tag == "colisor")
            {
                pulverizar(hitObj.gameObject);
            }
            Debug.Log(hitObj.name);
        }
    }

    public void pulverizar(GameObject alvo)
    {
        Destroy(alvo);
    }

    //void OnDrawGizmosSelected() {
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(gameObject.transform.position, 2); 
    //}

    IEnumerator autoDel(float duracao_animacao, GameObject objeto)
    {
        yield return new WaitForSeconds(duracao_animacao);
        Destroy(objeto);
    }

    public void pular()
    {
        //Debug.Log("Entrou no pulo, mas não ta´decetado o grounded");
        if (grounded)
        {
           // Debug.Log("Está entrando no pulo");
            gerenciadorDeAudio.PlayOneShot(arrayDeSons[0]);
            gerenciadorDeAudio.volume = 0.06f;

            playerRigidBody.AddForce(new Vector2(0, forceJump));
            pulou = true;
            timeTemp2 = 0;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            /*
			if (slide) {
				colisor.position = new Vector3 (colisor.position.x, colisor.position.y + 0.6f, colisor.position.z);
				slide = false;
			}

			slide = false;*/
        }
    }

    void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
            return;

        scoreToNextlevel *= 2;
        difficultyLevel += 0.3f;
        updateDiff();
    }

    void updateDiff()
    { }

    public void InstantiateParticula(ParticleSystem particula, Vector3 position)
    {
        ParticleSystem novaParticula = Instantiate(particula, position, Quaternion.identity);
        Destroy(novaParticula.gameObject, novaParticula.startLifetime);
    }
    public void InstantiateParticulaPlayer(ParticleSystem particula)
    {
       
            Vector3 positionParticule = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            ParticleSystem novaParticula = Instantiate(particula, positionParticule, Quaternion.identity);
            novaParticula.transform.SetParent(transform);
            Destroy(novaParticula.gameObject, novaParticula.startLifetime);
  
    }


    public IEnumerator esperatAnimcaoTerminar()
    {
        yield return new WaitForSeconds(anime.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag == "Player")
        {
            if (collision.gameObject.tag == "Medalha")
            {
                qntMedalhas++;

                gerenciadorDeAudio.PlayOneShot(arrayDeSons[1]);
                gerenciadorDeAudio.volume = 0.06f;
                // Instantiate(particulaMedalha, new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z));
                InstantiateParticula(particulaMedalha, collision.transform.position);
                Destroy(collision.transform.gameObject);

            }
            else if (collision.gameObject.tag == "Bonus" && !colidiuComOBonus)
            {

                // Debug.Log("Colidiu com o Bonus: "+ gameObject.tag);
                colidiuComOBonus = true;
                colidiuComOBonus2 = true;
                qntPontuacao *= 1.5f;
                gerenciadorDeAudio.PlayOneShot(arrayDeSons[1]);
                //gerenciadorDeAudio.volume = 0.06f;
                collision.gameObject.GetComponent<Rigidbody2D>().mass = 1;
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.8f;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceJump, forceJump));
                // Instantiate(particulaMedalha, new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z));
                InstantiateParticula(particulaMedalha, collision.transform.position);
                Destroy(collision.transform.gameObject, 0.5f);

            }
            else if (collision.gameObject.tag == "jumento")
            {
                TaComJumento = true;
                gerenciadorDeAudio.PlayOneShot(arrayDeSons[9]);
                gerenciadorDeAudio.volume = 0.09f;
                //gameObject.transform.position = new Vector3(gameObject.transform.position.x, -1.3f, gameObject.transform.position.z);
                // gameObject.GetComponent<Rigidbody2D>().simulated = false;
                anime.SetBool("ColJumento", true);
                anime.SetBool("jump", false);
                anime.SetBool("caiuAgua", false);
                // anime.SetBool("viveu", false);
                anime.SetBool("pausa", false);
                //-2.31
                transform.position = new Vector3(gameObject.transform.position.x, -1.71f, gameObject.transform.position.z);
                GameObject tempPrefab = Instantiate(jumentoExplosion, collision.transform.position, Quaternion.identity) as GameObject;
                Destroy(tempPrefab, 1.3f);
                Destroy(collision.transform.gameObject);
            }
            else if (collision.gameObject.tag == "portal")
            {
               
                gerenciadorDeAudio.PlayOneShot(arrayDeSons[9]);
                gerenciadorDeAudio.volume = 0.09f;
                //gameObject.transform.position = new Vector3(gameObject.transform.position.x, -1.3f, gameObject.transform.position.z);
                // gameObject.GetComponent<Rigidbody2D>().simulated = false;
                anime.SetBool("jump", false);
                anime.SetBool("caiuAgua", false);
                anime.SetBool("viveu", false);
                anime.SetBool("pausa", false);
                gC.mudarFase2();
                
            }
            else if (collision.gameObject.tag == "agua")
            {
                if (IsImune) {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, -1.3f, gameObject.transform.position.z);
                }
                else  {

                    registarRecorde();
                    registarMedalha();
                    gerenciadorDeAudio.PlayOneShot(arrayDeSons[6]);
                    gerenciadorDeAudio.volume = 0.06f;
                    // Debug.Log("Colisor" + collision.gameObject.tag);
                    // gameObject.transform.position = new Vector3(gameObject.transform.position.x, -1.3f, gameObject.transform.position.z);
                    gameObject.GetComponent<Rigidbody2D>().simulated = false;
                    anime.SetBool("jump", false);
                    anime.SetBool("caiuAgua", true);
                    anime.SetBool("viveu", false);
                    anime.SetBool("pausa", false);
                    anime.SetBool("ColJumento", false);
                    // Destroy(transform.gameObject,1f).;
                    StartCoroutine(esperatAnimcaoTerminar());
                    camera.GetComponent<GameController>().emJogo = false;
                }

            }

            else if (collision.gameObject.tag == "ChaoController" && !colidiu)
            {

                colidiu = true;
                GetComponent<SpawnController>().criarObj(-difficultyLevel);
            }

            else if (collision.gameObject.tag == "colisor")
            {
                // Felipe 
                if (duracaoInvencibilidade == 0)
                {
                    if(IsImune)
                    {
                       // Debug.Log("Está imune");
                        
                       // efeitoJogador = Instantiate(efeitoImunidade, transform.position, Quaternion.identity);

                    }
                    else if (!IsImune)
                    {
                            if (anime.GetBool("ColJumento"))
                            {
                                //colidiu = true;

                                IsImune = true;
                                gerenciadorDeAudio.PlayOneShot(arrayDeSons[3]);
                                gerenciadorDeAudio.volume = 0.06f;
                                gameObject.transform.position = new Vector3(gameObject.transform.position.x, -1.3f, gameObject.transform.position.z);
                                gameObject.GetComponent<Rigidbody2D>().simulated = false;
                                TaComJumento = false;
                                anime.SetBool("ColJumento", false);
                                anime.SetBool("IsImune", IsImune);
                            }
                            else if (temPeixeira)
                            {
                                peixeiraStateChange();
                                sofreuDano(2);
                                duracaoInvencibilidade += 0.25f / 7f;
                            }
                            else if (temEspingarda)
                            {
                                espingardaStateChange();
                                sofreuDano(2);
                                duracaoInvencibilidade += 0.25f / 7f;
                            }
                            else
                            {
                                
                                morreu();
                                gerenciadorDeAudio.PlayOneShot(arrayDeSons[3]);
                                gerenciadorDeAudio.volume = 0.06f;
                                anime.SetBool("colidiu", true);
                                anime.SetBool("viveu", false);
                            }
                    }
                }
                else
                {
                    GameObject obj = Instantiate(efeitoDestruirObjeto, collision.transform.position, Quaternion.identity);
                    Destroy(collision.transform.gameObject);
                    Destroy(obj, 1.0f);
                    gerenciadorDeAudio.PlayOneShot(arrayDeSons[10]);
                    gerenciadorDeAudio.volume = 0.10f;
                }
                // Application.LoadLevel("TelaInicial");
            }
            else if (collision.gameObject.tag == "PowerUp")
            {
                string nome_objeto = collision.gameObject.name;
                gerenciadorDeAudio.PlayOneShot(arrayDeSons[1]);
                gerenciadorDeAudio.volume = 0.06f;
                Destroy(collision.transform.gameObject);

                if (nome_objeto == "FrutoMandacaru")
                {
                    gC.aumentarVida();
                    gerenciadorDeAudio.PlayOneShot(arrayDeSons[9]);
                    gerenciadorDeAudio.volume = 0.09f;
                    InstantiateParticula(efeitoVida,collision.transform.position);
                }
                else if (nome_objeto == "Quartinho")
                {
                    // So uma variavel. Comportamento indicado no detector de colisao./
                    duracaoInvencibilidade += 1f;
                    gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                    InstantiateParticulaPlayer(efeitoInvensibilidade);
                }
                else if (nome_objeto == "Garrafa")
                {
                    gC.garrafaUp();
                    difficultyLevel += 0.5f;
                    qntPontuacao *= 1.1f;
                    garrafaBonus += 1f;
                }
                else if (nome_objeto == "Espingarda")
                {
                    if (!temEspingarda)
                    {
                        espingardaStateChange();
                    }
                }
                else if (nome_objeto == "Peixeira")
                {
                    if (!temPeixeira)
                    {
                        peixeiraStateChange();
                    }
                }
            }
        }
    }

    public void espingardaStateChange()
    {
        temEspingarda = !temEspingarda;
        botaoEspingarda.SetActive(temEspingarda);
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            Transform obj = this.gameObject.transform.GetChild(i);
            if (obj.name == "PlayerCosmeticEspingarda")
            {
                obj.GetComponent<Renderer>().enabled = !obj.GetComponent<Renderer>().enabled;
            }
        }
    }

    public void peixeiraStateChange()
    {
        
        temPeixeira = !temPeixeira;
        botaoPeixeira.SetActive(temPeixeira);
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            Transform obj = this.gameObject.transform.GetChild(i);
            if (obj.name == "PlayerCosmeticPeixeira")
            {
                obj.GetComponent<Renderer>().enabled = !obj.GetComponent<Renderer>().enabled;
            }
           
        }


    }

    IEnumerator sofreuDano(float duracao)
    {
        float remainingTime = 1;
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime / duracao;
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void morreu()
    {
       // difficultyLevel = 2;
        updateDiff();
        duracaoInvencibilidade = 0;
        gC.blurTimer = 0;
        garrafaBonus = 0;
        if (temPeixeira)
            peixeiraStateChange();
        if (temEspingarda)
            espingardaStateChange();

        registarRecorde();
        registarMedalha();
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        anime.SetBool("viveu", false);
        StartCoroutine(esperatAnimcaoTerminar());
        camera.GetComponent<GameController>().emJogo = false;   
    }


    public void registarRecorde()
    {
        PlayerPrefs.SetInt("pontuacao", (int)qntPontuacao);

        if (qntPontuacao > PlayerPrefs.GetInt("recorde"))
        {
            PlayerPrefs.SetInt("recorde", (int)qntPontuacao);
        }

    }

    public void registarMedalha()
    {
        PlayerPrefs.SetInt("quantMedalhas", (int)qntMedalhas);

        if (qntMedalhas > PlayerPrefs.GetInt("recordeMedalhas"))
        {
            PlayerPrefs.SetInt("recordeMedalhas", (int)qntMedalhas);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ChaoController" && colidiu)
        {
            //  Debug.Log("Criou Chão!");
            colidiu = false;

            //GameObject tempPrefab = Instantiate(newPrefabe) as GameObject;
        }
        if (collision.gameObject.tag == "Bonus" && colidiuComOBonus)
        {
            // Debug.Log("Saiu da colisão");
            colidiuComOBonus = false;

        }
    }

// Deprecado, usado para esquivar das cercas altas. 
    public void Slid()
    {
        colisor.position = new Vector3(colisor.position.x, colisor.position.y - 0.6f, colisor.position.z);
        slide = true;
        timeTemp = 0;
    }

    public void pausarJogo()
    {
        if (GameController.emPausa)
        {
            camera.GetComponent<GameController>().emJogo = true;
            GameController.emPausa = false;

        }
        else
        {
            // Debug.Log("Pausou o jogo");
            camera.GetComponent<GameController>().emJogo = false;
            GameController.emPausa = true;
        }

    }


}
