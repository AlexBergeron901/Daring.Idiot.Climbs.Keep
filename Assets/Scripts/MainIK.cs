using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainIK : MonoBehaviour
{
    protected Animator animator;

    [SerializeField] private bool ikActive = false;
    [SerializeField] private Transform objetMainGauche = null;
    [SerializeField] private Transform objetMainDroite = null;
    [SerializeField] private Transform regarderdirection = null;

    [Range(0, 1)] [SerializeField] private float positionMainDroite;
    [Range(0,1)] [SerializeField] private float rotationMainDroite;

    [SerializeField] private Transform coudeDroit;

    [Range(0, 1)] [SerializeField] private float positionMainGauche;
    [Range(0, 1)] [SerializeField] private float rotationMainGauche;

    [SerializeField] private Transform coudeGauche;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal.
            if (ikActive)
            {
                // Set the look target position, if one has been assigned
                if (regarderdirection != null)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(regarderdirection.position);
                }
                if (coudeDroit != null)
                {
                    animator.SetIKHintPosition(AvatarIKHint.RightElbow, coudeDroit.transform.position);
                    animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1);
                }
                if (coudeGauche != null)
                {
                    animator.SetIKHintPosition(AvatarIKHint.LeftElbow, coudeGauche.transform.position);
                    animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1);
                }
                // Set the right hand target position and rotation, if one has been assigned
                if (objetMainDroite != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, positionMainDroite);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rotationMainDroite);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, objetMainDroite.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, objetMainDroite.rotation);
                }
                if (objetMainGauche != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, positionMainGauche);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, rotationMainGauche);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, objetMainGauche.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, objetMainGauche.rotation);
                    
                }
            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKHintPosition(AvatarIKHint.RightElbow, new Vector3(0, 0, 0));
                animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 0);
                animator.SetIKHintPosition(AvatarIKHint.LeftElbow, new Vector3(0, 0, 0));
                animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 0);

                animator.SetLookAtWeight(0);
            }
        }
    }
}
