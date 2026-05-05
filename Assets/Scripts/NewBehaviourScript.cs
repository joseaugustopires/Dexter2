using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject painelMensagem;

    private void Start()
    {
        painelMensagem.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            painelMensagem.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            painelMensagem.SetActive(false);
        }
    }
}