using UnityEngine;

public class AtivarMusicaFase4 : MonoBehaviour
{
    void Start()
    {
        if (MusicaFase1.instancia != null)
        {
            MusicaFase1.instancia.TocarMusicaFase4Brian();
        }
        else
        {
            Debug.LogWarning("MusicaFase1 não foi encontrada. Isso pode acontecer se você iniciou o jogo direto pela Fase 4.");
        }
    }
}