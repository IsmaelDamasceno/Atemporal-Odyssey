using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    void Start()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
		Destroy(GameObject.FindGameObjectWithTag("Canvas"));
		Destroy(GameObject.FindGameObjectWithTag("GameController"));
	}

    void Update()
    {
        
    }
}
