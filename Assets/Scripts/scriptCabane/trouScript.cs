using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class trouScript : MonoBehaviour
{

    [SerializeField] GameObject _canvas;
    ScriptNarrateur _Narrateur;
    GamerManager _gamerManager;
    // Selection de l'endroit ou le joueur va respawn
    [Header("1 = Forêt, 2 = Cabane, 3 = Mine, 4 = Keep")]
    [Range(1,4)]
    [SerializeField] int _niveauVers;
    bool isLoading;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        _Narrateur = GameObject.FindGameObjectWithTag("Narrateur").GetComponent<ScriptNarrateur>();
        isLoading = false;
    }
    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        //Active la téléportation vers une autre scèene
        if (other.gameObject.tag == "Player" && isLoading == false || other.gameObject.tag == "Target" && isLoading == false)
        {
            isLoading = true;
            StartCoroutine(loadSceneAsync());
        }
    }

    IEnumerator loadSceneAsync()
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(_niveauVers);
        /*if (SceneManager.GetActiveScene().buildIndex > _niveauVers)
        {
            _Narrateur.fallFromLevel();
        }*/
        
        operation.allowSceneActivation = false;
        

        while (!operation.isDone)
        {
            //_progression.fillAmount = operation.progress;
            if (_canvas != null)
            {
                _canvas.SetActive(true);
                //yield return new WaitForSeconds(1f);
            }
            if (operation.progress >= 0.9f)
                {
                if (_canvas != null)
                {
                    _canvas.SetActive(false);
                    yield return new WaitForSeconds(0.1f);
                }
                    operation.allowSceneActivation = true;
                }
                yield return null;
           

            }
        }
}
