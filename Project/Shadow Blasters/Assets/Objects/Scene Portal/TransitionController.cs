using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TransitionController : MonoBehaviour
{

    public static TransitionController s_Instance;
    public static Animator s_Animator;

    [SerializeField] private float _transitiontime;

    public static float s_TransitionTime { get => s_Instance._transitiontime; private set => s_Instance._transitiontime = value; }

    void Awake()
    {
        if (s_Instance == null)
        {
			s_Instance = this;
			s_Animator = GetComponent<Animator>();
		}
        else
        {
            Destroy(gameObject);
        }
    }
}
