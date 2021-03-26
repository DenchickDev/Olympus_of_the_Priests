using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingRockOnChain : MonoBehaviour
{
    [SerializeField]
    Transform rock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rock.gameObject.GetComponent<Rigidbody2D>().simulated = true;

        }
    }
}
