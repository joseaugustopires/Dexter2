using UnityEngine;
using TMPro;

public class ObjetivoPolicial : MonoBehaviour
{
    public GameObject policialCorrupto;
    public Collider2D colliderSaida;
    public TMP_Text textoObjetivo;

    private bool objetivoConcluido = false;

    void Start()
    {
        if (colliderSaida != null)
        {
            colliderSaida.enabled = false;
        }
    }

    void Update()
    {
        if (objetivoConcluido)
        {
            return;
        }

        if (policialCorrupto == null)
        {
            objetivoConcluido = true;

            if (colliderSaida != null)
            {
                colliderSaida.enabled = true;
            }

            if (textoObjetivo != null)
            {
                textoObjetivo.text = "A evidência aponta para Doakes. Siga em frente.";
            }
        }
    }
}