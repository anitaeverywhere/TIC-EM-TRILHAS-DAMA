using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject skeleton;
    [SerializeField] float interval;
    [SerializeField] float timer;


    void Update()
    {
        timer += Time.deltaTime;
        if (interval < timer)
        {
            Vector3 position = new Vector3(transform.position.x, 0, transform.position.z);
            Instantiate(skeleton, position, Quaternion.LookRotation(Vector3.zero));
            timer = 0f;
        }
    }
}
