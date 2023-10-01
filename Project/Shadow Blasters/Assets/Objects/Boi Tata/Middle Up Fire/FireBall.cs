using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    [SerializeField] private GameObject groundFIre;
    public bool falling = false;

    void Start()
    {

    }

    void Update()
    {
		transform.Translate(Vector3.up * (Time.deltaTime * (5f + (falling ? 2f : 0f))), Space.Self);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{collision.name}: {falling}");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && falling)
        {
            Instantiate(groundFIre, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
