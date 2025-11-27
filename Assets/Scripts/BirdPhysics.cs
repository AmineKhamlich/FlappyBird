using System.Collections;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BirdPhysics : MonoBehaviour
{
    // Velocitat horitzontal constant
    [SerializeField]
    float speed = 2.0f;

    // Força cap amunt quan faig clic per saltar
    [SerializeField]
    float force = 200.0f;

    // Referència al Rigidbody2D per controlar el moviment físic de l’ocell
    Rigidbody2D birdBody;


    // S'executa només un cop al començar
    void Start()
    {
        // Agafo el Rigidbody2D del meu ocell
        birdBody = GetComponent<Rigidbody2D>();

        // Impulso l’ocell cap a la dreta amb velocitat constant
        birdBody.linearVelocity = Vector2.right * speed;
    }


    // Update es crida cada frame
    void Update()
    {
        // Quan faig clic amb el botó esquerre, salto cap amunt
        if (Input.GetMouseButtonDown(0))
        {
            // Aplico una força vertical cap amunt
            birdBody.AddForce(Vector2.up * force);
        }
    }


    // Quan xoco amb obstacles
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si toco el terra
        GameManager.instance.Reiniciar();
    }


    // Quan entro dins d'un trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si toco una moneda o pickup
        if (collision.CompareTag("pickup"))
        {
            Destroy(collision.gameObject);          // La recullo
            GameManager.instance.SumarPickUps();    // Sumo un punt
        }

        // Si topa un enemic contra mi
        else if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);          // El faig desaparèixer
            GameManager.instance.RestarVida();      // Perdo una vida
        }

        // Trigger per comptar obstacles superats
        else if (collision.CompareTag("trigger"))
        {
            GameManager.instance.SumarObstacles();
        }

        // Si arribo al final del nivell
        else if (collision.CompareTag("Finish"))
        {
            GameManager.instance.Win(); // Guanyo la partida o vaig a la seguent pantalla
        }
    }
}
