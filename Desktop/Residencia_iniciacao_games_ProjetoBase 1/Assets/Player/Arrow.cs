using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Vector3 initial;
    public Vector3 toGo;

    void Start()
    {
        speed = 0.3f;
        initial = transform.position;
        Destroy(gameObject, 1.5f);
    }

    void Update()
    {
        transform.position += (toGo - initial).normalized * speed;
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.layer == 6)
        {
            Destroy(coll.gameObject);
            Destroy(gameObject);
        }
    }
}
