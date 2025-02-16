using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    private bool _pauseOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !_pauseOn)
        {
            _pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            _pauseOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && _pauseOn)
        {
            _pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            Time.timeScale = 1;
            _pauseOn = false;
        }
    }

    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        Time.timeScale = 1;
        _pauseOn = false;
    }

    public void Quitter()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
