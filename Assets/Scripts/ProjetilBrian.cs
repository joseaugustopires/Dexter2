using UnityEngine;

public class ProjetilBrian : MonoBehaviour
{
    public float velocidade = 6f;
    public float tempoDeVida = 4f;
    public int dano = 1;

    public LayerMask camadaObstaculo;

    private float direcao = -1f;

    public void ConfigurarDirecao(float novaDirecao)
    {
        direcao = novaDirecao;

        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * direcao;
        transform.localScale = escala;

        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        transform.position += Vector3.right * direcao * velocidade * Time.deltaTime;
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
            return;
        }

        if (((1 << collision.gameObject.layer) & camadaObstaculo) != 0)
        {
            Destroy(gameObject);
        }
    }
}