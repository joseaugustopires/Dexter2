using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarraVidaBrian : MonoBehaviour
{
    [Header("Referências")]
    public GameObject brianMoser;
    public Slider barraVida;
    public TMP_Text textoVida;
    public GameObject painelVida;

    [Header("Configuração da vida")]
    public int vidaMaximaBrian = 8;
    public bool definirVidaInicial = true;

    private VidaInimigo vidaBrian;
    private bool painelDesativado = false;

    void Start()
    {
        if (brianMoser != null)
        {
            vidaBrian = brianMoser.GetComponent<VidaInimigo>();
        }

        if (vidaBrian != null && definirVidaInicial)
        {
            vidaBrian.vida = vidaMaximaBrian;
        }

        if (barraVida != null)
        {
            barraVida.minValue = 0;
            barraVida.maxValue = vidaMaximaBrian;
            barraVida.value = vidaMaximaBrian;
        }

        if (painelVida != null)
        {
            painelVida.SetActive(true);
        }

        AtualizarBarra();
    }

    void Update()
    {
        if (vidaBrian == null)
        {
            EsconderBarra();
            return;
        }

        AtualizarBarra();
    }

    void AtualizarBarra()
    {
        if (vidaBrian == null)
        {
            return;
        }

        if (barraVida != null)
        {
            barraVida.value = vidaBrian.vida;
        }

        if (textoVida != null)
        {
            textoVida.text = "Brian Moser: " + vidaBrian.vida + "/" + vidaMaximaBrian;
        }
    }

    void EsconderBarra()
    {
        if (painelDesativado)
        {
            return;
        }

        painelDesativado = true;

        if (painelVida != null)
        {
            painelVida.SetActive(false);
        }
    }
}