using UnityEngine;
using TMPro;

public class MensagemPistaDoakes : MonoBehaviour
{
    public TMP_Text textoPista;
    public Collider2D colliderSaida;

    void Start()
    {
        if (textoPista != null)
        {
            textoPista.text = "";
        }

        if (colliderSaida != null)
        {
            colliderSaida.enabled = false;
        }
    }

    public void PolicialMorreu()
    {
        if (textoPista != null)
        {
            textoPista.text = "Uma evidência foi deixada pelo policial corrupto. Investigue.";
        }
    }

    public void PistaColetada()
    {
        if (textoPista != null)
        {
            textoPista.text = "A evidência cita Doakes. Siga em frente.";
        }

        if (colliderSaida != null)
        {
            colliderSaida.enabled = true;
        }
    }
}