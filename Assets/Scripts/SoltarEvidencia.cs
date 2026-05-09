using UnityEngine;

public class SoltarEvidencia : MonoBehaviour
{
    public GameObject prefabEvidencia;

    public void Soltar()
    {
        if (prefabEvidencia != null)
        {
            Instantiate(prefabEvidencia, transform.position, Quaternion.identity);
        }
    }
}
