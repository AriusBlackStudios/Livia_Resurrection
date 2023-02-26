using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    public class AnimatorManager : MonoBehaviour
    {
        [HideInInspector]public Animator anim;
        public bool canRotate;

        [Header("Foot IK ")]
        [Tooltip ("The layers that have Game Objects That The Character's Feet Should Interact With")]
        [SerializeField]    private LayerMask detectForIKLayer;

        [Tooltip("The Enemy Manager or Player Manager Attached To this object")]
        [SerializeField]    private CharacterManager characterManager;


        [Range(0f, 1f)]
        public float distanceToGround;


        public void PlayTargetAnimation(string _targetAnim, bool _isinteracting, bool canRotate = false, bool mirror = false)
        {
            anim.applyRootMotion = _isinteracting;
            anim.SetBool("isInteracting", _isinteracting);
            anim.SetBool("MirrorAnimation", mirror);
            anim.CrossFade(_targetAnim, 0.2f);
            anim.SetBool("canRotate", canRotate);
        }
        public void PlayTargetAnimationWithRootRotation(string targetAnim, bool isinteracting)
        {
            anim.applyRootMotion = isinteracting;
            anim.SetBool("isInteracting", isinteracting);
            anim.CrossFade(targetAnim, 0.2f);
            anim.SetBool("isRotatingWithRootMotion", true);
        }
        public virtual void TakeCriticalDamageAnimationEvent()
        {

        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (characterManager.isInteracting) return;
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot,1f);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot,1f);

            RaycastHit hit;

            //left foot
            Ray leftRay = new Ray(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up,Vector3.down);
            if (Physics.Raycast(leftRay, out hit, distanceToGround + 1, detectForIKLayer))
            {

                    Vector3 footPosition = hit.point;
                    footPosition.y += distanceToGround;
                    anim.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                    anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward,hit.normal));

                
            }


            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

            Ray rightRay = new Ray(anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(rightRay, out hit, distanceToGround + 1, detectForIKLayer))
            {

                Vector3 footPosition = hit.point;
                footPosition.y += distanceToGround;
                anim.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                anim.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));


            }
        }
    }
}
