using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyTest : MonoBehaviour
{
    public Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_Rigidbody.detectCollisions = false;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            m_Rigidbody.detectCollisions = true;
        }
    }
}
