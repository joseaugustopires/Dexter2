using UnityEngine;

public class DonutColetavel : MonoBehaviour
{
    public int valor = 10;
    public SistemaDonuts sistemaDonuts;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (sistemaDonuts != null)
            {
                sistemaDonuts.AdicionarDonuts(valor);
            }

            Destroy(gameObject);
        }
    }
}