using UnityEngine;

public class HitboxAtaque : MonoBehaviour
{
    public int dano = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        VidaInimigo vidaInimigo = collision.GetComponent<VidaInimigo>();

        if (vidaInimigo != null)
        {
            vidaInimigo.TomarDano(dano, transform);
        }
    }
}