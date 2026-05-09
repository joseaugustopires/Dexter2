using System.Collections;
using UnityEngine;

public class DoakesIA : MonoBehaviour
{
    public float velocidade = 2.5f;
    public float distanciaParaPerseguir = 8f;
    public float distanciaMinima = 2f;

    public GameObject prefabProjetil;
    public float intervaloAtaque = 2f;
    public float alturaDisparo = 0.4f;

    private Transform player;
    private bool podeAtacar = true;

    void Start()
    {
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

        float distancia = Vector2.Distance(transform.position, player.position);

        if (distancia <= distanciaParaPerseguir && distancia > distanciaMinima)
        {
            PerseguirPlayer();
        }

        if (distancia <= distanciaParaPerseguir && podeAtacar)
        {
            StartCoroutine(Atacar());
        }
    }

    void PerseguirPlayer()
    {
        float direcao = player.position.x > transform.position.x ? 1f : -1f;

        transform.position += Vector3.right * direcao * velocidade * Time.deltaTime;

        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * direcao;
        transform.localScale = escala;
    }

    IEnumerator Atacar()
    {
        podeAtacar = false;

        if (prefabProjetil != null)
        {
            Vector3 posicaoDisparo = transform.position + new Vector3(0, alturaDisparo, 0);
            GameObject projetil = Instantiate(prefabProjetil, posicaoDisparo, Quaternion.identity);

            ProjetilDoakes scriptProjetil = projetil.GetComponent<ProjetilDoakes>();

            if (scriptProjetil != null)
            {
                scriptProjetil.Configurar(player.position);
            }
        }

        yield return new WaitForSeconds(intervaloAtaque);

        podeAtacar = true;
    }
}