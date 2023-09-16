using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashWhite : MonoBehaviour
{

    private float _totalTime;
    private float _flashInterval;
    private SpriteRenderer _targetSprRenderer;

    private Material _flashMaterial;
    private Material _originalMaterial;

    public void Init(float totalTime, float flashInterval, SpriteRenderer targetSprRenderer)
    {
        _totalTime = totalTime;
        _flashInterval = flashInterval;
        _targetSprRenderer = targetSprRenderer;

        _originalMaterial = _targetSprRenderer.material;
        _flashMaterial = Globals.GetFlashMaterial();

        StartCoroutine(FlashCoroutine(true));
        StartCoroutine(TotalTimeCoroutine());
	}

    private IEnumerator TotalTimeCoroutine()
    {
        yield return new WaitForSeconds(_totalTime);

		_targetSprRenderer.material = _originalMaterial;
		Destroy(this);
    }
    private IEnumerator FlashCoroutine(bool active)
    {
        _targetSprRenderer.material = active? _flashMaterial: _originalMaterial;
        yield return new WaitForSeconds(_flashInterval);
        StartCoroutine(FlashCoroutine(!active));
    }
}
