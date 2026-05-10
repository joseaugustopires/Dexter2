using UnityEngine;

public class ProgressoFase1 : MonoBehaviour
{
    public static bool jornalLido = false;

    void Start()
    {
        jornalLido = false;
    }

    public static void MarcarJornalLido()
    {
        jornalLido = true;
    }
}