using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla as animações do Inventory System (abrir/fechar)
/// </summary>
public class BUffUIManager : MonoBehaviour
{

    public static bool s_Open = false;

    public static void SetBuffUI(bool value)
    {
        s_Open = value;
        BuffUIAnimations.s_Instance.StartCoroutine(BuffUIAnimations.s_Instance.OpenCloseCoroutine(s_Open? 1: 0));
    }
    public static void SetBuffUI()
    {
		s_Open = !s_Open;
		BuffUIAnimations.s_Instance.StartCoroutine(BuffUIAnimations.s_Instance.OpenCloseCoroutine(s_Open ? 1 : 0));
	}
}
