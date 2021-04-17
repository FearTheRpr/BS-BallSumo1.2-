using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRinputModule : BaseInputModule
{
    public Camera m_camera;
    public SteamVR_Input_Sources m_targetSource;
    public SteamVR_Action_Boolean m_clickAction;

    private GameObject m_currentObject = null;
    private PointerEventData m_Data = null;

protected override void Awake()
    {
        base.Awake();

        m_Data = new PointerEventData(eventSystem);
    }
    public override void Process()
    {
        // reset data, set camera
        m_Data.Reset();
        m_Data.position = new Vector2(m_camera.pixelWidth / 2, m_camera.pixelHeight / 2);

        // raycast
        eventSystem.RaycastAll(m_Data, m_RaycastResultCache);
        m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_currentObject = m_Data.pointerCurrentRaycast.gameObject;

        // clear raycast
        m_RaycastResultCache.Clear();

        //hover
        HandlePointerExitAndEnter(m_Data, m_currentObject);

       // Press button/trigger
       if (m_clickAction.GetStateDown(m_targetSource))
            processPress(m_Data);
        
        //Release button/trigger
        if (m_clickAction.GetStateUp(m_targetSource))
            ProcessRelease(m_Data);
        
    }

    public PointerEventData GetData()
    {
        return m_Data;
    }

    private void processPress(PointerEventData data)
    {

    }

    private void ProcessRelease(PointerEventData data)
    {

    }
}
