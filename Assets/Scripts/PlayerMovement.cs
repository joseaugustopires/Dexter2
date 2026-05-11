using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    public Transform checarChao;
    public LayerMask camadaChao;
    public float raioChecarChao = 0.15f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float moveDirection;
    private bool estaNoChao;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        estaNoChao = Physics2D.OverlapCircle(checarChao.position, raioChecarChao, camadaChao);

        if (Input.GetButtonDown("Jump") && estaNoChao)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
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