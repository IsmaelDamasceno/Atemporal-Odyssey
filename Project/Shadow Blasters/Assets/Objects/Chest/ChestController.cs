using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour, IInteractable
{
    public bool Open { get; private set; }
    private Animator _animator;

    [SerializeField] private List<GameObject> _itemStorage;

    void Start()
    {
		_animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void SetChest(bool value)
    {
        Open = value;
        _animator.SetBool("Open", Open);
        if (Open && _itemStorage != null)
        {
            foreach(GameObject item in _itemStorage)
            {
                Debug.Log($"Instantiating {item}");
                Instantiate(item, transform.position, Quaternion.Euler(Vector3.zero));
            }
            _itemStorage = null;
		}
    }

    public bool Interact()
    {
        if (!Open)
        {
			SetChest(true);
            return true;
		}
        else
        {
            return false;
        }
    }
}
