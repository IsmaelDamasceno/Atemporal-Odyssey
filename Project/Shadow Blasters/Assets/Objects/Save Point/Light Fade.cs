using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFade : MonoBehaviour
{

    [SerializeField] private AnimationCurve fadeCurve;
    [SerializeField] private float totalTime;

    private float t = 0f;
    private Light2D light;
    private float totalIntensity;

    void Start()
    {
        light = GetComponent<Light2D>();
        totalIntensity = light.intensity;
    }

    private void OnEnable()
    {
        t = 0f;
        light.intensity = totalIntensity;
    }

    void Update()
    {
        t += Time.deltaTime;
        float p = fadeCurve.Evaluate(t / totalTime) * totalIntensity;
        light.intensity = p;

        if (t / totalTime >= 1f)
        {
            gameObject.SetActive(false);
        }
    }
}
