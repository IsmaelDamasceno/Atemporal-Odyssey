using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla o armazenamento, ações, e animações do baú
/// </summary>
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

    /// <summary>
    /// Abre ou fecha o baú
    /// </summary>
    /// <param name="value">False (fechar), true (abrir)</param>
    public void SetChest(bool value)
    {
        Open = value;
        _animator.SetBool("Open", Open);
        if (Open && _itemStorage != null)
        {
            foreach(GameObject item in _itemStorage)
            {
                Instantiate(item, transform.position, Quaternion.Euler(Vector3.zero));
            }
            _itemStorage = null;
		}
    }

    /// <summary>
    /// Executado ao interagir com o baú
    /// </summary>
    /// <returns>True caso a interação seja bem sucedida, false do contrário</returns>
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
