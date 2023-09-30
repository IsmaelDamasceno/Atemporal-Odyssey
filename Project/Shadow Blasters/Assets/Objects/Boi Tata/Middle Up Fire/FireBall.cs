using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    public bool falling = false;

    void Start()
    {

    }

    void Update()
    {
		transform.Translate(Vector3.up * (Time.deltaTime * (5f + (falling ? 2f : 0f))), Space.Self);
	}
}
