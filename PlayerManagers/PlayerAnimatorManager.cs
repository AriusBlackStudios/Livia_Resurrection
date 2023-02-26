using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{
    public class PlayerAnimatorManager : AnimatorManager
    {
        PlayerManager player;


        int m_vertical;
        int m_horizontal;



        public void Initialize()
        {
            anim = GetComponent<Animator>();
            player = GetComponent<PlayerManager>();
            m_vertical = Animator.StringToHash("Vertical");
            m_horizontal = Animator.StringToHash("Horizontal");

        }

        public void UpdateAnimatorValues(float _verticalMovement, float _horizontalMovement,bool isSprinting)
        {

            #region Vertical
            //hard claming the vertical movement to mkae things a lil more snappy 
            float _v = 0;
            if (_verticalMovement >0 && _verticalMovement < 0.55f)
            {
                _v = 0.55f;
            }
            else if(_verticalMovement > 0.55f)
            {
                _v = 1f;
            }else if (_verticalMovement < 0 && _verticalMovement >-0.55f)
            {
                _v = -0.55f;
            }
            else if (_verticalMovement < -0.55f)
            {
                _v = -1f;
            }
            else
            {
                _v = 0;
            }
            #endregion
            #region Horizontal
            float _h = 0;
            if (_horizontalMovement > 0 && _horizontalMovement < 0.55f)
            {
                _h = 0.55f;
            }
            else if (_horizontalMovement > 0.55f)
            {
                _h = 1f;
            }
            else if (_horizontalMovement < 0 && _horizontalMovement > -0.55f)
            {
                _h = -0.55f;
            }
            else if (_horizontalMovement < -0.55f)
            {
                _h = -1f;
            }
            else
            {
                _h = 0;
            }
            #endregion
            if (isSprinting)
            {
                _v= 2;
                _h = _horizontalMovement;

            }
            anim.SetFloat(m_vertical, _v, 0.1f, Time.deltaTime);
            anim.SetFloat(m_horizontal, _h, 0.1f, Time.deltaTime);
        }


        public void CanRotate()
        {

            anim.SetBool("canRotate", true);
        }
        public void StopRotation()
        {

            anim.SetBool("canRotate", false);
        }

        public void EnableCombo()
        {
            anim.SetBool("canDoCombo", true);
        }

        public void DisableCombo()
        {
            anim.SetBool("canDoCombo", false);
        }
        public void EnableIsInvulnerable()
        {
            anim.SetBool("isInvulnerable", true);
        }
        public void DisableIsInvulnerable()
        {
            anim.SetBool("isInvulnerable", false);
        }
        public void EnableIsParrying()
        {
            player.isParrying = true;
        }

        public void DisableIsParrying()
        {
            player.isParrying = false;
        }

        public void EnableCanBeReposted()
        {
            player.canBeRiposted = true;
        }

        public void DisableCanBeReposted()
        {
            player.canBeRiposted = false;
        }

        public void EnableCanBeParried()
        {
            player.canBeParried = true;
        }

        public void DisableCanBeParried()
        {
            player.canBeParried = false;
        }

        public void EnableCollision()
        {
            player.locomotion.characterCollider.enabled = true;
            player.locomotion.CharacterCollisionCollider.enabled = true;


        }
        public void DisableCollision()
        {
            player.locomotion.characterCollider.enabled = false;
            player.locomotion.CharacterCollisionCollider.enabled = false;
        }

        public override void TakeCriticalDamageAnimationEvent()
        {
            player.stats.TakeDamageNoAnimation(player.pendingCriticalDamage);
            player.pendingCriticalDamage = 0;
        }
        private void OnAnimatorMove()
        {
            if(player.isInteracting == false)
            {
                return;
            }

            float _delta = Time.deltaTime;
            player.locomotion.rigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 Velocity = deltaPosition / _delta;
            player.locomotion.rigidbody.velocity = Velocity;

        }
    }
}
