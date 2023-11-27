using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour, IInteractable
{
    [SerializeField] private Elevator elevator;
    [SerializeField] private int floorIndex;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public bool Interact()
    {
        if (elevator.direction == 0)
        {
			elevator.GoToFloor(floorIndex);
			animator.SetTrigger("Activate");
            return true;
		}
        return false;
    }
}
