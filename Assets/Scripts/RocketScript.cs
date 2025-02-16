using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
    
{
    [SerializeField] GameObject _boomObject;
    [SerializeField] float _projectileSpeed;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _launcher;
    [SerializeField] AudioClip _sonRocket;
    [SerializeField] AudioSource _sonLR;
    

    private bool blownUp;

    // Start is called before the first frame update
    void Start()
    {
        _sonLR.PlayOneShot(_sonRocket);
        StartCoroutine(TempsVol());
    }
    private void Awake()
    {
        blownUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * -_projectileSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (blownUp == false)
        {
            ContactPoint contact = collision.contacts[0];
            Instantiate(_boomObject, contact.point, Quaternion.identity);
            blownUp = true;
        }
        
        Destroy(this.gameObject);

        if (collision.transform.tag != "")
        {

        }
    }

    IEnumerator TempsVol()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
