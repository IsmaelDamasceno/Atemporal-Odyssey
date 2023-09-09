using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsistentCanvas : MonoBehaviour
{

    private static ConsistentCanvas s_instance;

    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
