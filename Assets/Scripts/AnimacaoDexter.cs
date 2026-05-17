using UnityEngine;

public class AnimacaoDexter : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D colisorPlayer;

    [Header("Configuração de movimento")]
    public float limiteMovimento = 0.1f;

    [Header("Chão")]
    public LayerMask camadaChao;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        colisorPlayer = GetComponent<Collider2D>();
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
        if (animator == null || colisorPlayer == null)
        {
            return;
        }

        bool estaNoChao = colisorPlayer.IsTouchingLayers(camadaChao);

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
}