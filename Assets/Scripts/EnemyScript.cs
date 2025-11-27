using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Velocitat de moviment de l’enemic
    [SerializeField]
    float speed = 2.0f;

    // Referència al jugador per saber on està en tot moment
    [SerializeField]
    Transform jugador;

    // Components de l’enemic
    Rigidbody2D enemyBody;
    SpriteRenderer Sp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Agafo el component Rigidbody2D per poder controlar el moviment físic
        enemyBody = GetComponent<Rigidbody2D>();
        // Agafo el component SpriteRenderer per poder mostrar o amagar l’enemic
        Sp = GetComponent<SpriteRenderer>();

        // Al principi amago l’enemic perquè no sigui visible fins que el jugador s’hi acosti
        Sp.enabled = false;

        // Busco el jugador dins de l’escena mitjançant el seu tag “Player”
        // i guardo el seu transform per saber sempre la seva posició
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculo la distància entre l’enemic i el jugador
        float dist = Vector2.Distance(transform.position, jugador.position);

        // Si el jugador està a menys de 8 
        if (dist < 8)
        {
            // Faig visible l'enemic
            Sp.enabled = true;
            // I li dono una velocitat cap a l’esquerra perquè comenci a moure’s
            enemyBody.linearVelocity = Vector2.left * speed;
        }
    }
}
