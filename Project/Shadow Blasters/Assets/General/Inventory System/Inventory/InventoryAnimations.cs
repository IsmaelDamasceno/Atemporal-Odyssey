using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryAnimations : MonoBehaviour
{
    public static InventoryAnimations s_Instance;

    [SerializeField] private float _time;

    private Transform _inventoryTransfrom;
    private float _invInitialY;

    private Image _backgroundImage;
    private float _backgroundAlpha = .85f;

    private static bool s_inAnimation = false;

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

    public IEnumerator OpenCloseCoroutine(int val)
    {
        if (s_inAnimation)
        {
            yield return null;
        }
        else
        {
			s_inAnimation = true;
			while ((val == 1) ? _inventoryTransfrom.localPosition.y < -5f : _inventoryTransfrom.localPosition.y > _invInitialY + 5f)
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
			finalColor.a = (val == 1) ? _backgroundAlpha : 0f;
			_backgroundImage.color = finalColor;

			s_inAnimation = false;
		}
	}
}
