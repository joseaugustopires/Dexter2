using UnityEngine;
using TMPro;

public class SistemaDonuts : MonoBehaviour
{
    public int donuts = 0;
    public int donutsParaVidaExtra = 100;

    public TMP_Text textoDonuts;
    public VidaDexter vidaDexter;

    void Start()
    {
        AtualizarTexto();
    }

    public void AdicionarDonuts(int quantidade)
    {
        donuts += quantidade;

        if (donuts >= donutsParaVidaExtra)
        {
            donuts -= donutsParaVidaExtra;

            if (vidaDexter != null)
            {
                vidaDexter.GanharVida(1);
            }
        }

        AtualizarTexto();
    }

    void AtualizarTexto()
    {
        if (textoDonuts != null)
        {
            textoDonuts.text = "Donuts: " + donuts;
        }
    }
}