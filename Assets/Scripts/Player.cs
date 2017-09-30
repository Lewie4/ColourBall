using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private bool m_moving;

    private void Awake()
    {
        m_rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!m_moving)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 vel = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                vel.z = 0;
                vel.Normalize();
                m_rb.velocity = vel;
                m_moving = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Block"))
        {
            col.GetComponent<Block>().BlockHit();
            m_moving = false;
            m_rb.velocity = new Vector2(0, 0);
        }
    }

    public bool IsMoving()
    {
        return m_moving;
    }
}
