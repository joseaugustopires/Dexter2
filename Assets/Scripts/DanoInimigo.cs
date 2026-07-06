using UnityEngine;

public class DanoInimigo : MonoBehaviour
{
    public int dano = 1;

    [Header("Pulo por cima")]
    public bool permitirPularPorCima = true;
    public float alturaMinimaParaIgnorarDano = 0.7f;
    public bool precisaEstarCaindo = true;

    [Header("Tempo entre danos")]
    public float tempoEntreDanos = 0.8f;

    private float ultimoTempoDano = -999f;
    private AnimacaoPolicial animacaoPolicial;

    private void Start()
    {
        // Busca o script de animação no próprio objeto ou no objeto pai
        animacaoPolicial = GetComponent<AnimacaoPolicial>();
        if (animacaoPolicial == null)
        {
            animacaoPolicial = GetComponentInParent<AnimacaoPolicial>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TentarDarDano(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TentarDarDano(collision);
    }

    void TentarDarDano(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        DarkPassengerDexter darkPassenger = collision.GetComponent<DarkPassengerDexter>();

        if (darkPassenger != null && darkPassenger.estaAtivo)
        {
            VidaInimigo vidaInimigo = GetComponent<VidaInimigo>();

            if (vidaInimigo != null)
            {
                vidaInimigo.TomarDano(999, collision.transform);
            }

            return;
        }

        if (permitirPularPorCima)
        {
            float diferencaAltura = collision.transform.position.y - transform.position.y;
            bool estaAltoOSuficiente = diferencaAltura >= alturaMinimaParaIgnorarDano;
            bool estaCaindo = true;

            Rigidbody2D rbPlayer = collision.GetComponent<Rigidbody2D>();

            if (rbPlayer != null && precisaEstarCaindo)
            {
                estaCaindo = rbPlayer.velocity.y <= 0;
            }

            if (estaAltoOSuficiente && estaCaindo)
            {
                return;
            }
        }

        if (Time.time < ultimoTempoDano + tempoEntreDanos)
        {
            return;
        }

        VidaDexter vidaDexter = collision.GetComponent<VidaDexter>();

        if (vidaDexter != null)
        {
            ultimoTempoDano = Time.time;
            vidaDexter.TomarDano(dano, transform);

            // GATILHOS DA ANIMAÇÃO
            if (animacaoPolicial != null)
            {
                animacaoPolicial.VirarParaPlayer(); // Vira na direção correta
                animacaoPolicial.TocarAtaque();     // Toca a animação de ataque
            }
        }
    }
}