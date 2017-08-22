using UnityEngine;

namespace Assets.Scripts.BaseClasses
{
    public abstract class CharacterBase : MonoBehaviour
    {
        protected int speed = (int)Enums.Speed.Normal;
        protected Animator animator;
        protected bool dead = false;
        public AudioClip stepSound;
        protected AudioSource audioSource;

        public abstract int GetSpeed();
        public abstract bool CanWallPass();
        public abstract int GetBombs();

        protected virtual void Start()
        {
            animator = GetComponent<Animator>();
            audioSource = GetComponentInChildren<AudioSource>();
        }

        protected virtual void PlayStepSound()
        {
            audioSource.PlayOneShot(stepSound);
        }
    }
}