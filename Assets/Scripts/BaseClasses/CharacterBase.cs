using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.BaseClasses
{
    public abstract class CharacterBase : NetworkBehaviour
    {
        protected int speed = (int)Enums.Speed.Normal;
        protected NetworkAnimator networkAnimator;
        public bool dead = false;
        public AudioClip stepSound;
        protected AudioSource audioSource;

        public abstract int GetSpeed();
        public abstract bool CanWallPass();
        public abstract int GetBombs();

        protected virtual void Start()
        {
            networkAnimator = GetComponent<NetworkAnimator>();
            audioSource = GetComponentInChildren<AudioSource>();
        }

        protected virtual void PlayStepSound()
        {
            audioSource.PlayOneShot(stepSound);
        }
    }
}