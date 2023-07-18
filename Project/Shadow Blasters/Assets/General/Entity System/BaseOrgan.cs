using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseOrgan : MonoBehaviour
{
	public GameObject GetRoot()
	{
		if (transform.parent != null)
		{
			if (transform.parent.TryGetComponent(out BaseOrgan baseOrgan))
			{
				return baseOrgan.GetRoot();
			}
		}
		return gameObject;
	}
	public BaseOrgan GetChild(string name)
	{
		return _children[name];
	}
	public void RegisterChild(string name, BaseOrgan organ)
	{
		_children[name] = organ;
	}

	private Dictionary<string, BaseOrgan> _children = new Dictionary<string, BaseOrgan>();


}
