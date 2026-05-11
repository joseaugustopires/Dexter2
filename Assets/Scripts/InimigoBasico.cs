using System.Collections;
using UnityEngine;

public class InimigoBasico : MonoBehaviour
{
    public float velocidade = 2f;
    public float distancia = 3f;

    public float velocidadePerseguindo = 3f;
    public float distanciaMinimaDoPlayer = 0.8f;
    public float distanciaParaPerceberPlayer = 5f;

    public bool patrulharAntesDeVerPlayer = true;

    [Header("Detecção de obstáculo")]
    public LayerMask camadaObstaculo;
    public float distanciaChecarObstaculo = 0.7f;
    public Vector2 offsetChecarObstaculo = new Vector2(0.4f, 0f);

    [Header("Pulo do inimigo")]
    public float alturaPulo = 1.2f;
    public float distanciaPulo = 1.5f;
    public float tempoPulo = 0.45f;

    private Vector3 posicaoInicial;
    private int direcao = 1;

    private bool perseguindoPlayer = false;
    private bool pulando = false;

    private Transform player;

    void Start()
    {
        posicaoInicial = transform.position;

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

        float distanciaPlayer = Vector2.Distance(transform.position, player.position);

        if (distanciaPlayer <= distanciaParaPerceberPlayer)
        {
            perseguindoPlayer = true;
        }

        if (perseguindoPlayer)
        {
            PerseguirPlayer();
        }
        else if (patrulharAntesDeVerPlayer)
        {
            Patrulhar();
        }
    }

    void Patrulhar()
    {
        if (TemObstaculoNaFrente(direcao))
        {
            StartCoroutine(PularObstaculo(direcao));
            return;
        }

        transform.position += Vector3.right * direcao * velocidade * Time.deltaTime;

        if (transform.position.x >= posicaoInicial.x + distancia)
        {
            direcao = -1;
            Virar(direcao);
        }

        if (transform.position.x <= posicaoInicial.x - distancia)
        {
            direcao = 1;
            Virar(direcao);
        }
    }

    void PerseguirPlayer()
    {
        float distanciaX = Mathf.Abs(transform.position.x - player.position.x);

        if (distanciaX <= distanciaMinimaDoPlayer)
        {
            return;
        }

        float direcaoPlayer = player.position.x > transform.position.x ? 1f : -1f;

        if (TemObstaculoNaFrente(direcaoPlayer))
        {
            StartCoroutine(PularObstaculo(direcaoPlayer));
            return;
        }

        transform.position += Vector3.right * direcaoPlayer * velocidadePerseguindo * Time.deltaTime;

        // Mantém o inimigo na altura original quando ele não está pulando
        transform.position = new Vector3(transform.position.x, posicaoInicial.y, transform.position.z);

        Virar(direcaoPlayer);
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
            float y = posicaoInicial.y + Mathf.Sin(t * Mathf.PI) * alturaPulo;

            transform.position = new Vector3(x, y, transform.position.z);

            tempo += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(fim.x, posicaoInicial.y, transform.position.z);

        pulando = false;
    }

    void Virar(float direcaoAtual)
    {
        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * direcaoAtual;
        transform.localScale = escala;
    }

    public void FicarAlertado()
    {
        perseguindoPlayer = true;
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