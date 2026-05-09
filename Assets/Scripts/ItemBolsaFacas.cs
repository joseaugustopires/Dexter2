using UnityEngine;

public class ItemBolsaFacas : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AtaqueDistanciaDexter ataqueDistancia = collision.GetComponent<AtaqueDistanciaDexter>();

            if (ataqueDistancia != null)
            {
                ataqueDistancia.LiberarFacas();
                Destroy(gameObject);
            }
        }
    }
}