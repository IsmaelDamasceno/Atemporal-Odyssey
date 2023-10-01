using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GroundFire : MonoBehaviour
{

    [SerializeField] private Vector2 fireDuration;

    private SpriteRenderer sprRenderer;
    private Light2D light;
    private float maxLightIntensity;


    private float choosenDuration;
    private float currentTime;

    void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        choosenDuration = Random.Range(fireDuration.x, fireDuration.y);

		light = GetComponentInChildren<Light2D>();
        maxLightIntensity = light.intensity;
	}

    void Update()
    {
        if (currentTime < choosenDuration)
        {
            currentTime += Time.deltaTime;

            float sprP = 1f - (currentTime / choosenDuration);
            float lgtP = sprP * maxLightIntensity;

            Color newColor = sprRenderer.color;
            newColor.a = sprP;
            sprRenderer.color = newColor;

            light.intensity = lgtP;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
