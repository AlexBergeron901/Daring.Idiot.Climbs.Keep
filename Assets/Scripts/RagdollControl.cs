using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollControl : MonoBehaviour
{
    [SerializeField] private GameObject _joueurSansRagdoll;
    [SerializeField] private GameObject _joueurAvecRagdoll;

    [SerializeField] private Rigidbody _joueurRigidBody;
    [SerializeField] private CapsuleCollider _joueurCapsuleCollider;
    public Death KillManager;

    //Vérifier plus tard si c'est problématique
    [field: SerializeField] public bool mortActive { get;  set; }

    private Rigidbody[] joueurRigidBodies;
    private Collider[] joueurColliders;

    private void Awake()
    {
        KillManager = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<Death>();
        joueurRigidBodies = GetComponentsInChildren<Rigidbody>();
        joueurColliders = GetComponentsInChildren<Collider>();
    }

    private void Update()
    {
        ActivationRagdoll();
    }

    public bool ActivationRagdoll()
    {
        if (mortActive == true)
        {
            _joueurCapsuleCollider.enabled = false;
            _joueurRigidBody.isKinematic = true;

            DefinirColliders(true);

            return true;
        }
        else
        {
            _joueurCapsuleCollider.enabled = true;
            _joueurRigidBody.isKinematic = false;

            DefinirColliders(true);
            DefinirRigidKine(false);

            return false;
        }
    }

    private void DefinirColliders(bool estActif)
    {
        foreach (Collider joueurCol in joueurColliders)
        {
            joueurCol.enabled = estActif;
        }
    }

    private void DefinirRigidKine(bool estActif)
    {
        foreach (Rigidbody joueurRb in joueurRigidBodies)
        {
            joueurRb.isKinematic = estActif;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            //StartCoroutine(tempsMortAvantDeFiger());
        }
        if (collision.transform.CompareTag("OutOfBounds"))
        {
            KillManager.isSuperDead = true;
            //Debug.Log("OH GOD IM FUCKING DED 2");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("OutOfBounds"))
        {
            KillManager.isSuperDead = true;
            //Debug.Log("OH GOD IM FUCKING DED");
        }
    }

    IEnumerator tempsMortAvantDeFiger()
    {
        yield return new WaitForSeconds(3f);
        DefinirRigidKine(true);
    }
}
