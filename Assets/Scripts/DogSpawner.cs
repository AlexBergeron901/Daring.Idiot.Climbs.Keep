using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSpawner : MonoBehaviour
{
    [SerializeField] GameObject _dog;
    GameObject _joueurPos;
    public Death KillManager;
    float spawnTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        KillManager = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<Death>();
       
    }

    // Update is called once per frame
    void Update()
    {
        _joueurPos = GameObject.FindGameObjectWithTag("Target");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Time.time > spawnTimer && KillManager.isDead == false && other.transform.CompareTag("Player") || Time.time > spawnTimer && KillManager.fallDeath == true && other.transform.CompareTag("Player"))
        {
            spawnTimer += 5f;
            var position = new Vector3(Random.Range(_joueurPos.transform.position.x + 10, _joueurPos.transform.position.x - 10), 0, Random.Range(_joueurPos.transform.position.z + 10, _joueurPos.transform.position.z - 10));
            Instantiate(_dog, position, Quaternion.identity);
            position = new Vector3(Random.Range(_joueurPos.transform.position.x + 10, _joueurPos.transform.position.x - 10), 0, Random.Range(_joueurPos.transform.position.z + 10, _joueurPos.transform.position.z - 10));
            Instantiate(_dog, position, Quaternion.identity);
            position = new Vector3(Random.Range(_joueurPos.transform.position.x + 10, _joueurPos.transform.position.x - 10), 0, Random.Range(_joueurPos.transform.position.z + 10, _joueurPos.transform.position.z - 10));
            Instantiate(_dog, position, Quaternion.identity);
        }


    }
    
}
