using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Assigno aquí l'ocell perquè la càmera el pugui seguir
    [SerializeField]
    Transform targetBird;


    // S'executa un cop al començament
    void Start()
    {
        // Aquí no cal fer res, només necessito tenir referència al meu ocell
    }


    // Update es crida cada frame
    void Update()
    {
        // Faig que la càmera segueixi l'ocell només en l'eix X
        // Mantinguent la Y i la Z originals de la càmera
        transform.position = new Vector3(
            targetBird.position.x,
            transform.position.y,
            transform.position.z
        );
    }
}