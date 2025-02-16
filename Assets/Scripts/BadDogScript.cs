using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadDogScript : MonoBehaviour
{
    GameObject _player;
    [SerializeField] Rigidbody _dogRigidBody;
    [SerializeField] AudioSource _dogMouth;
    [SerializeField] AudioClip[] _listeClips;
    float _dogRunSpeed;
    int randClip;
    public Death KillManager;
    Collider dogCollider;
    //bool chase = true;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }
    private void Awake()
    {
        _dogRunSpeed = 15f;
        KillManager = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<Death>();
        
    }
    // Update is called once per frame
    void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Target");
        this.transform.LookAt(_player.transform);
        _dogRigidBody.velocity = _dogRigidBody.transform.forward * (_dogRunSpeed);
        if(KillManager.isDead == true)
        {
            StartCoroutine(IKillTheDogsHaHa(3f));
        }
        else
        {
            StartCoroutine(IKillTheDogsHaHa(7f));
        }
        StartCoroutine(WoofTimer());
    }
    IEnumerator WoofTimer()
    {
        
        if (_dogMouth.isPlaying == false)
        {
            randClip = Random.Range(0, _listeClips.Length);
            _dogMouth.PlayOneShot(_listeClips[randClip]);
        }
        yield return new WaitForSeconds(0.3f);
    }

    IEnumerator IKillTheDogsHaHa(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Target") || collision.transform.CompareTag("Ragdoll"))
        {
            KillManager.isSuperDead = true;
        }
    }
}
