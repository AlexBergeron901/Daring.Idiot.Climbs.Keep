using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptFin : MonoBehaviour
{
    // Start is called before the first frame update
    TimeController _Ui;
    [SerializeField] AudioSource _borf;
    [SerializeField] AudioSource _whip;
    GamerManager _GamerManager;
    AudioSource _narrateur;
    bool flipped;
    void Start()
    {
        _GamerManager = GameObject.FindGameObjectWithTag("GamerManager").GetComponent<GamerManager>();
        _GamerManager.fellFromKeep = true;
        _GamerManager.fellFromCabin = false;
        _Ui = GameObject.FindGameObjectWithTag("UI").GetComponent<TimeController>();
        _narrateur = GameObject.FindGameObjectWithTag("Narrateur").GetComponent<AudioSource>();
        flipped = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Boom"))
        {
            Debug.Log("bctih");
            _narrateur.Stop();
            _borf.Play();
            StartCoroutine(Falling());
        }
        if (other.transform.CompareTag("Player") && flipped == false)
        {

            //transform.LookAt(new Vector3(transform.position.x, transform.position.y, other.transform.position.z));
            this.transform.Rotate(0f,0f, 180f);
            _whip.Play();
            flipped = true;
            //this.gameObject.transform.LookAt(other.transform.position);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    
    IEnumerator Falling()
    {
        yield return new WaitForSeconds(8f);
        _Ui.Victory();
    }
}
