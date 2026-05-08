using UnityEngine;

public class ItemVida : MonoBehaviour
{
    public int vidaGanha = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            VidaDexter vidaDexter = collision.GetComponent<VidaDexter>();

            if (vidaDexter != null)
            {
                vidaDexter.GanharVida(vidaGanha);
                Destroy(gameObject);
            }
        }
    }
}