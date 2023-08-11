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
	/// Retona um Component desse Member
	/// </summary>
	/// <param name="name">Nome do Component para rotornar</param>
	/// <returns>Referência ao Member</returns>
	public MonoBehaviour GetMember(string name)
	{
		return _children[name];
	}

	/// <summary>
	/// Regista um component nesse Member
	/// </summary>
	/// <param name="name">Nome do Component</param>
	/// <param name="member">Referência ao objeto do Component a ser registrado nesse Member</param>
	public void RegisterMember(string name, MonoBehaviour member)
	{
		_children[name] = member;
	}

	/// <summary>
	/// Lista de Components desse objeto
	/// </summary>
	private Dictionary<string, MonoBehaviour> _children = new();
}
