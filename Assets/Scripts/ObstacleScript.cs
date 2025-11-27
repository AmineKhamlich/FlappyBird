using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    // Velocitat amb què es mou l’obstacle (cap amunt o cap avall)
    [SerializeField]
    float speed = 2.0f;

    // Flap force
    // Temps que triga a invertir el moviment (canviar de direcció)
    [SerializeField]
    float timeBetweenReverse = 0f;

    // Variable per accedir al component Rigidbody2D de l’obstacle
    Rigidbody2D obstacleBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Agafo el component Rigidbody2D del mateix objecte
        obstacleBody = GetComponent<Rigidbody2D>();
        // Li dono una velocitat inicial cap amunt multiplicant Vector2.up per la velocitat indicada
        obstacleBody.linearVelocity = Vector2.up * speed;

        // Switch velocity every timeBetweenReverse
        // Amb InvokeRepeating faig que es cridi el mètode Reverse cada cert temps (timeBetweenReverse)
        // Això serveix per fer que l’obstacle canviï de direcció de manera automàtica
        InvokeRepeating("Reverse", 0, timeBetweenReverse);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Reverse()
    {
        // invertim la direcció de la velocitat, és com canviar de sentit
        // Multiplico la velocitat per -1 per invertir el moviment
        obstacleBody.linearVelocity *= -1;
    }
}
