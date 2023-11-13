using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGA : MonoBehaviour
{
    public float DestroyTime;
    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }

}
