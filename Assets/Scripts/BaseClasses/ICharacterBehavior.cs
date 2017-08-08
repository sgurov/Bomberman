using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BaseClasses
{
    public interface ICharacterBehavior
    {
        bool CanMove(CharacterBase gameObjectBehavior);
        void Move(CharacterBase gameObjectBehavior);
    }
}
