using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _murCollisionScript;
    [SerializeField] GameObject _cam;
    [SerializeField] float _movespeed;
    [SerializeField] float _maxspeed;
    double maxHeight;
    double currentHeight;
    public string deathSentence;
    public Death KillManager;
    bool isGrounded = false;
    public float sensitivityX = 15F;
    private float groundTimer = 0;
    private bool mountainGrounded = false;
    bool fallDeath;


    void Start()
    {


    }
    private void Awake()
    {
        fallDeath = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        maxHeight = 0;
        //KillManager = GetComponentInParent<Death>();
        KillManager = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<Death>();
    }
    // Update is called once per frame
    void Update()
    {

        move();
        jump();
        cam();
        killme();
        fallCheck();
        // groundCheck();
        //Cursor.visible = false;
    }
    public void fallCheck()
    {
        if (isGrounded == true || mountainGrounded == true)
        {
            maxHeight = 0;
            if (fallDeath == true)
            {
                KillManager.ImDead("fall");
            }
            fallDeath = false;
        }
        if (isGrounded == false)
        {
            currentHeight = transform.position.y;
            if (currentHeight > maxHeight)
            {
                maxHeight = currentHeight;
                //Debug.Log(maxHeight);
            }
            else if (currentHeight < maxHeight - 20)
            {
                fallDeath = true;

            }
        }

    }

    public void killme()
    {
        if (Input.GetKey(KeyCode.K) && KillManager.isDead == false)
        {
            KillManager.ImDead("suicide");
        }
    }
    //void groundCheck()
    //{
    //    if (isGrounded)
    //    {
    //        Debug.Log("Ground!");
    //    }
    //    else
    //    {
    //        Debug.Log("No Ground!");
    //    }
    //}
    void DeathCheck()
    {

    }

    void move()
    {
        //Vérifie que le joueur ne dépasse pas la vitesse maximale et touche au sol
        if (rb.velocity.magnitude < _maxspeed && isGrounded == true)
        {

            if (Input.GetKey(KeyCode.W))
            {
                rb.velocity = rb.transform.forward * _movespeed;
                //Debug.Log("W");
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.velocity = rb.transform.forward * -_movespeed;
                //Debug.Log("S");

            }
            if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
                {
                    rb.velocity += rb.transform.right * _movespeed;
                    // Debug.Log("W-D");

                }
                else
                {
                    rb.velocity = rb.transform.right * _movespeed;
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
                {
                    rb.velocity += rb.transform.right * -_movespeed;
                    //Debug.Log("A");

                }
                else
                {
                    rb.velocity = rb.transform.right * -_movespeed;
                    //Debug.Log("D");

                }
            }
        }

        // Si le joueur n'est pas sur le sol, réduire sa vitesse de déplacement dans les airs
        if (isGrounded == false)
        {
            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity += rb.transform.right * Time.deltaTime * (_movespeed) * 1.66666666667f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity += rb.transform.right * Time.deltaTime * -(_movespeed) * 1.66666666667f;
            }
            if (Input.GetKey(KeyCode.W))
            {
                rb.velocity += rb.transform.forward * Time.deltaTime * (_movespeed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.velocity += rb.transform.forward * Time.deltaTime * -(_movespeed);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Définit si le joueur est au sol ou non
        if (other.transform.CompareTag("Ground"))
        {
            isGrounded = true;
            maxHeight = 0;
        }
        if (other.transform.CompareTag("Mountain")){
            mountainGrounded = true;
        }
        if (other.transform.CompareTag("Boom"))
        {
            isGrounded = false;
            mountainGrounded = false;
        }
        if (other.transform.CompareTag("EnvironmentDeath"))
        {
            KillManager.ImDead("suicide");
        }
        if (other.transform.CompareTag("Dog"))
        {
            KillManager.ImDead("dog");
        }
        if (other.transform.CompareTag("Bombe"))
        {
            KillManager.ImDead("bomb");
        }
        if (other.transform.CompareTag("OutOfBounds"))
        {
            KillManager.ImDead("OutOfBounds");
            KillManager.isSuperDead = true;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        //Définit si le joueur est au sol selon un certain lapse de temps
        if (other.transform.CompareTag("Ground") && Time.time > groundTimer)
        {
            //groundTimer = Time.time + 0.20f;
            isGrounded = true;
        }
        if (other.transform.CompareTag("Mountain"))
        {
            mountainGrounded = true;
        }
    } 
    
    private void OnTriggerExit(Collider other)
    {
        //Définit si le joueur à quitter le sol
        if (other.transform.CompareTag("Ground"))
        {
           isGrounded = false;
        }
        if (other.transform.CompareTag("Mountain"))
        {
            mountainGrounded = false;
        }

    }

    void jump()
    {
        //Fonction saut
        if ((Input.GetKeyDown(KeyCode.Space)) && isGrounded == true)
        {
            groundTimer = Time.time + 0.50f;
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    
    void cam()
    {
        //Bouge la caméra en X, voir script "Camera" pour la caméra en Y
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);

    }

    
    /* private void OnTriggerEnter(Collider other)
    {
        
          Si le joueur atteint la limite, il aura alors 15s pour revenir dans 
          la zone de collision avec le tag MurInvisible, sinon dans le cas contraire, il sera punis
         
        if (other.transform.tag == "MurInvisibleLimite" && lastRoutine == null)
        {
            lastRoutine = StartCoroutine("TempsMort");
        }
        else if (other.transform.tag == "MurInvisible" && lastRoutine != null)
        {
            StopCoroutine(lastRoutine);
            lastRoutine = null;

            _murCollisionScript.GetComponent<murCollision>().murSafeZone(true);
        } 
    }
    */
    IEnumerator TempsMort()
    {
        _murCollisionScript.GetComponent<MurCollision>().MurDangerZone(true);
        yield return new WaitForSeconds(5f);
        _murCollisionScript.GetComponent<MurCollision>().MurDangerZone(false);
        yield return new WaitForSeconds(15f);
        _murCollisionScript.GetComponent<MurCollision>().MurTuMeurs(true);
        yield return new WaitForSeconds(5f);
        _murCollisionScript.GetComponent<MurCollision>().MurTuMeurs(false);
    }
}
