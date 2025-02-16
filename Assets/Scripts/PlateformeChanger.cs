using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeChanger : MonoBehaviour
{
    //Plateforme mobile
    [Tooltip("Indique à la plateforme le temps avant qu'elle revienne.")]
    [Header("Temps avant de revenir")]
    [SerializeField]
    private float temps;

    [Tooltip("Indique à la plateforme la direction qu'elle doit prendre.")]
    [Header("x = 1, y = 2, z = 3, -x = 4, -y = 5, -z = 6")]
    [SerializeField]
    [Range(1, 6)]
    private int direction;

    [Tooltip("Indique à la plateforme quel est sa vitesse.")]
    [Header("Vitesse de déplacement pour la plateforme")]
    [SerializeField]
    private float vitessePlateforme;

    [Tooltip("RigidBody de la plateforme.")]
    [Header("Associer le rigidbody de la plateforme concerné")]
    [SerializeField]
    private Rigidbody plateformeRigidBody;

    private float calculTemps;
    private bool changementDirection = true;

    private void Start()
    {
        calculTemps = temps;
    }

    private void Update()
    {
        if (changementDirection)
        {
            switch (direction)
            {
                case 1:
                    this.transform.Translate(new Vector3(vitessePlateforme, 0, 0) * Time.deltaTime, Space.World);
                    break;
                case 2:
                    this.transform.Translate(new Vector3(0, vitessePlateforme, 0) * Time.deltaTime, Space.World);
                    break;
                case 3:
                    this.transform.Translate(new Vector3(0, 0, vitessePlateforme) * Time.deltaTime, Space.World);
                    break;
                case 4:
                    this.transform.Translate(new Vector3(-vitessePlateforme, 0, 0) * Time.deltaTime, Space.World);
                    break;
                case 5:
                    this.transform.Translate(new Vector3(0, -vitessePlateforme, 0) * Time.deltaTime, Space.World);
                    break;
                case 6:
                    this.transform.Translate(new Vector3(0, 0, -vitessePlateforme) * Time.deltaTime, Space.World);
                    break;
            }
            temps -= Time.deltaTime;
            if (temps <= 0)
            {
                changementDirection = false;
            }
        }
        else
        {
            switch (direction)
            {
                case 1:
                    this.transform.Translate(new Vector3(-vitessePlateforme, 0, 0) * Time.deltaTime, Space.World);
                    break;
                case 2:
                    this.transform.Translate(new Vector3(0, -vitessePlateforme, 0) * Time.deltaTime, Space.World);
                    break;
                case 3:
                    this.transform.Translate(new Vector3(0, 0, -vitessePlateforme) * Time.deltaTime, Space.World);
                    break;
                case 4:
                    this.transform.Translate(new Vector3(vitessePlateforme, 0, 0) * Time.deltaTime, Space.World);
                    break;
                case 5:
                    this.transform.Translate(new Vector3(0, vitessePlateforme, 0) * Time.deltaTime, Space.World);
                    break;
                case 6:
                    this.transform.Translate(new Vector3(0, 0, vitessePlateforme) * Time.deltaTime, Space.World);
                    break;
            }
            temps += Time.deltaTime;
            if (temps >= calculTemps)
            {
                changementDirection = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
