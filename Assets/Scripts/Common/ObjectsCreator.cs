using Assets.Scripts.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Common
{
    public static class ObjectsCreator
    {
        public static StaticObjectsGeneratorBase GetStaticObjects()
        {
            return new StaticObjectsGenerator();
        }

        public static DynamicObjectsGeneratorBase GetDynamicObjects()
        {
            return new AdvancedDynamicObjectsGenerator();
        }
    }
}
