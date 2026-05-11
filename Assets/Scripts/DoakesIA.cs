using System.Collections;
using UnityEngine;

public class DoakesIA : MonoBehaviour
{
    public float velocidade = 2.5f;
    public float distanciaParaPerseguir = 8f;
    public float distanciaMinima = 2f;

    public GameObject prefabProjetil;
    public float intervaloAtaque = 2f;
    public float alturaDisparo = 0.4f;

    [Header("Detecção de obstáculo")]
    public LayerMask camadaObstaculo;
    public float distanciaChecarObstaculo = 0.7f;
    public Vector2 offsetChecarObstaculo = new Vector2(0.4f, 0f);

    [Header("Pulo do Doakes")]
    public float alturaPulo = 1.2f;
    public float distanciaPulo = 1.5f;
    public float tempoPulo = 0.45f;

    private Transform player;
    private bool podeAtacar = true;
    private bool pulando = false;
    private float alturaInicial;

    void Start()
    {
        alturaInicial = transform.position.y;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null || pulando)
        {
            return;
        }

        float distancia = Vector2.Distance(transform.position, player.position);

        if (distancia <= distanciaParaPerseguir && distancia > distanciaMinima)
        {
            PerseguirPlayer();
        }

        if (distancia <= distanciaParaPerseguir && podeAtacar)
        {
            StartCoroutine(Atacar());
        }
    }

    void PerseguirPlayer()
    {
        float direcao = player.position.x > transform.position.x ? 1f : -1f;

        if (TemObstaculoNaFrente(direcao))
        {
            StartCoroutine(PularObstaculo(direcao));
            return;
        }

        transform.position += Vector3.right * direcao * velocidade * Time.deltaTime;

        // Mantém o Doakes no chão quando ele não está pulando
        transform.position = new Vector3(transform.position.x, alturaInicial, transform.position.z);

        Virar(direcao);
    }

    IEnumerator Atacar()
    {
        podeAtacar = false;

        if (prefabProjetil != null)
        {
            Vector3 posicaoDisparo = transform.position + new Vector3(0, alturaDisparo, 0);
            GameObject projetil = Instantiate(prefabProjetil, posicaoDisparo, Quaternion.identity);

            ProjetilDoakes scriptProjetil = projetil.GetComponent<ProjetilDoakes>();

            if (scriptProjetil != null)
            {
                scriptProjetil.Configurar(player.position);
            }
        }

        yield return new WaitForSeconds(intervaloAtaque);

        podeAtacar = true;
    }

    bool TemObstaculoNaFrente(float direcaoAtual)
    {
        Vector2 origem = (Vector2)transform.position + new Vector2(offsetChecarObstaculo.x * direcaoAtual, offsetChecarObstaculo.y);
        Vector2 direcaoRaio = Vector2.right * direcaoAtual;

        RaycastHit2D hit = Physics2D.Raycast(origem, direcaoRaio, distanciaChecarObstaculo, camadaObstaculo);

        return hit.collider != null;
    }

    IEnumerator PularObstaculo(float direcaoPulo)
    {
        pulando = true;

        Virar(direcaoPulo);

        Vector3 inicio = transform.position;
        Vector3 fim = inicio + new Vector3(direcaoPulo * distanciaPulo, 0, 0);

        float tempo = 0f;

        while (tempo < tempoPulo)
        {
            float t = tempo / tempoPulo;

            float x = Mathf.Lerp(inicio.x, fim.x, t);
            float y = alturaInicial + Mathf.Sin(t * Mathf.PI) * alturaPulo;

            transform.position = new Vector3(x, y, transform.position.z);

            tempo += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(fim.x, alturaInicial, transform.position.z);

        pulando = false;
    }

    void Virar(float direcaoAtual)
    {
        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * direcaoAtual;
        transform.localScale = escala;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector2 origemDireita = (Vector2)transform.position + offsetChecarObstaculo;
        Gizmos.DrawLine(origemDireita, origemDireita + Vector2.right * distanciaChecarObstaculo);

        Vector2 origemEsquerda = (Vector2)transform.position + new Vector2(-offsetChecarObstaculo.x, offsetChecarObstaculo.y);
        Gizmos.DrawLine(origemEsquerda, origemEsquerda + Vector2.left * distanciaChecarObstaculo);
    }
}