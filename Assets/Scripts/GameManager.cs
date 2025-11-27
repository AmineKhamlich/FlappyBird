using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Textos de la UI que actualitzo durant el joc
    TextMeshProUGUI txtPuntuacio;
    TextMeshProUGUI txtObstacles;
    TextMeshProUGUI txtVida;
    TextMeshProUGUI txtTimer;
    TextMeshProUGUI txtVictoria;

    // Singleton: només vull un GameManager a tota la vida del joc
    public static GameManager instance;

    // Variables del joc
    public float timer;         // El temps que porto jugant
    int puntuacio;              // Monedes recollides
    int vida;                   // Vides restants
    int obstaclesCount;         // Obstacles superats


    // Awake es crida abans que Start
    void Awake()
    {
        // Si no existeix cap GameManager, aquest serà l'únic
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);                // Faig que no es destrueixi entre escenes
            SceneManager.sceneLoaded += OnSceneLoaded;    // M'aviso quan es carregui una escena

            // Inicialitzo les variables principals
            puntuacio = 0;
            vida = 5;
            obstaclesCount = 0;
            timer = 0f;
        }
        // Si ja existeix un GameManager, elimino aquest duplicat
        else
        {
            Destroy(gameObject);
        }
    }


    // Update es crida cada frame
    void Update()
    {
        // Incremento el temporitzador mentre el joc està en marxa
        timer += Time.deltaTime;
        txtTimer.text = FormatTime(timer);

        // Si estic a la segona pantalla, comprovo si he guanyat
        if (SceneManager.GetActiveScene().name == "MainScene2")
        {
            // Guanyo si arribo a 120 segons o 50 punts
            if (timer >= 120f || puntuacio >= 50)
            {
                Win();
            }
        }
    }


    // Converteixo el temps en minuts i segons
    string FormatTime(float time)
    {
        string minut = Mathf.Floor(time / 60).ToString("00");
        string seconds = Mathf.Floor(time % 60).ToString("00");
        return "Time: " + minut + ":" + seconds;
    }


    // Quan una escena acaba de carregar, torno a trobar tots els elements de la UI
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        IniciarReferencies();
    }


    // Agafo totes les referències als textos de la UI de l’escena
    void IniciarReferencies()
    {
        txtPuntuacio = GameObject.Find("txtPuntuacio").GetComponent<TextMeshProUGUI>();
        txtObstacles = GameObject.Find("txtObstacles").GetComponent<TextMeshProUGUI>();
        txtVida = GameObject.Find("txtVida").GetComponent<TextMeshProUGUI>();
        txtTimer = GameObject.Find("txtTimer").GetComponent<TextMeshProUGUI>();
        txtVictoria = GameObject.Find("txtVictoria").GetComponent<TextMeshProUGUI>();

        // Actualitzo tots els textos per començar correctament
        txtVida.text = "Vida: " + vida;
        txtObstacles.text = "Obstacles: " + obstaclesCount;
        txtPuntuacio.text = "Puntuació: " + puntuacio;

        // Al principi no vull mostrar la victòria
        txtVictoria.enabled = false;
    }


    // Reinicio l’escena quan perdo o xoco
    public void Reiniciar()
    {
        ResetGame();// Reinicio variables
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Reinicio totes les variables importants del joc
    public void ResetGame()
    {
        puntuacio = 0;
        vida = 5;
        obstaclesCount = 0;
        timer = 0f;

        // Actualitzo UI si els textos existeixen ja
        if (txtPuntuacio != null) txtPuntuacio.text = "Puntuació: " + puntuacio;
        if (txtVida != null) txtVida.text = "Vida: " + vida;
        if (txtObstacles != null) txtObstacles.text = "Obstacles: " + obstaclesCount;
        if (txtTimer != null) txtTimer.text = FormatTime(timer);
    }


    // Resto una vida quan un enemic em toca
    public void RestarVida()
    {
        vida--;

        // Si em quedo sense vides, reinicio el joc
        if (vida < 1)
        {
            ResetGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        txtVida.text = "Vida: " + vida;
    }


    // Sumo punts quan recullo pickups
    public void SumarPickUps()
    {
        puntuacio++;
        txtPuntuacio.text = "Puntuació: " + puntuacio;
    }


    // Sumo obstacles quan els supero
    public void SumarObstacles()
    {
        obstaclesCount++;
        txtObstacles.text = "Obstacles: " + obstaclesCount;
    }


    // El jugador guanya
    public void Win()
    {
        string escenaActual = SceneManager.GetActiveScene().name;

        // Si estic a la pantalla 1, passo a la 2
        if (escenaActual == "MainScene")
        {
            SceneManager.LoadScene("MainScene2");
        }
        else
        {
            // Si estic a la segona, guanyo el joc
            Time.timeScale = 0;       // Pauso el joc
            txtVictoria.enabled = true;
        }
    }
}



