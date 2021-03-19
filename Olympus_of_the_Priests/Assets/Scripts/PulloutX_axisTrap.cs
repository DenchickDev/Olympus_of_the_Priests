using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulloutX_axisTrap : MonoBehaviour
{
    public float speed = 4f;
    bool isWait = false;
    bool isHidden = false;
    public float waitTime = 4f;
    public Transform point;
    [SerializeField]
    public bool Right = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Right == false)
        {
            point.transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
        }
        else
        {
            point.transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Right == false)
        {

            if (isWait == false)
                transform.position = Vector3.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
            if (transform.position == point.position)
            {
                if (isHidden == true)
                {
                    point.transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
                    isHidden = false;
                }
                else
                {
                    point.transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
                    isHidden = true;
                }
                isWait = true;
                StartCoroutine(Waiting());
            }
        }
        else
        {
            if (isWait == false)
                transform.position = Vector3.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
            if (transform.position == point.position)
            {
                if (isHidden)
                {
                    point.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                    isHidden = false;
                }
                else
                {
                    point.transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                    isHidden = true;
                }
                isWait = true;
                StartCoroutine(Waiting());
            }

        }
    }
    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(waitTime);
        isWait = false;
    }

}
