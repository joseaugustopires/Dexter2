using UnityEngine;

public class EvidenciaChutavel : MonoBehaviour
{
    public float velocidade = 8f;
    public float tempoDeVidaDepoisDoChute = 3f;
    public int dano = 999;

    private bool foiChutada = false;
    private int direcao = 1;

    void Update()
    {
        if (foiChutada)
        {
            transform.position += Vector3.right * direcao * velocidade * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!foiChutada && collision.CompareTag("Player"))
        {
            direcao = transform.position.x > collision.transform.position.x ? 1 : -1;
            foiChutada = true;

            Vector3 escala = transform.localScale;
            escala.x = Mathf.Abs(escala.x) * direcao;
            transform.localScale = escala;

            Destroy(gameObject, tempoDeVidaDepoisDoChute);
            return;
        }

        if (foiChutada)
        {
            VidaInimigo vidaInimigo = collision.GetComponent<VidaInimigo>();

            if (vidaInimigo != null)
            {
                vidaInimigo.TomarDano(dano);
                Destroy(gameObject);
            }
        }
    }
}