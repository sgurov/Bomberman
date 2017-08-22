using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public static class Enums
    {
        public enum Speed
        {
            Normal = 2,
            Fast = 4
        }

        public enum TypeOfVector3
        {
            Direction,
            NextPosition
        }
        
        public enum FieldState
        {
            Empty = 0,
            Filled = 1,
            Player = 2,
            Enemy = 3
        };
    }
}
