using Assets.Scripts.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : CharacterBase
    {
        public override int GetSpeed()
        {
            return speed;
        }

        public override bool CanWallPass()
        {
            return false;
        }
    }
}
