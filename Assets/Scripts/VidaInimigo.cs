using UnityEngine;

public class VidaInimigo : MonoBehaviour
{
    public int vida = 1;

    public void TomarDano(int dano)
    {
        vida -= dano;

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}