using UnityEngine;

public class InteracaoJornal : MonoBehaviour
{
    public GameObject painelMensagem;
    public GameObject painelObjetivo;

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

            ProgressoFase1.MarcarJornalLido();
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

            // Eu recomendo NÃO esconder o objetivo depois que ele leu o jornal.
            // Por isso deixei o painelObjetivo ligado.
        }
    }
}