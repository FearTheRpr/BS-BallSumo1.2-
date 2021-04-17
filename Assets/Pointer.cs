using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public float m_DefaultLength = 5.0f;
    public GameObject Dot;
    public GameObject m_inputModule;

    private LineRenderer m_lineRenderer = null;

    private void Awake()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        // use default or distance
        float targetLength = m_DefaultLength;

        //raycast
        RaycastHit hit = CreateRaycast(targetLength);

        //default
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        //based on hit
        if (hit.collider != null)
                {
                   endPosition = hit.point;
                }

        //set position of dot
        Dot.transform.position = endPosition;

        //set position of line renderer
        m_lineRenderer.SetPosition(0, transform.position);
        m_lineRenderer.SetPosition(1, endPosition);

    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);
        return hit;
    }
}
