using UnityEngine;
using TMPro;

public class ObjetivoPolicial : MonoBehaviour
{
    public Collider2D colliderSaida;
    public TMP_Text textoObjetivo;

    private bool pistaColetada = false;

    void Start()
    {
        if (colliderSaida != null)
        {
            colliderSaida.enabled = false;
        }
    }

    public void PolicialFoiDerrotado()
    {
        if (textoObjetivo != null)
        {
            textoObjetivo.text = "Investigue a evidência deixada pelo policial corrupto.";
        }
        else
        {
            Debug.LogError("Texto Objetivo não foi configurado no ControleFase2.");
        }
    }

    public void ColetarPistaDoakes()
    {
        if (pistaColetada)
        {
            return;
        }

        pistaColetada = true;

        if (textoObjetivo != null)
        {
            textoObjetivo.text = "A evidência cita Doakes. Siga em frente.";
        }

        if (colliderSaida != null)
        {
            colliderSaida.enabled = true;
        }
        else
        {
            Debug.LogError("Collider Saida não foi configurado no ControleFase2.");
        }
    }
}