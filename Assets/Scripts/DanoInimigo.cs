using UnityEngine;

public class DanoInimigo : MonoBehaviour
{
    public int dano = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DarkPassengerDexter darkPassenger = collision.GetComponent<DarkPassengerDexter>();

            if (darkPassenger != null && darkPassenger.estaAtivo)
            {
                VidaInimigo vidaInimigo = GetComponent<VidaInimigo>();

                if (vidaInimigo != null)
                {
                    vidaInimigo.TomarDano(999);
                }

                return;
            }

            VidaDexter vidaDexter = collision.GetComponent<VidaDexter>();

            if (vidaDexter != null)
            {
                vidaDexter.TomarDano(dano, transform);
            }
        }
    }
}