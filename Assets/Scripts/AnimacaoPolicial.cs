using UnityEngine;

public class AnimacaoPolicial : MonoBehaviour
{
    private Animator animator;
    private Vector3 posicaoAnterior;
    private Transform player;

    [Header("Animação")]
    public float limiteMovimento = 0.01f;

    [Header("Virar Sprite")]
    public bool spriteOlhaParaDireita = true;

    [Header("Comportamento de direção")]
    public bool virarParaPlayerQuandoPerto = true;
    public float distanciaParaOlharPlayer = 4f;

    void Start()
    {
        animator = GetComponent<Animator>();
        posicaoAnterior = transform.position;

        GameObject jogador = GameObject.FindGameObjectWithTag("Player");

        if (jogador != null)
        {
            player = jogador.transform;
        }
    }

    void Update()
    {
        AtualizarAnimacaoMovimento();
        AtualizarDirecaoDoSprite();

        posicaoAnterior = transform.position;
    }

    void AtualizarAnimacaoMovimento()
    {
        if (animator == null)
        {
            return;
        }

        float movimentoX = transform.position.x - posicaoAnterior.x;
        float velocidade = Mathf.Abs(movimentoX) / Time.deltaTime;

        animator.SetFloat("Velocidade", velocidade);
    }

    void AtualizarDirecaoDoSprite()
    {
        float movimentoX = transform.position.x - posicaoAnterior.x;

        bool deveOlharParaPlayer = false;

        if (virarParaPlayerQuandoPerto && player != null)
        {
            float distancia = Vector2.Distance(transform.position, player.position);

            if (distancia <= distanciaParaOlharPlayer)
            {
                deveOlharParaPlayer = true;
            }
        }

        if (deveOlharParaPlayer)
        {
            VirarParaPlayer();
        }
        else
        {
            VirarPeloMovimento(movimentoX);
        }
    }

    void VirarPeloMovimento(float movimentoX)
    {
        if (Mathf.Abs(movimentoX) <= limiteMovimento)
        {
            return;
        }

        Vector3 escala = transform.localScale;

        if (movimentoX > 0)
        {
            escala.x = spriteOlhaParaDireita ? Mathf.Abs(escala.x) : -Mathf.Abs(escala.x);
        }
        else if (movimentoX < 0)
        {
            escala.x = spriteOlhaParaDireita ? -Mathf.Abs(escala.x) : Mathf.Abs(escala.x);
        }

        transform.localScale = escala;
    }

    void VirarParaPlayer()
    {
        if (player == null)
        {
            return;
        }

        Vector3 escala = transform.localScale;

        if (player.position.x > transform.position.x)
        {
            escala.x = spriteOlhaParaDireita ? Mathf.Abs(escala.x) : -Mathf.Abs(escala.x);
        }
        else
        {
            escala.x = spriteOlhaParaDireita ? -Mathf.Abs(escala.x) : Mathf.Abs(escala.x);
        }

        transform.localScale = escala;
    }

    public void TocarAtaque()
    {
        if (animator != null)
        {
            animator.SetTrigger("Atacar");
        }
    }
}