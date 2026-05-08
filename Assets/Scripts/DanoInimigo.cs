using UnityEngine;

public class DanoInimigo : MonoBehaviour
{
    public int dano = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            VidaDexter vidaDexter = collision.GetComponent<VidaDexter>();

            if (vidaDexter != null)
            {
                vidaDexter.TomarDano(dano, transform);
            }
        }
    }
}