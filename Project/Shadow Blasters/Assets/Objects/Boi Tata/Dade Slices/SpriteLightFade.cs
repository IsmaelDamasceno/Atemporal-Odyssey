using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLightFade : MonoBehaviour
{

    [SerializeField] private Vector2 totalTime;

    private float totalChoosenTime;
    private float t = 0f;

    private SpriteRenderer sprRenderer;

    void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        totalChoosenTime = Random.Range(totalTime.x, totalTime.y);
    }

    void Update()
    {
        t += Time.deltaTime;
        float p = 1 - (t / totalChoosenTime);
        Color newColor = new Color(p, p, p);
        sprRenderer.color = newColor;

        if (t >= totalChoosenTime)
        {
            Destroy(gameObject);
        }
    }
}
