using UnityEngine;

public class AnimacaoDexter : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    [Header("Movimento")]
    public float limiteMovimento = 0.1f;

    [Header("Checagem de chão")]
    public Transform checarChao;
    public float raioChecagemChao = 0.18f;
    public LayerMask camadaChao;

    [Header("Pulo")]
    public float velocidadeMinimaPulo = 0.1f;
    public float tempoMinimoAnimacaoPulo = 0.18f;

    private float tempoPulando = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        AtualizarMovimento();
        AtualizarPulo();
        AtualizarAtaques();
    }

    void AtualizarMovimento()
    {
        if (animator == null || rb == null)
        {
            return;
        }

        float velocidadeHorizontal = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Velocidade", velocidadeHorizontal);
    }

    void AtualizarPulo()
    {
        if (animator == null || rb == null)
        {
            return;
        }

        bool estaNoChao = EstaEncostandoNoChao();
        bool estaSubindoOuCaindo = Mathf.Abs(rb.velocity.y) > velocidadeMinimaPulo;

        if (Input.GetButtonDown("Jump"))
        {
            tempoPulando = tempoMinimoAnimacaoPulo;
        }

        if (tempoPulando > 0)
        {
            tempoPulando -= Time.deltaTime;
        }

        bool deveMostrarPulo = tempoPulando > 0 || !estaNoChao || estaSubindoOuCaindo;

        animator.SetBool("Pulando", deveMostrarPulo);
    }

    bool EstaEncostandoNoChao()
    {
        if (checarChao == null)
        {
            return false;
        }

        Collider2D[] colisores = Physics2D.OverlapCircleAll(
            checarChao.position,
            raioChecagemChao,
            camadaChao
        );

        foreach (Collider2D colisor in colisores)
        {
            if (colisor != null && !colisor.isTrigger)
            {
                return true;
            }
        }

        return false;
    }

    void AtualizarAtaques()
    {
        if (animator == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Atacar");
        }

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("JogarFaca");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (checarChao != null)
        {
            Gizmos.DrawWireSphere(checarChao.position, raioChecagemChao);
        }
    }
}