using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Classe base para todos os Members do Entity System
/// </summary>
public class BaseMember : MonoBehaviour
{
	/// <summary>
	/// M�todo que procura pela Entity associada ao chamador do m�todo
	/// </summary>
	/// <returns>Refer�ncia ao Entity GameObject</returns>
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

	/// <summary>
	/// Retorna um Member na hierarquia do dono do m�todo
	/// </summary>
	/// <param name="name">Nome do Member da hierarquia</param>
	/// <returns>Refer�ncia ao Member</returns>
	public BaseMember GetChild(string name)
	{
		return _children[name];
	}

	/// <summary>
	/// Registra um Member na Hierarquia do dono do m�todo
	/// </summary>
	/// <param name="name">Nome do Member</param>
	/// <param name="member">Refer�ncia ao objeto do Member a ser registrado na hierarquia</param>
	public void RegisterChild(string name, BaseMember member)
	{
		_children[name] = member;
	}

	/// <summary>
	/// Lista de Members na hierarquia desse objeto
	/// </summary>
	private Dictionary<string, BaseMember> _children = new Dictionary<string, BaseMember>();
}
