using UnityEngine;

namespace Asteroids2.Utilities
{
    public class EnsureOnlyOneGO : MonoBehaviour
    {
        private void Awake()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag(gameObject.tag);

            if (objs.Length > 1)
            {
                Destroy(gameObject);
            }
        }
    }
}