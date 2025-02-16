using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherScript : MonoBehaviour
{
    [SerializeField] GameObject _rocket;
    [SerializeField] private float _delai = 0.5f;
    private float _canFire = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Tir();
    }

    void Tir()
    {
        if (Input.GetAxis("Fire1") == 1 && Time.time > _canFire)
        {
            //Tir à chaque 0.5 secondes
            _canFire = Time.time + _delai;
            //Instancie la rocket à la position du launcher
            Instantiate(_rocket, (transform.position), this.transform.rotation);
        }
    }
    
}
