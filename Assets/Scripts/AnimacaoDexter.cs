using UnityEngine;

public class AnimacaoDexter : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    [Header("Movimento")]
    public float limiteMovimento = 0.1f;

    [Header("Checagem de chão")]
    public Transform checarChao;
    public float raioChecagemChao = 0.35f;
    public LayerMask camadaChao;

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
        if (animator == null)
        {
            return;
        }

        bool estaNoChao = false;

        if (checarChao != null)
        {
            estaNoChao = Physics2D.OverlapCircle(
                checarChao.position,
                raioChecagemChao,
                camadaChao
            );
        }

        animator.SetBool("Pulando", !estaNoChao);
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