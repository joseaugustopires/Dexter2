using UnityEngine;

public class InteracaoJornal : MonoBehaviour
{
    public GameObject painelMensagem;
    public GameObject painelObjetivo;

    void Start()
    {
        if (painelMensagem != null)
        {
            painelMensagem.SetActive(false);
        }

        if (painelObjetivo != null)
        {
            painelObjetivo.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (painelMensagem != null)
            {
                painelMensagem.SetActive(true);
            }

            if (painelObjetivo != null)
            {
                painelObjetivo.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (painelMensagem != null)
            {
                painelMensagem.SetActive(false);
            }

            if (painelObjetivo != null)
            {
                painelObjetivo.SetActive(false);
            }
        }
    }
}