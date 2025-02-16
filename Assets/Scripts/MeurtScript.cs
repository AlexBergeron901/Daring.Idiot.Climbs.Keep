using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class MeurtScript : MonoBehaviour
{

    [SerializeField] private GameObject _playerToInstantiate;
    [SerializeField] private GameObject _playerRagdoll;
    [SerializeField] private GameObject _explosionPrefab;
    [Header("Mettre la liste joueur, sinon en créer une")]
    [SerializeField] public GameObject _listeJoueur;
    [SerializeField] public Transform _spawnPoint;
    [SerializeField] private bool peutRebondir;
    //[SerializeField] private List<AudioClip> _sonMort = new List<AudioClip>();
    [SerializeField] AudioClip[] _listeMortSon;
    [SerializeField] AudioSource _sourceAudioMort;
    public bool isDead = false;
    
    //
    //Collider de l'objet tuant le joueur
    private Collider[] leColliderDie;

    private GameObject joueurRagdollTemp;
    private GameObject explosionTemp;
    
    private GameObject joueurToInstantiateCopy;

    private Vector3 positionActuel;
    private Vector3 positionTransformDie;

    private int _randomSon;

    // Pour les collisions avec un mur solide
    /* private void OnCollisionEnter(Collision collision)
     {
         if (collision.gameObject.tag == "Player")
         {
             Debug.Log("Boom" + this.name);
         }
     }
    */

    private void Awake()
    {
        leColliderDie = GetComponentsInChildren<Collider>();
    }

    // Explose le joueur
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("duplication ?" + 1);
        //Si joueur
        if (other.gameObject.tag == "Player")
        {
                //Désavtive tout les collider du joueur
                foreach (Collider collisionMort in leColliderDie)
                {
                    collisionMort.enabled = false;
                }

                float nombreJoueur = _listeJoueur.transform.childCount;
                //Position du joueur au moment de la mort
                positionActuel = other.transform.position;
                //Détruit le joueur
                Destroy(other.gameObject);

                //Active l'effet de mort
                StartCoroutine(DelaiAvantActivation());
        }
    }
    IEnumerator DelaiAvantActivation()
    {
        //Trouver un moyen d'avoir la position actuelle du joueur
        joueurRagdollTemp = Instantiate(_playerRagdoll, positionActuel, new Quaternion(0, 0, 0, 0), _listeJoueur.transform);
        if (peutRebondir == true)
        {
            explosionTemp = Instantiate(_explosionPrefab, joueurRagdollTemp.transform.position, new Quaternion(0, 0, 0, 0), _listeJoueur.transform);
        }
        yield return new WaitForSeconds(0.1f);

        if (_listeMortSon.Length > 0)
        {
            Debug.Log("Le son doit jouer");
            try
            {
                Debug.Log(_listeMortSon.Length);
                _randomSon = UnityEngine.Random.Range(0, _listeMortSon.Length);
                _sourceAudioMort = joueurRagdollTemp.GetComponent<AudioSource>();
                _sourceAudioMort.PlayOneShot(_listeMortSon[_randomSon]);
            }
            catch (Exception ex)
            {
                Debug.Log("Problème le son est en position : " + ex);
            }
        }

        yield return new WaitForSeconds(3f);
        
        //antiDuplication = true;
        Destroy(joueurRagdollTemp);

        if (peutRebondir == true)
        {
            Destroy(explosionTemp);
        }

        foreach (Collider collisionMort in leColliderDie)
        {
            collisionMort.enabled = true;
        }

        //Respawn le joueur au spawn
        joueurToInstantiateCopy = Instantiate(_playerToInstantiate, _spawnPoint.transform.position, new Quaternion(0, 0, 0, 0), _listeJoueur.transform);
        joueurToInstantiateCopy.transform.position = _spawnPoint.transform.position;
    }
}