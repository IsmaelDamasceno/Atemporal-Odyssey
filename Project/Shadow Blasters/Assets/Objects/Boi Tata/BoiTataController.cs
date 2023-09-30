using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BoiTataState
{
    Cooldown,
    SideTaleAttack,
    MiddleFire,
    MiddleFireBall,
    TopTaleTargetAttack,
    SpawnEnemies,
}

public class BoiTataController : MonoBehaviour
{
    [Header("Attack Setups")]
    [SerializeField] private GameObject sideTaleAttackSetup;
    [SerializeField] private GameObject topTaleAttackSetup;

    private static Vector2 waitTime = new(.25f, 1f);

    private static BoiTataState curState = BoiTataState.Cooldown;
    public static List<BoiTataState> possibleStates = new();
    public static bool readyToAttack = true;

    void Start()
    {
        StartPhase1();
    }

    public static void StartPhase1()
    {
        BoiTataState[] list = {
			BoiTataState.SideTaleAttack,
			//BoiTataState.MiddleFire,
			//BoiTataState.MiddleFireBall,
			BoiTataState.TopTaleTargetAttack,
			//BoiTataState.SpawnEnemies
		};
        possibleStates = new(list);
    }

    void Update()
    {
        if (readyToAttack)
        {
            readyToAttack = false;
            StartCoroutine(StateWait());
        }
    }

    private IEnumerator StateWait()
    {
        yield return new WaitForSeconds(Random.Range(waitTime.x, waitTime.y));

        int nextAttack = Random.Range(0, possibleStates.Count);
        curState = possibleStates[nextAttack];

        // curState = BoiTataState.TopTaleTargetAttack;

        switch(curState)
        {
            case BoiTataState.SideTaleAttack:
                {
                    Vector3 spawnPos = Camera.main.transform.position;
                    spawnPos.z = 0f;

                    Instantiate(sideTaleAttackSetup, spawnPos, Quaternion.identity);
                }break;
			case BoiTataState.TopTaleTargetAttack:
				{
					Vector3 spawnPos = Camera.main.transform.position;
					spawnPos.z = 0f;

					Instantiate(topTaleAttackSetup, spawnPos, Quaternion.identity);
				}
                break;

		}
    }
}
