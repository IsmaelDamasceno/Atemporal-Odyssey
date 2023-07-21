using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{

    private AnimatorStateInfo _animStateInfo;
    public bool Open;

    void Start()
    {
		_animStateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
    }

    void Update()
    {
        
    }
}
