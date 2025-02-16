using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForetManager : MonoBehaviour
{
    public GamerManager _GamerManager;
    public GameObject _player;
    public GameObject _SpawnCabin;
    public GameObject _SpawnKeep;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {
        _GamerManager = GameObject.FindGameObjectWithTag("GamerManager").GetComponent<GamerManager>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _SpawnCabin = GameObject.FindGameObjectWithTag("CabinFallPoint");
        _SpawnKeep = GameObject.FindGameObjectWithTag("KeepFallPoint");
        
        if (_GamerManager.fellFromCabin == true)
        {
            _player.transform.position = _SpawnCabin.transform.position;
            _GamerManager.fellFromCabin = false;
        }
        else if (_GamerManager.fellFromKeep == true)
        {
            _player.transform.position = _SpawnKeep.transform.position;
            _GamerManager.fellFromKeep = false;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
