using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    [Header("Checagem de chão")]
    public Transform checarChao;
    public LayerMask camadaChao;
    public float raioChecarChao = 0.25f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Collider2D colisorPlayer;

    private float moveDirection;
    private bool estaNoChao;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colisorPlayer = GetComponent<Collider2D>();
    }

    void Update()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");

        if (moveDirection > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveDirection < 0)
        {
            spriteRenderer.flipX = true;
        }

        VerificarChao();

        if (Input.GetButtonDown("Jump") && estaNoChao)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }

    void VerificarChao()
    {
        bool tocandoPeloChecarChao = false;
        bool tocandoPeloCollider = false;

        if (checarChao != null)
        {
            tocandoPeloChecarChao = Physics2D.OverlapCircle(
                checarChao.position,
                raioChecarChao,
                camadaChao
            );
        }

        if (colisorPlayer != null)
        {
            tocandoPeloCollider = colisorPlayer.IsTouchingLayers(camadaChao);
        }

        estaNoChao = tocandoPeloChecarChao || tocandoPeloCollider;
    }

    private void OnDrawGizmosSelected()
    {
        if (checarChao != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(checarChao.position, raioChecarChao);
        }
    }
}