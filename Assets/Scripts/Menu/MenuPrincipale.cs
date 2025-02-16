using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPrincipale : MonoBehaviour
{
    // Start is called before the first frame update
    public void Jouer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        TimeController.instance.BeginTimer();
    }

    public void Quitter()
    {
        Debug.Log("Quitter");
        Application.Quit();
    }

    
}
