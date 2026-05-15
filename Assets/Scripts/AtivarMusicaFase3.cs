using UnityEngine;

public class AtivarMusicaFase3 : MonoBehaviour
{
    void Start()
    {
        if (MusicaFase1.instancia != null)
        {
            MusicaFase1.instancia.TocarMusicaFase3Doakes();
        }
        else
        {
            Debug.LogWarning("MusicaFase1 não foi encontrada. Isso pode acontecer se você iniciou o jogo direto pela Fase 3.");
        }
    }
}