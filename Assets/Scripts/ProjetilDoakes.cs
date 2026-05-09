using UnityEngine;

public class ProjetilDoakes : MonoBehaviour
{
    public float velocidade = 6f;
    public float tempoDeVida = 3f;
    public int dano = 1;

    private Vector3 direcao;

    public void Configurar(Vector3 alvo)
    {
        direcao = (alvo - transform.position).normalized;
        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        transform.position += direcao * velocidade * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            VidaDexter vidaDexter = collision.GetComponent<VidaDexter>();

            if (vidaDexter != null)
            {
                vidaDexter.TomarDano(dano, transform);
            }

            Destroy(gameObject);
        }
    }
}