using UnityEngine;

public class InimigoBasico : MonoBehaviour
{
    public float velocidade = 2f;
    public float distancia = 3f;

    private Vector3 posicaoInicial;
    private int direcao = 1;

    void Start()
    {
        posicaoInicial = transform.position;
    }

    void Update()
    {
        transform.position += Vector3.right * direcao * velocidade * Time.deltaTime;

        if (transform.position.x >= posicaoInicial.x + distancia)
        {
            direcao = -1;
            Virar();
        }

        if (transform.position.x <= posicaoInicial.x - distancia)
        {
            direcao = 1;
            Virar();
        }
    }

    void Virar()
    {
        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * direcao;
        transform.localScale = escala;
    }
}