using UnityEngine;

namespace Livia
{
    public class FootStepPlayer : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip clip;
        public Animator animator;

        [Tooltip("The minimum time between playing footstep sounds.")]
        public float minTimeBetweenFootsteps = 0.33f;
        [Tooltip("The height at which a foot is marked as no longer being on the ground (so it will trigger a sound later).")]
        public float heightToMarkOffGround = 0.02f;
        [Tooltip("The height at which a foot is marked as being on the ground (so it will trigger a sound if was marked as off the ground).")]
        public float heightToMarkOnGround = 0.02f;
        [Tooltip("The velocity at which a foot moving triggers the maximum volume (1), lower values mean louder sounds. (i.e. a slower foot is louder the lower the value)")]
        public float velocityForMaxVolume = 0.2f;

        float timeSinceLastPlayed = 0;

        float originalLeftFootHeight;
        float originalRightFootHeight;
        float previousLeftFootHeight;
        float previousRightFootHeight;
        bool leftFootOffGround = false;
        bool rightFootOffGround = false;

        void OnEnable()
        {
            originalLeftFootHeight = animator.GetBoneTransform(HumanBodyBones.LeftFoot).position.y - transform.position.y;
            originalRightFootHeight = animator.GetBoneTransform(HumanBodyBones.RightFoot).position.y - transform.position.y;
        }

        void LateUpdate()
        {
            audioSource.pitch = Random.Range(0.8f, 1.4f);

            var currentLFHeight = animator.GetBoneTransform(HumanBodyBones.LeftFoot).position.y - transform.position.y;
            var LFVel = currentLFHeight - previousLeftFootHeight;
            previousLeftFootHeight = currentLFHeight;


            if (leftFootOffGround == false && currentLFHeight > originalLeftFootHeight + heightToMarkOffGround)
            {
                leftFootOffGround = true;
            }

            if (LFVel < 0 && currentLFHeight <= originalLeftFootHeight + heightToMarkOnGround && leftFootOffGround && timeSinceLastPlayed > minTimeBetweenFootsteps)
            {
                audioSource.volume = Mathf.InverseLerp(0, velocityForMaxVolume, Mathf.Abs(LFVel));

                audioSource.PlayOneShot(clip);
                timeSinceLastPlayed = 0;
                leftFootOffGround = false;
            }

            var currentRFHeight = animator.GetBoneTransform(HumanBodyBones.RightFoot).position.y - transform.position.y;
            var RFVel = currentRFHeight - previousRightFootHeight;
            previousRightFootHeight = currentRFHeight;

            if (rightFootOffGround == false && currentRFHeight > originalRightFootHeight + heightToMarkOffGround)
            {
                rightFootOffGround = true;
            }

            if (RFVel < 0 && currentRFHeight <= originalRightFootHeight + heightToMarkOnGround && rightFootOffGround && timeSinceLastPlayed > minTimeBetweenFootsteps)
            {
                audioSource.volume = Mathf.InverseLerp(0, velocityForMaxVolume, Mathf.Abs(RFVel));

                audioSource.PlayOneShot(clip);
                timeSinceLastPlayed = 0;
                rightFootOffGround = false;
            }

            timeSinceLastPlayed += Time.deltaTime;
        }
    }
}