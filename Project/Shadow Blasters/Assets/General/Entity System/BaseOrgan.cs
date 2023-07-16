using System.Collections;
using System.Collections.Generic;
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
}
