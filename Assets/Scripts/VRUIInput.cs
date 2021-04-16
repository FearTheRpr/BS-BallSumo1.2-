using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
using Valve.VR;

[RequireComponent(typeof(SteamVR_LaserPointer))]
public class VRUIInput : MonoBehaviour
{
    private SteamVR_LaserPointer laserPointer;
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean PointerActivate;


    private void OnEnable()
    {
        laserPointer = GetComponent<SteamVR_LaserPointer>();
        laserPointer.PointerIn -= HandlePointerIn;
        laserPointer.PointerIn += HandlePointerIn;
        laserPointer.PointerOut -= HandlePointerOut;
        laserPointer.PointerOut += HandlePointerOut;
        laserPointer.PointerClick -= HandlePointerClick;
        laserPointer.PointerClick += HandlePointerClick;
       
    }
    private void Update()
    {
       // if the PointerActivate action is enable and the player is not holding an object enable the laser pointer
        if (PointerActivate.GetState(handType))
        {
            laserPointer.holder.SetActive(true);
        }
        else
        {
            laserPointer.holder.SetActive(false);
        }
    }
    // if the laser pointer hits a button, invoke a button press
    private void HandlePointerClick(object sender, PointerEventArgs e)
    {
        var button = e.target.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.Invoke();
        }
    }
    private void HandlePointerIn(object sender, PointerEventArgs e)
    {
        // pass a highlight message to buttons the laser pointer is hovering over
        var button = e.target.GetComponent<Button>();
        if (button != null)
        {
            button.OnSelect(null);
        }
    }
    // disable hover effect of buttons when the laser pointer is not hovering over the button
    private void HandlePointerOut(object sender, PointerEventArgs e)
    {
        var button = e.target.GetComponent<Button>();
        if (button != null)
        {
            button.OnDeselect(null);
        }
    }
}