using UnityEngine;
using Cinemachine;
[RequireComponent(typeof(CinemachineFreeLook))]

public class CameraController : MonoBehaviour{
    public Joystick joystick;
    private bool _freeLookActive;
    public float jHorizontal,jVertical,hSensivity,vSensivity;
    private void Start() {
        CinemachineCore.GetInputAxis = GetInputAxis;
    }
    private void Update(){
        jHorizontal = joystick.Horizontal;
        jVertical = joystick.Vertical;
        if (jHorizontal != 0 || jVertical != 0) {
            _freeLookActive = true;
            this.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = null;
            this.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = null;
            
            if(jHorizontal < 0)
                this.GetComponent<CinemachineFreeLook>().m_XAxis.Value -= (hSensivity * jHorizontal)*-1;
            if (jHorizontal >0)
                this.GetComponent<CinemachineFreeLook>().m_XAxis.Value += (hSensivity * jHorizontal);

            if (jVertical < 0)
                this.GetComponent<CinemachineFreeLook>().m_YAxis.Value -= (vSensivity * jVertical)*-1;
            if (jVertical > 0)
                this.GetComponent<CinemachineFreeLook>().m_YAxis.Value += (vSensivity * jVertical);
            _freeLookActive = false;
        }
            
        //_freeLookActive = Input.GetMouseButton(1); // 0 = left mouse btn or 1 = right

    }
    private float GetInputAxis(string axisName) {
        //return !_freeLookActive ? 0 : Input.GetAxis(axisName == "Mouse Y" ? "Mouse Y" : "Mouse X");
        if(!_freeLookActive)
        {
            return 0;
        }
        else
        {
            if(axisName == "Mouse Y")
            {
               return Input.GetAxis("Mouse Y");
            }
            else
            {
                return Input.GetAxis("Mouse X");
            }
        }
        
    }



}
