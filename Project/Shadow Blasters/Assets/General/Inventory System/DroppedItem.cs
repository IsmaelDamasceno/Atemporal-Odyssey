using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe assimilada a todos os objetos largados no chão, os quais são pegáveis
/// </summary>
public class DroppedItem : MonoBehaviour, IInteractable
{
	[SerializeField] private Item _item;
	private bool _pickable = false;
	private SpriteRenderer _image;

	private Rigidbody2D _rb;

	public void InitializeItem(Item newItem)
	{
		_item = newItem;
		_image.sprite = _item.Sprite;
	}

	private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6, 7);
		Physics2D.IgnoreLayerCollision(6, 6);

		_image = transform.GetComponent<SpriteRenderer>();
		if (_item != null)
		{
			InitializeItem(_item);
		}
	}

    void Start()
    {
		float angle = 90f + Random.Range(-20f, 20f);
        float strenght = Random.Range(6f, 10f);
        Vector2 force = new(strenght * Mathf.Cos(angle * Mathf.Deg2Rad), strenght * Mathf.Sin(angle * Mathf.Deg2Rad));
        _rb.velocity = force;
	}

    void Update()
    {
        if (_rb.velocity.sqrMagnitude <= 0.2236f && !_rb.isKinematic)
        {
            StartCoroutine(StopPhysics());
        }
    }

	/// <summary>
	/// Timer para desativar a física do Item
	/// </summary>
	/// <returns></returns>
    private IEnumerator StopPhysics()
    {
        yield return new WaitForSeconds(Random.Range(0.35f, 1.3f));
        _rb.velocity = Vector2.zero;
        _rb.isKinematic = true;
        _pickable = true;
    }

	/// <summary>
	/// Executa ao pegar esse Item
	/// </summary>
	/// <returns></returns>
	public bool Interact()
	{
		if (_pickable)
		{
			InventoryManager.CreateInvItem(_item);
			Destroy(gameObject);
			return true;
		}
		return false;
	}
}
