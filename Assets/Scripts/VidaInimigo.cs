using UnityEngine;

public class VidaInimigo : MonoBehaviour
{
    public int vida = 1;

    private bool morreu = false;

    public void TomarDano(int dano)
    {
        if (morreu)
        {
            return;
        }

        vida -= dano;

        if (vida <= 0)
        {
            morreu = true;

            SoltarEvidencia soltarEvidencia = GetComponent<SoltarEvidencia>();

            if (soltarEvidencia != null)
            {
                soltarEvidencia.Soltar();
            }

            if (DarkPassengerDexter.instancia != null)
            {
                DarkPassengerDexter.instancia.RegistrarInimigoDerrotado();
            }

            Destroy(gameObject);
        }
    }
}