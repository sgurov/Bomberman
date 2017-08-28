using Assets.Scripts.BaseClasses;
using Assets.Scripts.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public static class Exploder
    {
        public static IEnumerator Explode(PlayerBase playerBehavior, GameObject explosionSource, 
            float explosionDelay, float explosionDistance, Action<RaycastHit[]> callBack)
        {
            yield return new WaitForSeconds(explosionDelay);
            DynamicObjectsGeneratorBase dynamicObjects = ObjectsCreator.GetDynamicObjects();
            GameObject explosion = dynamicObjects.GetExplosion();
            GameObject gameObject = UnityEngine.Object.Instantiate(explosion, explosionSource.transform.position, 
                Quaternion.identity);

            if (playerBehavior.isServer)
            {
                NetworkServer.Spawn(gameObject);
            }

            GameObject.Destroy(explosionSource);
            RaycastHit[] rayCastHits = GetHits(explosionSource.transform.position, explosionDistance);
            callBack(rayCastHits);
            yield return new WaitForSeconds(explosionDelay);
            GameObject.Destroy(gameObject);
        }

        public static RaycastHit[] GetHits(Vector3 position, float distance)
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
                RaycastHit[] rayCastHits = Physics.RaycastAll(position, direction, distance);
                result.AddRange(rayCastHits);
            }

            RaycastHit rayCastHit;

            if (Physics.Raycast(position + Vector3.forward, Vector3.back, out rayCastHit, 1.0f))
            {
                result.Add(rayCastHit);
            }

            return result.ToArray();
        }
    }
}
