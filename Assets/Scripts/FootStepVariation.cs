using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepVariation : MonoBehaviour
{
    [SerializeField] AudioClip[] listeClipWood;
    [SerializeField] AudioClip[] listeClipGrass;
    [SerializeField] AudioClip[] listeClipConcrete;
    [SerializeField] AudioClip[] listeClipSand;
    [SerializeField] AudioSource sourceAudio;
    [SerializeField] float pitchMin, pitchMax, volMin, volMax;

    int indexSon;
    int sonVariation;

    string detectionMat;

    //Méthode pour faire jouer le son (attaché aux frames de l'animation)
    public void jouerFootStepSon()
    {
        //Change le pitch pour faire varier le son
        sourceAudio.pitch = Random.Range(pitchMin, pitchMax);
        //Change le volume pour agrémenter le son
        sourceAudio.volume = Random.Range(volMin, volMax);

        switch(detectionMat)
        {
            //Si la variable de détection est égal à Grass
            case "Grass":
                //On va chercher une valeur aléatoire
                indexSon = Random.Range(0, listeClipGrass.Length);
                //Si je met un while, ça fait crash quand tu entres entre deux collisions (coincé)
                //Si l'index est identique à celui d'avant
                if (indexSon == sonVariation)
                {
                    //On relance une deuxième fois pour avoir un nombre aléatoire
                    indexSon = Random.Range(0, listeClipGrass.Length);
                }
                //Après on vérifie que le son peut-être jouer
                if (indexSon < listeClipGrass.Length && indexSon >= 0)
                {
                    //Fait jouer le son à la position valide
                    sourceAudio.PlayOneShot(listeClipGrass[indexSon]);
                }
                break;
            case "Concrete":
                indexSon = Random.Range(0, listeClipConcrete.Length);
                //Si je met un while, ça fait crash quand tu entres entre deux collisions (coincé)
                if (indexSon == sonVariation)
                {
                    indexSon = Random.Range(0, listeClipConcrete.Length);
                }
                if (indexSon < listeClipConcrete.Length && indexSon >= 0)
                {
                    sourceAudio.PlayOneShot(listeClipConcrete[indexSon]);
                }
                break;
            case "Wood":
                indexSon = Random.Range(0, listeClipWood.Length);
                //Si je met un while, ça fait crash quand tu entres entre deux collisions (coincé)
                if (indexSon == sonVariation)
                {
                    indexSon = Random.Range(0, listeClipWood.Length);
                }
                if (indexSon < listeClipWood.Length && indexSon >= 0)
                {
                    sourceAudio.PlayOneShot(listeClipWood[indexSon]);
                }
                break;
            case "Sand":
                indexSon = Random.Range(0, listeClipSand.Length);
                //Si je met un while, ça fait crash quand tu entres entre deux collisions (coincé)
                if (indexSon == sonVariation)
                {
                    indexSon = Random.Range(0, listeClipSand.Length);
                }
                if (indexSon < listeClipSand.Length && indexSon >= 0)
                {
                    sourceAudio.PlayOneShot(listeClipSand[indexSon]);
                }
                sonVariation = indexSon;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Une fois qu'on entre en collision avec le sol
    private void OnCollisionStay(Collision collision)
    {
        //Va chercher le layer du gameObject
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetAxis("Mouse X") != 0)
        {
            //Si le numéro de layer = nombre choisi
            if (collision.gameObject.layer.ToString() == "8")
            {
                //met la variable à Grass depuis le string
                detectionMat = "Grass";
            }
            else if (collision.gameObject.layer.ToString() == "9")
            {
                detectionMat = "Wood";
            }
            else if (collision.gameObject.layer.ToString() == "10")
            {
                detectionMat = "Concrete";
            }
            else if (collision.gameObject.layer.ToString() == "11")
            {
                detectionMat = "Sand";
            }
        }
    }
}
