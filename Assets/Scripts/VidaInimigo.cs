using System.Collections;
using UnityEngine;

public class VidaInimigo : MonoBehaviour
{
    public int vida = 1;

    public float forcaKnockbackX = 0.5f;
    public float forcaKnockbackY = 0.1f;
    public float tempoKnockback = 0.12f;

    private bool morreu = false;

    public void TomarDano(int dano)
    {
        TomarDano(dano, null);
    }

    public void TomarDano(int dano, Transform origemDano)
    {
        if (morreu)
        {
            return;
        }

        vida -= dano;

        InimigoBasico inimigoBasico = GetComponent<InimigoBasico>();

if (inimigoBasico != null)
{
    inimigoBasico.FicarAlertado();
}

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
            return;
        }

        if (origemDano != null)
        {
            StartCoroutine(AplicarKnockback(origemDano));
        }
    }

    IEnumerator AplicarKnockback(Transform origemDano)
    {
        float direcao = transform.position.x > origemDano.position.x ? 1f : -1f;

        Vector3 posicaoInicial = transform.position;
        Vector3 posicaoFinal = posicaoInicial + new Vector3(direcao * forcaKnockbackX, forcaKnockbackY, 0);

        float tempo = 0f;

        while (tempo < tempoKnockback)
        {
            transform.position = Vector3.Lerp(posicaoInicial, posicaoFinal, tempo / tempoKnockback);
            tempo += Time.deltaTime;
            yield return null;
        }

        transform.position = posicaoFinal;
    }
}