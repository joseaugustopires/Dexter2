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
    public float distanciaChecarObstaculo = 0.8f;
    public Vector2 offsetChecarObstaculo = new Vector2(0.4f, 0.2f);

    private Transform player;
    private bool podeAtacar = true;
    private float alturaInicial;

    // NOVA ADIÇÃO: Referência para controlar as animações do Doakes
    private Animator animator;

    void Start()
    {
        alturaInicial = transform.position.y;

        // NOVA ADIÇÃO: Busca o Animator no objeto do Doakes assim que o jogo começa
        animator = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null)
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
            // Se tiver uma caixa/obstáculo na frente, o Doakes para.
            // Assim o Dexter consegue usar a caixa para fugir ou ganhar distância.
            Virar(direcao);
            return;
        }

        transform.position += Vector3.right * direcao * velocidade * Time.deltaTime;

        // Mantém o Doakes preso na altura inicial dele,
        // para ele não subir junto quando o Dexter pula.
        transform.position = new Vector3(transform.position.x, alturaInicial, transform.position.z);

        Virar(direcao);
    }

    IEnumerator Atacar()
    {
        podeAtacar = false;

        // NOVA ADIÇÃO: Toca a animação de ataque definindo o Gatilho (Trigger) "Atacar" no Animator
        if (animator != null)
        {
            animator.SetTrigger("Atacar");
        }

        if (prefabProjetil != null && player != null)
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
        Vector2 origem = (Vector2)transform.position + new Vector2(
            offsetChecarObstaculo.x * direcaoAtual,
            offsetChecarObstaculo.y
        );

        Vector2 direcaoRaio = Vector2.right * direcaoAtual;

        RaycastHit2D hit = Physics2D.Raycast(
            origem,
            direcaoRaio,
            distanciaChecarObstaculo,
            camadaObstaculo
        );

        return hit.collider != null;
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
        Gizmos.DrawLine(
            origemDireita,
            origemDireita + Vector2.right * distanciaChecarObstaculo
        );

        Vector2 origemEsquerda = (Vector2)transform.position + new Vector2(
            -offsetChecarObstaculo.x,
            offsetChecarObstaculo.y
        );

        Gizmos.DrawLine(
            origemEsquerda,
            origemEsquerda + Vector2.left * distanciaChecarObstaculo
        );
    }
}