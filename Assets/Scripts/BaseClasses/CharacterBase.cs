using UnityEngine;

namespace Assets.Scripts.BaseClasses
{
    public abstract class CharacterBase : MonoBehaviour
    {
        protected int speed = (int)Speed.Normal;

        public abstract int GetSpeed();
        public abstract bool CanWallPass();

        protected enum Speed
        {
            Normal = 2,
            Fast = 4
        }
    }
}