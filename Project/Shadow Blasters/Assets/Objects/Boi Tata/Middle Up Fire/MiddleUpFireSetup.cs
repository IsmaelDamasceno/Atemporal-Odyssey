using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MiddleUpFireSetup : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private Vector2 fireBallSpawnInterval;
    [SerializeField] private Vector2Int fireBallSpawnAmount;

    [Header("Fire")]
    [SerializeField] private Vector2 fireBallFireInterval;
    
    private Transform fireBallSpawnTrs;
    private int spawnned = 0;
    private int amountToSpawn;
    private int fireItr = 0;
    private bool started = false;

    private List<GameObject> fireBallList = new();

    void Start()
    {
        amountToSpawn = Random.Range(fireBallSpawnAmount.x, fireBallSpawnAmount.y);
    }

    public void InitRef(Transform boiTataTrs)
    {
        fireBallSpawnTrs = boiTataTrs.GetChild(0);
    }

    void Update()
    {
        if (BoiTataController.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && !started)
        {
            started = true;
			StartCoroutine(SpawnCoroutine());
		}
	}

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(fireBallSpawnInterval.x, fireBallSpawnInterval.y));
        spawnned++;

        GameObject instance = Instantiate(fireBallPrefab, fireBallSpawnTrs.position, Quaternion.Euler(0f, 0f, Random.Range(-15f, 15f)));
        fireBallList.Add(instance);

        if (spawnned < amountToSpawn)
        {
            StartCoroutine(SpawnCoroutine());
        }
        else {
            BoiTataController.animator.SetBool("Middle Up Fire", false);
            BoiTataController.readyToAttack = true;
			StartCoroutine(FireCoroutine());
        }
    }
    IEnumerator FireCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(fireBallFireInterval.x, fireBallFireInterval.y));
        GameObject fireBallItr = fireBallList[fireItr];

        Vector2 camSize = Globals.GetCamSize();
        Vector2 camPos = Camera.main.transform.position;
        fireBallItr.transform.position = new Vector2(
            camPos.x + Random.Range(-camSize.x/2f, camSize.x/2f), camPos.y + camSize.y/2f + 2f);
        fireBallItr.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        fireBallItr.GetComponent<FireBall>().falling = true;

		fireItr++;
        if (fireItr < fireBallList.Count)
        {
            StartCoroutine(FireCoroutine());
        }
	}
}
