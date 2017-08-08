using Assets.Scripts.BaseClasses;
using Assets.Scripts.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Exploder
    {
        public static IEnumerator Explode(GameObject explosionSource, float explosionDelay, float explosionDistance,
            Action<RaycastHit[]> callBack)
        {
            yield return new WaitForSeconds(explosionDelay);
            StaticObjectsGeneratorBase staticObjects = ObjectsCreator.GetStaticObjects();
            GameObject explosion = staticObjects.GetExplosion();
            GameObject gameObject = UnityEngine.Object.Instantiate(explosion, explosionSource.transform.position, 
                Quaternion.identity);
            GameObject.Destroy(explosionSource);
            RaycastHit[] rayCastHits = GetHits(explosionSource, explosionDistance);
            callBack(rayCastHits);
            yield return new WaitForSeconds(explosionDelay);
            GameObject.Destroy(gameObject);
        }

        private static RaycastHit[] GetHits(GameObject bomb, float distance)
        {
            List<Vector3> directions = new List<Vector3>();
            List<RaycastHit> result = new List<RaycastHit>();
            directions.Add(Vector3.forward);
            directions.Add(Vector3.back);
            directions.Add(Vector3.left);
            directions.Add(Vector3.right);
            directions.Add(Vector3.zero);

            foreach (var direction in directions)
            {
                RaycastHit[] rayCastHits = Physics.RaycastAll(bomb.transform.position, direction, distance);
                result.AddRange(rayCastHits);
            }

            RaycastHit rayCastHit;

            if (Physics.Raycast(bomb.transform.position + Vector3.forward, Vector3.back, out rayCastHit, 1.0f))
            {
                result.Add(rayCastHit);
            }

            return result.ToArray();
        }
    }
}
