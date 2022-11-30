using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTwoSeconds : MonoBehaviour
{
    private void Start()
    {
        Invoke("DestroyObject", 2f);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
