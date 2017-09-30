using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer m_line;
    [SerializeField] private int m_maxReflections;
    [SerializeField] private float m_maxDistance;
    [SerializeField] private LayerMask m_layerMask;

    private Player m_player;
    private Ray2D m_ray;
    private int m_numPoints;
    private RaycastHit2D m_hit;
    private Vector2 m_reflectionDirection;
    private bool m_done;

    private void Awake()
    {
        if (m_line == null)
        {
            m_line = this.gameObject.AddComponent<LineRenderer>();
            m_line.startWidth = 0.05f;
            m_line.positionCount = 2;
        }

        m_player = this.GetComponentInParent<Player>();
    }

    private void Update()
    {
        if (!m_player.IsMoving())
        {
            if (Input.GetMouseButton(0))
            {  
                float currentDistance = 0; 
                m_done = false;

                Vector2 mp = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = mp - (Vector2)this.transform.position;

                m_ray = new Ray2D((Vector2)this.transform.position, direction);  

                Debug.DrawRay(m_ray.origin, direction, Color.magenta);  

                m_line.positionCount = 1;
                m_line.SetPosition(0, (Vector2)this.transform.position);

                for (int i = 0; i <= m_maxReflections; i++)
                {   
                    if (!m_done)
                    {
                        m_hit = Physics2D.Raycast(m_ray.origin, m_ray.direction, m_maxDistance - currentDistance, m_layerMask);
                        if (m_hit)
                        {  
                            currentDistance += m_hit.distance;
                            m_reflectionDirection = Vector2.Reflect(m_ray.direction, m_hit.normal);  

                            Debug.DrawRay(m_hit.point, m_hit.normal, Color.green);  
                            Debug.DrawRay(m_hit.point, m_reflectionDirection, Color.white);  
   
                            m_ray = new Ray2D(m_hit.point + (m_hit.normal * 0.005f), m_reflectionDirection);  

                            if (!string.IsNullOrEmpty(m_hit.transform.name))
                            {
                                Debug.Log("Object name: " + m_hit.transform.name);  
                            }
                            m_line.positionCount++;  

                            m_line.SetPosition(i + 1, m_ray.origin);  
                            if (m_hit.transform.CompareTag("Block"))
                            {
                                m_done = true;
                            }
                        }
                    }                   
                } 
                if (currentDistance < m_maxDistance && m_maxReflections + 2 > m_line.positionCount && !m_done)
                {
                    m_line.positionCount++;
                    m_line.SetPosition(m_line.positionCount - 1, m_ray.origin + (m_ray.direction * (m_maxDistance - currentDistance)));
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                m_line.positionCount = 0;
            }
        }
    }
}