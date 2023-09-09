using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla as animações do Inventory System (abrir/fechar)
/// </summary>
public class BuffUIAnimations : MonoBehaviour
{
    public static BuffUIAnimations s_Instance;

    [SerializeField] private float _time;

    private float _invInitialY;

    
    private static bool s_inAnimation = false;

    void Start()
    {
        if (s_Instance == null)
        {
            s_Instance = this;

            _invInitialY = transform.localPosition.y;

			/*
             _backgroundImage = transform.GetChild(0).GetComponent<Image>();
            s_scrollRect = GetComponent<ScrollRect>();
             */

			DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Executa a animação
    /// </summary>
    /// <param name="val">Define a animação: fechar (0), ou abrir (1)</param>
    /// <returns></returns>
    public IEnumerator OpenCloseCoroutine(int val)
    {
        if (s_inAnimation)
        {
            yield return null;
        }
        else
        {
            BUffUIManager.ResetScroll();

			s_inAnimation = true;
			while ((val == 1) ? transform.localPosition.y < -5f : transform.localPosition.y > _invInitialY + 5f)
            {
				float time = 1f - Mathf.Pow(_time, Time.deltaTime);

				#region Inventory Y
				float newY = Mathf.Lerp(transform.localPosition.y, (1 - val) * _invInitialY, time);
				Vector3 newPos = Vector3.up * newY;
				transform.localPosition = newPos;
				#endregion

				yield return null;
			}
			transform.localPosition = (val == 1) ? Vector3.zero : Vector3.up * _invInitialY;

			s_inAnimation = false;
		}
	}
}
