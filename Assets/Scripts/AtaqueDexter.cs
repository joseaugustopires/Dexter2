using System.Collections;
using UnityEngine;

public class AtaqueDexter : MonoBehaviour
{
    public Collider2D hitboxAtaque;
    public float tempoHitboxAtiva = 0.15f;
    public float tempoEntreAtaques = 0.3f;

    private bool podeAtacar = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && podeAtacar)
        {
            StartCoroutine(Atacar());
        }
    }

    IEnumerator Atacar()
    {
        podeAtacar = false;

        hitboxAtaque.enabled = true;

        yield return new WaitForSeconds(tempoHitboxAtiva);

        hitboxAtaque.enabled = false;

        yield return new WaitForSeconds(tempoEntreAtaques);

        podeAtacar = true;
    }
}