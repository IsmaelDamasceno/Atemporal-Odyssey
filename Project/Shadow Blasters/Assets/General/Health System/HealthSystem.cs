using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Controla o Health System (adiciona/subtrai vida, seta a vida máxima, etc)
/// </summary>
public class HealthSystem : MonoBehaviour
{

    private static int s_health;
	private static int s_healthMax;

    private static List<GameObject> s_heartList;

    public static HealthSystem s_Entity;

    [SerializeField] private GameObject _heartPrefab;
    [SerializeField] private float _spacing;

    private void Awake()
    {
        if (s_Entity == null)
        {
            s_Entity = this;
			s_heartList = new List<GameObject>();
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
		SetMaxHealth(10);
    }

    void Update()
    {
        
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
        for(int i = 0; i < s_healthMax; i++)
        {
            HealthController controller = s_heartList[i].GetComponent<HealthController>();
            controller.ChangeHeart(i < s_health);
        }
    }

    /// <summary>
    /// Setar vida máxima
    /// </summary>
    /// <param name="value">Quantidade de vida máxima</param>
    public static void SetMaxHealth(int value)
    {
        s_healthMax = value;
        if (s_heartList.Count < value)
        {
			for (int i = s_heartList.Count; i < value; i++)
            {
                GameObject instance = Instantiate(s_Entity._heartPrefab, s_Entity.transform);
                instance.transform.localPosition = new Vector2(i * s_Entity._spacing, 0f);
				s_heartList.Add(instance);
            }
			ChangeHealth(value);
		}
		else if (s_heartList.Count > value)
        {
            for(int i = s_heartList.Count-1; i >= value; i--)
            {
                Destroy(s_heartList[i]);
                s_heartList.RemoveAt(i);
            }
        }
    }
}
