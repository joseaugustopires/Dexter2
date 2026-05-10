using UnityEngine;

public class SoltarEvidencia : MonoBehaviour
{
    public GameObject prefabEvidencia;

    public void Soltar()
    {
        MensagemPistaDoakes mensagem = FindObjectOfType<MensagemPistaDoakes>();

        if (mensagem != null)
        {
            mensagem.PolicialMorreu();
        }
        else
        {
            Debug.LogError("MensagemPistaDoakes não foi encontrado na cena.");
        }

        if (prefabEvidencia != null)
        {
            Instantiate(prefabEvidencia, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab Evidencia não foi configurado no PolicialCorrupto.");
        }
    }
}