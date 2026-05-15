using System.Collections;
using UnityEngine;

public class InteracaoJornal : MonoBehaviour
{
    [Header("Painéis")]
    public GameObject painelPressioneE;
    public GameObject painelHistoria;
    public GameObject painelObjetivo;

    [Header("Tempo da mensagem da história")]
    public float tempoMostrarHistoria = 5f;

    private bool jogadorPerto = false;
    private bool jornalLido = false;

    void Start()
    {
        if (painelPressioneE != null)
        {
            painelPressioneE.SetActive(false);
        }

        if (painelHistoria != null)
        {
            painelHistoria.SetActive(false);
        }

        if (painelObjetivo != null)
        {
            painelObjetivo.SetActive(false);
        }
    }

    void Update()
    {
        if (jogadorPerto && !jornalLido && Input.GetKeyDown(KeyCode.E))
        {
            LerJornal();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !jornalLido)
        {
            jogadorPerto = true;

            if (painelPressioneE != null)
            {
                painelPressioneE.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !jornalLido)
        {
            jogadorPerto = false;

            if (painelPressioneE != null)
            {
                painelPressioneE.SetActive(false);
            }
        }
    }

    void LerJornal()
    {
        jornalLido = true;

        if (painelPressioneE != null)
        {
            painelPressioneE.SetActive(false);
        }

        if (painelHistoria != null)
        {
            painelHistoria.SetActive(true);
        }

        if (painelObjetivo != null)
        {
            painelObjetivo.SetActive(true);
        }

        ProgressoFase1.MarcarJornalLido();

        if (MusicaFase1.instancia != null)
        {
            MusicaFase1.instancia.TocarMusicaSuspense();
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        Collider2D collider = GetComponent<Collider2D>();

        if (collider != null)
        {
            collider.enabled = false;
        }

        StartCoroutine(EsconderHistoriaDepoisDeUmTempo());
    }

    IEnumerator EsconderHistoriaDepoisDeUmTempo()
    {
        yield return new WaitForSeconds(tempoMostrarHistoria);

        if (painelHistoria != null)
        {
            painelHistoria.SetActive(false);
        }

        Destroy(gameObject);
    }
}