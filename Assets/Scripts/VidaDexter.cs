using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class VidaDexter : MonoBehaviour
{
    public int vida = 1;
    public int vidaMaxima = 2;

    public Slider barraVida;
    public TMP_Text textoVida;

    public float forcaKnockbackX = 1.2f;
    public float forcaKnockbackY = 0.3f;
    public float tempoKnockback = 0.15f;
    public float tempoInvulneravel = 0.8f;

    private bool invulneravel = false;

    void Start()
    {
        AtualizarUI();
    }

    public void TomarDano(int dano)
    {
        TomarDano(dano, null);
    }

    public void TomarDano(int dano, Transform origemDano)
    {
        DarkPassengerDexter darkPassenger = GetComponent<DarkPassengerDexter>();

        if (darkPassenger != null && darkPassenger.estaAtivo)
        {
            return;
        }

        if (invulneravel)
        {
            return;
        }

        vida -= dano;
        AtualizarUI();

        if (origemDano != null)
        {
            StartCoroutine(AplicarKnockback(origemDano));
        }

        if (vida <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        StartCoroutine(Invulnerabilidade());
    }

    public void GanharVida(int quantidade)
    {
        vida += quantidade;

        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }

        AtualizarUI();
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

    IEnumerator Invulnerabilidade()
    {
        invulneravel = true;
        yield return new WaitForSeconds(tempoInvulneravel);
        invulneravel = false;
    }

    void AtualizarUI()
    {
        if (barraVida != null)
        {
            barraVida.maxValue = vidaMaxima;
            barraVida.value = vida;
        }

        if (textoVida != null)
        {
            textoVida.text = "Vida: " + vida;
        }
    }
}