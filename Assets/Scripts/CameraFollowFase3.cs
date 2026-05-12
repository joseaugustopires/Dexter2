using UnityEngine;

public class CameraFollowFase3 : MonoBehaviour
{
    public Transform target;

    [Header("Suavidade")]
    public float smoothSpeed = 0.125f;

    [Header("Offset da câmera")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    [Header("Travar altura da câmera")]
    public bool travarY = true;
    public float yFixo = -0.1f;

    void Start()
    {
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            if (playerObj != null)
            {
                target = playerObj.transform;
            }
            else
            {
                Debug.LogError("CameraFollowFase3 não encontrou nenhum objeto com Tag Player.");
            }
        }
    }

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 posicaoDesejada = target.position + offset;

        if (travarY)
        {
            posicaoDesejada.y = yFixo;
        }

        posicaoDesejada.z = -10f;

        Vector3 posicaoSuavizada = Vector3.Lerp(
            transform.position,
            posicaoDesejada,
            smoothSpeed
        );

        transform.position = posicaoSuavizada;
    }
}