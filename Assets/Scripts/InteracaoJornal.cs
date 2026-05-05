using UnityEngine;

public class InteracaoJornal : MonoBehaviour
{
    public GameObject painelMensagem;

    void Start()
    {
        painelMensagem.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            painelMensagem.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            painelMensagem.SetActive(false);
        }
    }
}