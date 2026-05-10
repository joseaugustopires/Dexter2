using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SaidaFaseJornal : MonoBehaviour
{
    public string nomeDaProximaCena;
    public TMP_Text textoObjetivo;
    public string mensagemBloqueio = "Leia o jornal antes de seguir.";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        if (!ProgressoFase1.jornalLido)
        {
            if (textoObjetivo != null)
            {
                textoObjetivo.text = mensagemBloqueio;
            }

            return;
        }

        SceneManager.LoadScene(nomeDaProximaCena);
    }
}