using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controla o Health System (adiciona/subtrai vida, seta a vida m�xima, etc)
/// </summary>
public class HealthSystem : MonoBehaviour
{

    private static int s_health;
    private static int s_healthMax;
    private static GridLayoutGroup s_layoutGroup;

    public static HealthSystem s_Instance;

    [SerializeField] private GameObject _heartPrefab;

    /// <summary>
    /// Quantidade m�xima de cora��es por linha
    /// </summary>
    /// <param name="newValue">Nova quantidade m�xima</param>
    public static void SetHartPerRow(int newValue)
    {
        s_layoutGroup.constraintCount = newValue;
    }

    private void Awake()
    {
        if (s_Instance == null)
        {
			s_Instance = this;
            s_layoutGroup = GetComponent<GridLayoutGroup>();
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
		SetMaxHealth(transform.childCount);
		SetHealth(transform.childCount);
	}

	/// <summary>
	/// Set a quantidade de vida
	/// </summary>
	/// <param name="newAmount">Nova quantidade de vida</param>
	public static void SetHealth(int newAmount)
    {
		s_health = newAmount;
		HealthUpdate();
	}

    /// <summary>
    /// Aumenta a quantidade de vida por um valor
    /// </summary>
    /// <param name="increase">Valor para aumentar/diminuir da vida atual</param>
	public static void ChangeHealth(int increase)
    {
		s_health += increase;
		HealthUpdate();
	}

    /// <summary>
    /// Atualiza os Hearts ao mudar o valor da vida
    /// </summary>
	private static void HealthUpdate()
    {
        if (s_health <= 0)
        {
			s_Instance.StartCoroutine(s_Instance.RestartCoroutine());
		}

        for(int i = 0; i < s_healthMax; i++)
        {
            HealthController controller = s_Instance.transform.GetChild(i).GetComponent<HealthController>();
            controller.ChangeHeart(i < s_health);
        }
    }
    IEnumerator RestartCoroutine()
    {
		TransitionController.s_Animator.SetTrigger("Start");

		yield return new WaitForSeconds(TransitionController.s_TransitionTime);

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		SetMaxHealth(transform.childCount);
		SetHealth(transform.childCount);
		Player.PropertiesCore.Player.transform.position = GameController.savePos;
	}

    /// <summary>
    /// Setar vida m�xima
    /// </summary>
    /// <param name="value">Quantidade de vida m�xima</param>
    public static void SetMaxHealth(int value)
    {
        int oldMaxHealth = s_healthMax;
        s_healthMax = value;

        if (s_healthMax == s_Instance.transform.childCount)
        {
            return;
        }

        if (oldMaxHealth < value)
        {
			for (int i = oldMaxHealth; i < value; i++)
            {
                Instantiate(s_Instance._heartPrefab, s_Instance.transform);
            }
			SetHealth(value);
		}
		else if (oldMaxHealth > value)
        {
			for (int i = value; i < s_Instance.transform.childCount; i++)
			{
                Destroy(s_Instance.transform.GetChild(i).gameObject);
			}
			SetHealth(value);
		}
    }

    private void Update()
    {
    }
}
