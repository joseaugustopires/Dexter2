using UnityEngine;
using TMPro;

public class FinalBoss : MonoBehaviour
{
    public GameObject brian;
    public GameObject panelFimDemo;
    public TMP_Text textoObjetivo;

    private bool terminou = false;

    void Update()
    {
        if (terminou)
        {
            return;
        }

        if (brian == null)
        {
            terminou = true;

            if (panelFimDemo != null)
            {
                panelFimDemo.SetActive(true);
            }

            if (textoObjetivo != null)
            {
                textoObjetivo.text = "Brian Moser foi derrotado.";
            }
        }
    }
}