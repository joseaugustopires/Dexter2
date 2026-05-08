using UnityEngine;
using UnityEngine.SceneManagement;

public class SaidaFase : MonoBehaviour
{
    public string nomeDaProximaCena;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(nomeDaProximaCena);
        }
    }
}