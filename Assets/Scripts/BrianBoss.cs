using System.Collections;
using UnityEngine;

public class BrianBoss : MonoBehaviour
{
    public GameObject prefabProjetil;
    public float intervaloAtaque = 1.5f;
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

        VirarParaPlayer();

        if (podeAtacar)
        {
            StartCoroutine(Atacar());
        }
    }

    void VirarParaPlayer()
    {
        float direcao = player.position.x > transform.position.x ? 1f : -1f;

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

            ProjetilBrian scriptProjetil = projetil.GetComponent<ProjetilBrian>();

            if (scriptProjetil != null)
            {
                scriptProjetil.Configurar(player.position);
            }
        }

        yield return new WaitForSeconds(intervaloAtaque);

        podeAtacar = true;
    }
}