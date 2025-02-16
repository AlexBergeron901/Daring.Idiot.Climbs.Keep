using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] Animator uneAnimation;
    [SerializeField] GameObject atterirFootstep;
    bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if ((horizontal != 0 || vertical != 0) && isGrounded == true)
        {
            uneAnimation.SetBool("Sauter", false);
            //Il faut juste ajouter une condition afin de savoir si tu as ramassé le lance roquette
            /*if (lanceRoquetteIdle.activeInHierarchy)
            {
                lanceRoquetteIdle.SetActive(false);
            }
            if (!lanceRoquetteRunning.activeInHierarchy)
            {
                lanceRoquetteRunning.SetActive(true);
            }*/
            if (Input.GetKey(KeyCode.Space))
            {
                uneAnimation.SetBool("Sauter", true);
            }
            uneAnimation.SetBool("Bouge", true);
        }
        //Rajouter une condition pour quand tu recule au lieu de quand tu avances
        else if (horizontal == 0 && vertical == 0)
        {
            /*if (!lanceRoquetteIdle.activeInHierarchy)
            {
                lanceRoquetteIdle.SetActive(true);
            }
            if (lanceRoquetteRunning.activeInHierarchy)
            {
                lanceRoquetteRunning.SetActive(false);
            }*/
            uneAnimation.SetBool("Bouge", false);
            if (Input.GetKey(KeyCode.Space))
            {
                uneAnimation.SetBool("Sauter", true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isGrounded = true;
            uneAnimation.SetBool("Sauter", false);
            atterirFootstep.GetComponent<FootStepVariation>().jouerFootStepSon();
        }
        if (other.transform.CompareTag("Boom"))
        {
            isGrounded = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
}
