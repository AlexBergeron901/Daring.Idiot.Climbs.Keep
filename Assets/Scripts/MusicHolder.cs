using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHolder : MonoBehaviour
{
    [SerializeField] public int _clip;
    
    public MusicManager _MusicManager;
    // Start is called before the first frame update
    void Start()
    {
        _MusicManager = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
    }
    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
       // _MusicManager = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
        //Debug.Log(_MusicManager);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("egg");

        if (other.transform.CompareTag("Narrateur"))
        {
            _MusicManager.clipNumber = _clip;
            _MusicManager.PlayMusic();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        //_MusicManager.interrupt(); 
    }
}
