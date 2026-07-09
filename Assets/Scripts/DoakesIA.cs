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

    [Header("Configuração de Sprites Assimétricos")]
    [Tooltip("Tempo (em segundos) que o Doakes passa exibindo a animação de tiro antes de voltar a correr.")]
    public float duracaoAnimacaoAtaque = 0.5f;

    [Header("Detecção de obstáculo")]
    public LayerMask camadaObstaculo;
    public float distanciaChecarObstaculo = 0.8f;
    public Vector2 offsetChecarObstaculo = new Vector2(0.4f, 0.2f);

    private Transform player;
    private bool podeAtacar = true;
    private float alturaInicial;
    private Animator animator;

    // Flag interna para sabermos se ele está executando os frames de tiro
    private bool estaAtacandoVisual = false;

    void Start()
    {
        alturaInicial = transform.position.y;
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

        if (distancia <= distanciaParaPerseguir)
        {
            float direcao = player.position.x > transform.position.x ? 1f : -1f;
            Virar(direcao);
        }

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
            return;
        }

        transform.position += Vector3.right * direcao * velocidade * Time.deltaTime;

        // Mantém o Doakes preso na altura inicial dele
        transform.position = new Vector3(transform.position.x, alturaInicial, transform.position.z);
    }

    IEnumerator Atacar()
    {
        podeAtacar = false;
        estaAtacandoVisual = true; // ATIVA a inversão de sinal porque os sprites de tiro são opostos

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

        // Espera o tempo exato que o Doakes passa atirando na tela
        yield return new WaitForSeconds(duracaoAnimacaoAtaque);
        
        estaAtacandoVisual = false; // DESATIVA a inversão para que ele volte a correr olhando para frente normal

        // Espera o restante do tempo de recarga (cooldown) antes do próximo tiro
        float tempoRestanteCooldown = intervaloAtaque - duracaoAnimacaoAtaque;
        if (tempoRestanteCooldown > 0f)
        {
            yield return new WaitForSeconds(tempoRestanteCooldown);
        }

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
        
        if (estaAtacandoVisual)
        {
            // Se ele estiver atirando, usamos o sinal INVERTIDO para compensar a arte original
            escala.x = Mathf.Abs(escala.x) * -direcaoAtual; 
        }
        else
        {
            // Se estiver correndo ou parado, usamos o sinal NORMAL
            escala.x = Mathf.Abs(escala.x) * direcaoAtual; 
        }
        
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