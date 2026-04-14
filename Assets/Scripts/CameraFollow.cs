using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Alvo e Configurações")]
    public Transform target; // O que a câmera vai seguir (nosso Player)
    public float smoothSpeed = 0.125f; // O quão suave é o movimento da câmera
    public Vector3 offset = new Vector3(0f, 2f, -10f); // A distância da câmera em relação ao jogador

    // Usamos o LateUpdate para a câmera porque ele roda DEPOIS que o jogador já se moveu no Update, evitando "soquinhos" na imagem.
    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula qual deve ser a posição final da câmera
            Vector3 desiredPosition = target.position + offset;
            
            // Move a câmera suavemente da posição atual para a desejada usando Lerp
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}