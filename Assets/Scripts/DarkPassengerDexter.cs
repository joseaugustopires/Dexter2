using System.Collections;
using UnityEngine;
using TMPro;

public class DarkPassengerDexter : MonoBehaviour
{
    public static DarkPassengerDexter instancia;

    public int inimigosParaCarregar = 3;
    public int inimigosDerrotados = 0;

    public float duracaoDarkPassenger = 3f;
    public bool estaAtivo = false;

    public TMP_Text textoDarkPassenger;

    private SpriteRenderer spriteRenderer;
    private Color corOriginal;

    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            corOriginal = spriteRenderer.color;
        }

        AtualizarTexto();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PodeAtivar())
        {
            StartCoroutine(AtivarDarkPassenger());
        }
    }

    public void RegistrarInimigoDerrotado()
    {
        if (estaAtivo)
        {
            return;
        }

        inimigosDerrotados++;

        if (inimigosDerrotados > inimigosParaCarregar)
        {
            inimigosDerrotados = inimigosParaCarregar;
        }

        AtualizarTexto();
    }

    bool PodeAtivar()
    {
        return inimigosDerrotados >= inimigosParaCarregar && estaAtivo == false;
    }

    IEnumerator AtivarDarkPassenger()
    {
        estaAtivo = true;
        inimigosDerrotados = 0;

        if (textoDarkPassenger != null)
        {
            textoDarkPassenger.text = "DARK PASSENGER ATIVO!";
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
        }

        yield return new WaitForSeconds(duracaoDarkPassenger);

        estaAtivo = false;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = corOriginal;
        }

        AtualizarTexto();
    }

    void AtualizarTexto()
    {
        if (textoDarkPassenger == null)
        {
            return;
        }

        if (inimigosDerrotados >= inimigosParaCarregar)
        {
            textoDarkPassenger.text = "Dark Passenger pronto! Pressione E";
        }
        else
        {
            textoDarkPassenger.text = "Dark Passenger: " + inimigosDerrotados + "/" + inimigosParaCarregar;
        }
    }
}