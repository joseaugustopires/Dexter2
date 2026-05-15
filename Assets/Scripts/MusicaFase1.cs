using System.Collections;
using UnityEngine;

public class MusicaFase1 : MonoBehaviour
{
    public static MusicaFase1 instancia;

    [Header("Músicas")]
    public AudioClip musicaAnimada;
    public AudioClip musicaSuspense;
    public AudioClip musicaFase3Doakes;
    public AudioClip musicaFase4Brian;

    [Header("Configuração")]
    public float volume = 0.25f;
    public float tempoTransicao = 0.5f;

    [Header("Corte da música de suspense")]
    public float comecarSuspenseNoSegundo = 10f;

    [Header("Corte da música da Fase 3")]
    public float comecarFase3NoSegundo = 25f;

    [Header("Corte da música da Fase 4")]
    public float comecarFase4NoSegundo = 10f;

    private AudioSource audioSource;
    private bool jaMudouParaSuspense = false;
    private bool jaMudouParaFase3 = false;
    private bool jaMudouParaFase4 = false;

    void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (audioSource == null)
        {
            return;
        }

        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.volume = volume;

        if (musicaAnimada != null && !audioSource.isPlaying)
        {
            audioSource.clip = musicaAnimada;
            audioSource.time = 0f;
            audioSource.Play();
        }
    }

    public void TocarMusicaSuspense()
    {
        if (jaMudouParaSuspense)
        {
            return;
        }

        jaMudouParaSuspense = true;

        if (musicaSuspense != null)
        {
            StartCoroutine(TrocarMusicaComFade(musicaSuspense, comecarSuspenseNoSegundo));
        }
    }

    public void TocarMusicaFase3Doakes()
    {
        if (jaMudouParaFase3)
        {
            return;
        }

        jaMudouParaFase3 = true;

        if (musicaFase3Doakes != null)
        {
            StartCoroutine(TrocarMusicaComFade(musicaFase3Doakes, comecarFase3NoSegundo));
        }
    }

    public void TocarMusicaFase4Brian()
    {
        if (jaMudouParaFase4)
        {
            return;
        }

        jaMudouParaFase4 = true;

        if (musicaFase4Brian != null)
        {
            StartCoroutine(TrocarMusicaComFade(musicaFase4Brian, comecarFase4NoSegundo));
        }
    }

    IEnumerator TrocarMusicaComFade(AudioClip novaMusica, float comecarNoSegundo)
    {
        if (audioSource == null)
        {
            yield break;
        }

        float volumeInicial = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= volumeInicial * Time.deltaTime / tempoTransicao;
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = novaMusica;

        if (comecarNoSegundo > 0 && comecarNoSegundo < novaMusica.length)
        {
            audioSource.time = comecarNoSegundo;
        }
        else
        {
            audioSource.time = 0f;
        }

        audioSource.Play();

        while (audioSource.volume < volume)
        {
            audioSource.volume += volume * Time.deltaTime / tempoTransicao;
            yield return null;
        }

        audioSource.volume = volume;
    }

    public void PararMusica()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    public void DestruirMusica()
    {
        Destroy(gameObject);
    }
}