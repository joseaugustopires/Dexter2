using UnityEngine;
using TMPro;

public class ObjetivoDoakes : MonoBehaviour
{
    public GameObject doakes;
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

        if (doakes == null)
        {
            objetivoConcluido = true;

            if (colliderSaida != null)
            {
                colliderSaida.enabled = true;
            }

            if (textoObjetivo != null)
            {
                textoObjetivo.text = "Doakes foi despistado. Siga para o esconderijo de Brian.";
            }
        }
    }
}