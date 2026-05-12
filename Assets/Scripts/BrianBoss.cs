using System.Collections;
using UnityEngine;

public class BrianBoss : MonoBehaviour
{
    [Header("Ataque")]
    public GameObject prefabProjetil;
    public float intervaloAtaque = 2.5f;
    public float alturaDisparo = 0.4f;

    [Header("Movimento")]
    public float velocidadeAproximacao = 1.4f;
    public float distanciaMinimaDoPlayer = 2.2f;
    public bool seguirDepoisDeTomarDano = true;

    [Header("Chão")]
    public bool manterNoChao = true;

    private Transform player;
    private bool podeAtacar = true;
    private bool foiAtingido = false;

    private float alturaInicial;
    private VidaInimigo vidaInimigo;
    private int vidaInicial;

    void Start()
    {
        alturaInicial = transform.position.y;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        vidaInimigo = GetComponent<VidaInimigo>();

        if (vidaInimigo != null)
        {
            vidaInicial = vidaInimigo.vida;
        }
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        VerificarSeTomouDano();

        VirarParaPlayer();

        if (foiAtingido && seguirDepoisDeTomarDano)
        {
            AproximarDoPlayer();
        }

        if (podeAtacar)
        {
            StartCoroutine(Atacar());
        }
    }

    void LateUpdate()
    {
        if (manterNoChao)
        {
            transform.position = new Vector3(
                transform.position.x,
                alturaInicial,
                transform.position.z
            );
        }
    }

    void VerificarSeTomouDano()
    {
        if (vidaInimigo == null)
        {
            return;
        }

        if (vidaInimigo.vida < vidaInicial)
        {
            foiAtingido = true;
        }
    }

    void AproximarDoPlayer()
    {
        float distanciaX = Mathf.Abs(transform.position.x - player.position.x);

        if (distanciaX <= distanciaMinimaDoPlayer)
        {
            return;
        }

        float direcao = player.position.x > transform.position.x ? 1f : -1f;

        transform.position += Vector3.right * direcao * velocidadeAproximacao * Time.deltaTime;
    }

    void VirarParaPlayer()
    {
        float direcao = player.position.x > transform.position.x ? 1f : -1f;

        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * direcao;
        transform.localScale = escala;
    }

    IEnumerator Atacar()
    {
        podeAtacar = false;

        if (prefabProjetil != null && player != null)
        {
            float direcaoProjetil = player.position.x > transform.position.x ? 1f : -1f;

            Vector3 posicaoDisparo = transform.position + new Vector3(
                direcaoProjetil * 0.5f,
                alturaDisparo,
                0
            );

            GameObject projetil = Instantiate(prefabProjetil, posicaoDisparo, Quaternion.identity);

            ProjetilBrian scriptProjetil = projetil.GetComponent<ProjetilBrian>();

            if (scriptProjetil != null)
            {
                scriptProjetil.ConfigurarDirecao(direcaoProjetil);
            }
        }

        yield return new WaitForSeconds(intervaloAtaque);

        podeAtacar = true;
    }
}