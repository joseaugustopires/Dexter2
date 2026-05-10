using UnityEngine;

public class ProjetilFaca : MonoBehaviour
{
    public float velocidade = 8f;
    public float tempoDeVida = 3f;
    public int dano = 1;

    private int direcao = 1;

    public void ConfigurarDirecao(int novaDirecao)
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
        VidaInimigo vidaInimigo = collision.GetComponent<VidaInimigo>();

        if (vidaInimigo != null)
        {
            vidaInimigo.TomarDano(dano, transform);
            Destroy(gameObject);
        }
    }
}