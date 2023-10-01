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

    public static BUffUIManager s_Instance;
    public static bool s_Open = false;
	private static ScrollRect s_scrollRect;

	private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
            s_scrollRect = GetComponent<ScrollRect>();
            s_Open = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void ResetScroll()
    {
		s_scrollRect.verticalNormalizedPosition = 1f;
	}

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

    public static void CreateBuff(GameObject buffPrefab)
    {
        Instantiate(buffPrefab, s_Instance.transform.GetChild(0));
        ResetScroll();
    }
}
