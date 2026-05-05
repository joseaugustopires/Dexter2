using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f; // Força do pulo que você pode ajustar no Unity
 
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; // Componente responsável por desenhar e virar o sprite
    private float moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Lê as setas do teclado (A/D)
        moveDirection = Input.GetAxisRaw("Horizontal");

        // Vira o personagem para a direita ou esquerda
        if (moveDirection > 0)
        {
            spriteRenderer.flipX = false; // Olha para a direita
        }
        else if (moveDirection < 0)
        {
            spriteRenderer.flipX = true; // Olha para a esquerda
        }

        // Verifica se apertou a barra de Espaço
        if (Input.GetButtonDown("Jump"))
        {
            // Dá um empurrãozinho para cima
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // Aplica o movimento contínuo
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }
}