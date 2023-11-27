using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TransitionController : MonoBehaviour
{

    public static TransitionController s_Instance;
    public static Animator s_Animator;

    public static float s_TransitionTime = 2f;

    void Awake()
    {
		s_Animator = GetComponent<Animator>();
		if (s_Instance == null)
        {
			s_Instance = this;
		}
    }
}
