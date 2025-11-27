using UnityEngine;

public class pickupScript : MonoBehaviour
{
    // Vector que indica la rotació que farà l’objecte en cada frame
    [SerializeField]
    Vector3 rotacio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Start s’executa una vegada al començar el joc (aquí no cal fer res concret)
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Faig rotar l’objecte segons el vector “rotacio”, que ho selecciono desde Unity
        transform.Rotate(rotacio);
    }
}
