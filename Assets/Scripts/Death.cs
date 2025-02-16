using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private GameObject _playerToInstantiate;
    [SerializeField] private GameObject _playerRagdoll;
    [SerializeField] private GameObject _explosionPrefab;
    [Header("Mettre la liste joueur, sinon en créer une")]
    //[SerializeField] public GameObject _listeJoueur;
    [SerializeField] public Transform _spawnPoint;
    [SerializeField] private bool peutRebondir;
    public bool isSuperDead;
    //[SerializeField] private List<AudioClip> _sonMort = new List<AudioClip>();
    [SerializeField] AudioClip[] _listeMortSon;
    [SerializeField] AudioSource _sourceAudioMort;
    public ScriptNarrateur narrateurScript;
    private Vector3 deathPos;
    
    int sound;
    private float killTimer = 0;
    public bool fallDeath = false;
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
    // Start is called before the first frame update
    void Start()
    {
        narrateurScript = GameObject.FindGameObjectWithTag("Narrateur").GetComponent<ScriptNarrateur>();
    }

    // Update is called once per frame
    void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        

       /* 
        if (GameObject.FindGameObjectWithTag("Target") != null)
        {
            _player = GameObject.FindGameObjectWithTag("Target");
            this.gameObject.transform.position = _player.transform.position;
        }
        else
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            this.gameObject.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z);
           
        }
       */

    }
    public void ImDead(string type)
    {
        if(isDead == false)
        {
            isDead = true;
            leColliderDie = _player.GetComponents<Collider>();
            foreach (Collider collisionMort in leColliderDie)
            {
                collisionMort.enabled = false;
            }
            //Position du joueur au moment de la mort
            positionActuel = _player.transform.position;
            Debug.Log(positionActuel);
            //Détruit le joueur
            Destroy(_player.gameObject);
            //Active l'effet de mort
            joueurRagdollTemp = null;
            narrateurScript.sassCheck();
            StartCoroutine(lookADeadguy(type));
        }
        
    }
    IEnumerator lookADeadguy(string type)
    {
        joueurRagdollTemp = Instantiate(_playerRagdoll, positionActuel, new Quaternion(0, 0, 0, 0), transform.parent.transform);
        switch (type)
        {
            case "suicide":
                killTimer = 3f;
                sound = 0;
                break;
            case "bomb":
                killTimer = 3f;
                sound = 0;
                peutRebondir = true;
                break;
            case "fall":
                killTimer = 3f;
                sound = 2;
                fallDeath = true;
                break;
            case "dog":
                killTimer = 3f;
                sound = 0;
                break;
            case "OutOfBounds":
                killTimer = 10f;
                sound = 1;
                isSuperDead = true;
                break;

        }
        if (peutRebondir == true)
        {
            explosionTemp = Instantiate(_explosionPrefab, joueurRagdollTemp.transform.position, new Quaternion(0, 0, 0, 0), transform.parent.transform);
        }
        yield return new WaitForSeconds(0.1f);
        if (_listeMortSon.Length > 0)
        {
            _randomSon = Random.Range(0, _listeMortSon.Length);
            _sourceAudioMort = joueurRagdollTemp.GetComponent<AudioSource>();
            _sourceAudioMort.PlayOneShot(_listeMortSon[sound]);
        }
        yield return new WaitForSeconds(killTimer);

        //antiDuplication = true;
        Vector3 posRespawn = GameObject.FindGameObjectWithTag("Target").transform.position;
        Destroy(joueurRagdollTemp);
        if (peutRebondir == true)
        {
            Destroy(explosionTemp);
        }

        //foreach (Collider collisionMort in leColliderDie)
        //{
        //collisionMort.enabled = true;
        //}

        //Respawn le joueur au spawn
        if (fallDeath == true && isSuperDead == false)
        {
            joueurToInstantiateCopy = Instantiate(_playerToInstantiate, posRespawn, new Quaternion(0, 0, 0, 0), transform.parent.transform);
            joueurToInstantiateCopy.transform.position = posRespawn;
        }
        else
        {
            joueurToInstantiateCopy = Instantiate(_playerToInstantiate, _spawnPoint.transform.position, new Quaternion(0, 0, 0, 0), transform.parent.transform);
            joueurToInstantiateCopy.transform.position = _spawnPoint.transform.position;
            StartCoroutine(SuperDeathRemoval());
            
        }
        isDead = false;
        peutRebondir = false;
        fallDeath = false;
        StopCoroutine("lookADeadguy");
    }
    IEnumerator SuperDeathRemoval()
    {
        yield return new WaitForSeconds(5f);
        isSuperDead = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OutOfBounds"))
        {
            isSuperDead = true;
            //Debug.Log("bitch");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("OutOfBounds"))
        {
            isSuperDead = true;
            //Debug.Log("bitch");
        }
    }

}
