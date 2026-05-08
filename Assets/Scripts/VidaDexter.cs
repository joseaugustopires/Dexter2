using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VidaDexter : MonoBehaviour
{
    public int vida = 1;
    public TMP_Text textoVida;

    void Start()
    {
        AtualizarTextoVida();
    }

    public void TomarDano(int dano)
    {
        vida -= dano;

        AtualizarTextoVida();

        if (vida <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void GanharVida(int quantidade)
    {
        vida += quantidade;
        AtualizarTextoVida();
    }

    void AtualizarTextoVida()
    {
        if (textoVida != null)
        {
            textoVida.text = "Vida: " + vida;
        }
    }
}