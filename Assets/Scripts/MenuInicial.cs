using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    [Header("Cenas")]
    public string nomeCenaJogo = "Fase1_Miami";

    [Header("Painéis")]
    public GameObject painelComoJogar;

    void Start()
    {
        if (painelComoJogar != null)
        {
            painelComoJogar.SetActive(false);
        }
    }

    public void IniciarJogo()
    {
        SceneManager.LoadScene(nomeCenaJogo);
    }

    public void AbrirComoJogar()
    {
        if (painelComoJogar != null)
        {
            painelComoJogar.SetActive(true);
        }
    }

    public void FecharComoJogar()
    {
        if (painelComoJogar != null)
        {
            painelComoJogar.SetActive(false);
        }
    }

    public void SairDoJogo()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}