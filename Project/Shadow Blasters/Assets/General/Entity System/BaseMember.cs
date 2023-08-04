using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseMember : MonoBehaviour
{
	public GameObject GetRoot()
	{
		if (transform.parent != null)
		{
			if (transform.parent.TryGetComponent(out BaseMember baseMember))
			{
				return baseMember.GetRoot();
			}
		}
		return gameObject;
	}
	public BaseMember GetChild(string name)
	{
		return _children[name];
	}
	public void RegisterChild(string name, BaseMember member)
	{
		_children[name] = member;
	}

	private Dictionary<string, BaseMember> _children = new Dictionary<string, BaseMember>();


}
