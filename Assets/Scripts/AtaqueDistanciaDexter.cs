using System.Collections;
using UnityEngine;

public class AtaqueDistanciaDexter : MonoBehaviour
{
    public GameObject prefabFaca;

    public float distanciaSpawn = 0.8f;
    public float alturaSpawn = 0.2f;
    public float tempoEntreFacas = 0.4f;

    private bool temFacas = false;
    private bool podeAtirar = true;
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

        if (Input.GetMouseButtonDown(1) && temFacas && podeAtirar)
        {
            StartCoroutine(ArremessarFaca());
        }
    }

    IEnumerator ArremessarFaca()
    {
        podeAtirar = false;

        int direcao = olhandoDireita ? 1 : -1;

        Vector3 posicaoSpawn = transform.position + new Vector3(distanciaSpawn * direcao, alturaSpawn, 0);

        GameObject facaCriada = Instantiate(prefabFaca, posicaoSpawn, Quaternion.identity);

        ProjetilFaca projetil = facaCriada.GetComponent<ProjetilFaca>();

        if (projetil != null)
        {
            projetil.ConfigurarDirecao(direcao);
        }

        yield return new WaitForSeconds(tempoEntreFacas);

        podeAtirar = true;
    }

    public void LiberarFacas()
    {
        temFacas = true;
    }
}