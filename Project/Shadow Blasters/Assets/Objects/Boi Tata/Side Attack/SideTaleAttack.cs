using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTaleAttack : MonoBehaviour
{
    [Header("Fade In")]
    [SerializeField] private AnimationCurve fadeInCurve;
    [SerializeField] private float fadeInTimeScale;
    [SerializeField] private float fadeInScale;

    [Header("Attack")]
    [SerializeField] private AnimationCurve attackCurve;
    [SerializeField] private float attackTimeScale;
    [SerializeField] private float attackScale;

    private float time = 0f;
    private bool attacking = false;

    private Transform taleTrs;

    void Start()
    {
        taleTrs = transform.GetChild(0);
    }

    void Update()
    {
        if (!attacking)
        {
            FadeIn();
        }
        else
        {
            Attack();
        }
    }

    private void FadeIn()
    {
        time += Time.deltaTime * fadeInTimeScale;
        if (time >= 1f)
        {
            attacking = true;
            time = 0f;
            return;
        }

        float val = fadeInCurve.Evaluate(time) * fadeInScale;
		taleTrs.localPosition = new Vector3(val, 0f, 0f);
    }
    private void Attack()
    {
		time += Time.deltaTime * attackTimeScale;
		if (time >= 1f)
		{
            Debug.Log("Setting Ready To Attack");
            BoiTataController.readyToAttack = true;
            Destroy(gameObject);

			/*
             time = 0f;
            attacking = false;
            taleTrs.localPosition = Vector3.zero;
             */
			return;
		}

		float val = attackCurve.Evaluate(time) * attackScale;
		taleTrs.localPosition = new Vector3(val, 0f, 0f);
	}
}
