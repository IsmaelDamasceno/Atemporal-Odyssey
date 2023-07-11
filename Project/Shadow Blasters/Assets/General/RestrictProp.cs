
using System;
using UnityEngine;

public class RestrictProp<T>
{
	public RestrictProp(T value, params Type[] authorized) {
		Value = value;
		_authorizedClasses = authorized;
	}
	public T Value;

	private Type[] _authorizedClasses;
	public bool IsAuthorized(Type classType)
	{
		foreach (Type type in _authorizedClasses)
		{
			if (type == classType)
			{
				return true;
			}
		}
		return false;
	}
	public bool TrySet(T value, Type classType)
	{
		if (IsAuthorized(classType))
		{
			Value = value;
			return true;
		}
		throw new UnityException("Trying to set property from unauthorized class");
	}
}
