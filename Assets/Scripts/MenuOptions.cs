using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{

    [SerializeField]
    GameObject panell;
    public void pausa()
    {
        Debug.Log("Pausem el joc");
        Time.timeScale = 0;
        panell.SetActive(true);
    }

    public void sortir()
    {
        Application.Quit();
    }

    public void reiniciar()
    {
        GameManager.instance.Reiniciar();
        resumir();
    }

    public void resumir()
    {
        Time.timeScale = 1;
        panell.SetActive(false);
    }
}
