using System.Collections;
using UnityEngine;

public class AtaqueDexter : MonoBehaviour
{
    public Collider2D hitboxAtaque;
    public float tempoHitboxAtiva = 0.15f;
    public float tempoEntreAtaques = 0.3f;
    public float distanciaHitbox = 0.32f;

    private bool podeAtacar = true;
    private bool olhandoDireita = true;

    void Update()
    {
        float movimentoHorizontal = Input.GetAxisRaw("Horizontal");

        if (movimentoHorizontal > 0)
        {
            olhandoDireita = true;
        }
        else if (movimentoHorizontal < 0)
        {
            olhandoDireita = false;
        }

        if (Input.GetMouseButtonDown(0) && podeAtacar)
        {
            StartCoroutine(Atacar());
        }
    }

    IEnumerator Atacar()
    {
        podeAtacar = false;

        Vector3 posicaoHitbox = hitboxAtaque.transform.localPosition;

        if (olhandoDireita)
        {
            posicaoHitbox.x = distanciaHitbox;
        }
        else
        {
            posicaoHitbox.x = -distanciaHitbox;
        }

        hitboxAtaque.transform.localPosition = posicaoHitbox;

        hitboxAtaque.enabled = true;

        yield return new WaitForSeconds(tempoHitboxAtiva);

        hitboxAtaque.enabled = false;

        yield return new WaitForSeconds(tempoEntreAtaques);

        podeAtacar = true;
    }
}