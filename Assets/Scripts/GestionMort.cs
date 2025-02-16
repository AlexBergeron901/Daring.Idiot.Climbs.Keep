using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionMort : MonoBehaviour
{
    [SerializeField] protected bool _peutMourrir;
    [SerializeField] protected bool _peutRebondir;
    [SerializeField] protected GameObject _joueurRagdoll;
    [SerializeField] protected GameObject _joueurRespawn;
    [SerializeField] protected GameObject _spawnPoint;
    [SerializeField] protected GameObject _listeJoueur;
    [SerializeField] protected Vector3 positionActuel;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] protected AudioClip[] _listeMortSon;
    [SerializeField] protected AudioSource _sourceAudioMort;

    private GameObject joueurToInstantiateCopy;
    private int _randomSon;
    private GameObject explosionTemp;

    public void TuerJoueur()
    {
        _joueurRagdoll = Instantiate(_joueurRagdoll, positionActuel, new Quaternion(0, 0, 0, 0), _listeJoueur.transform);
        if (_peutRebondir == true)
        {
            explosionTemp = Instantiate(_explosionPrefab, _joueurRagdoll.transform.position, new Quaternion(0, 0, 0, 0), _listeJoueur.transform);
        }

        if (_listeMortSon.Length > 0)
        {
            try
            {
                Debug.Log(_listeMortSon.Length);
                _randomSon = UnityEngine.Random.Range(0, _listeMortSon.Length);
                _sourceAudioMort = _joueurRagdoll.GetComponent<AudioSource>();
                _sourceAudioMort.PlayOneShot(_listeMortSon[_randomSon]);
            }
            catch (Exception ex)
            {
                Debug.Log("Problème le son est en position : " + ex);
            }
        }

        Destroy(_joueurRagdoll);

        if (_peutRebondir == true)
        {
            Destroy(explosionTemp);
        }

        //Respawn le joueur au spawn
        joueurToInstantiateCopy = Instantiate(_joueurRespawn, _spawnPoint.transform.position, new Quaternion(0, 0, 0, 0), _listeJoueur.transform);
        joueurToInstantiateCopy.transform.position = _spawnPoint.transform.position;
    }
}
