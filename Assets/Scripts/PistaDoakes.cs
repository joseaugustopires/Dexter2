using UnityEngine;

public class PistaDoakes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        MensagemPistaDoakes mensagem = FindObjectOfType<MensagemPistaDoakes>();

        if (mensagem != null)
        {
            mensagem.PistaColetada();
        }
        else
        {
            Debug.LogError("MensagemPistaDoakes não foi encontrado na cena.");
        }

        Destroy(gameObject);
    }
}