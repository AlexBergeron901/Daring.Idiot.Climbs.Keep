using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomScript : MonoBehaviour
{
    [SerializeField] float _magnitude;
    [SerializeField] SphereCollider _hitbox;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(boomCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator boomCoroutine()
    {
        yield return new WaitForSeconds(0.15f);
        _hitbox.enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            if (other.isTrigger)
            {

            }
            else
            {
                if (other.transform.tag != "Ground")
                {
                    var direction = (transform.position - other.transform.position) * -1;
                    float distance = Vector3.Distance(transform.position, other.transform.position);
                    direction.Normalize();
                    other.GetComponent<Rigidbody>().AddForce(direction * _magnitude);
                }
            }
            
        }
    }
}
