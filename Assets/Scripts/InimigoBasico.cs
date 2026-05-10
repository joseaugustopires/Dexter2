using UnityEngine;

public class InimigoBasico : MonoBehaviour
{
    public float velocidade = 2f;
    public float distancia = 3f;

    public float velocidadePerseguindo = 3f;
    public float velocidadeVertical = 2f;
    public float distanciaMinimaDoPlayer = 0.8f;
    public float distanciaParaPerceberPlayer = 5f;

    public bool patrulharAntesDeVerPlayer = true;
    public bool descerParaAtacar = true;

    private Vector3 posicaoInicial;
    private int direcao = 1;

    private bool perseguindoPlayer = false;
    private Transform player;

    void Start()
    {
        posicaoInicial = transform.position;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        float distanciaPlayer = Vector2.Distance(transform.position, player.position);

        if (distanciaPlayer <= distanciaParaPerceberPlayer)
        {
            perseguindoPlayer = true;
        }

        if (perseguindoPlayer)
        {
            PerseguirPlayer();
        }
        else if (patrulharAntesDeVerPlayer)
        {
            Patrulhar();
        }
    }

    void Patrulhar()
    {
        transform.position += Vector3.right * direcao * velocidade * Time.deltaTime;

        if (transform.position.x >= posicaoInicial.x + distancia)
        {
            direcao = -1;
            Virar(direcao);
        }

        if (transform.position.x <= posicaoInicial.x - distancia)
        {
            direcao = 1;
            Virar(direcao);
        }
    }

    void PerseguirPlayer()
    {
        float distanciaPlayer = Vector2.Distance(transform.position, player.position);

        if (distanciaPlayer <= distanciaMinimaDoPlayer)
        {
            return;
        }

        float direcaoPlayer = player.position.x > transform.position.x ? 1f : -1f;

        Vector3 movimento = Vector3.right * direcaoPlayer * velocidadePerseguindo * Time.deltaTime;

        if (descerParaAtacar)
        {
            float novoY = Mathf.MoveTowards(
                transform.position.y,
                player.position.y,
                velocidadeVertical * Time.deltaTime
            );

            transform.position = new Vector3(
                transform.position.x + movimento.x,
                novoY,
                transform.position.z
            );
        }
        else
        {
            transform.position += movimento;
        }

        Virar(direcaoPlayer);
    }

    void Virar(float direcaoAtual)
    {
        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * direcaoAtual;
        transform.localScale = escala;
    }

    public void FicarAlertado()
    {
        perseguindoPlayer = true;
    }
}