using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryAnimations : MonoBehaviour
{

    [SerializeField] private float _time;

    private Transform _inventoryTransfrom;
    private float _invInitialY;

    private Image _backgroundImage;
    private float _backgroundAlpha = .85f;

    public static InventoryAnimations s_Instance;

    void Start()
    {
        if (s_Instance == null)
        {
            s_Instance = this;

            _inventoryTransfrom = transform.GetChild(2);
            _invInitialY = _inventoryTransfrom.localPosition.y;

            _backgroundImage = transform.GetChild(0).GetComponent<Image>();
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        
    }

    public static void OpenInventory()
    {
        s_Instance.StartCoroutine(s_Instance.OpenCloseCoroutine(1));
    }
    public static void CloseInventory()
    {
		s_Instance.StartCoroutine(s_Instance.OpenCloseCoroutine(0));
	}

    private IEnumerator OpenCloseCoroutine(int val)
    {
        while ((val == 1) ? _inventoryTransfrom.localPosition.y < -0.15f : _inventoryTransfrom.localPosition.y > _invInitialY + 0.15f)
        {
            float time = 1f - Mathf.Pow(_time, Time.deltaTime);

			#region Inventory Y
			float newY = Mathf.Lerp(_inventoryTransfrom.localPosition.y, (1 - val) * _invInitialY, time);
			Vector3 newPos = Vector3.up * newY;
			_inventoryTransfrom.localPosition = newPos;
            #endregion

            #region Background Opacity
            float newAlpha = Mathf.Lerp(_backgroundImage.color.a, val * _backgroundAlpha, time);
            Color newColor = _backgroundImage.color;
            newColor.a = newAlpha;
            _backgroundImage.color = newColor;
            #endregion

            yield return null;
        }
		_inventoryTransfrom.localPosition = (val == 1) ? Vector3.zero : Vector3.up * _invInitialY;

        Color finalColor = _backgroundImage.color;
		finalColor.a = (val == 1) ? _backgroundAlpha : 0f ;
		_backgroundImage.color = finalColor;
	}
}
