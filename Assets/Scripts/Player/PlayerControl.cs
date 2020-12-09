using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Cache;
using UnityEngine.Diagnostics;
using System.Collections.Generic;
using Cinemachine;

public class PlayerControl : MonoBehaviour {
    public Interactable focus;
    public LayerMask movementMask,itemMask;
    public GameObject hitPrefab, arrow,posBow,CM_Cam,crossAim,canvas;
    public float fov,minT,maxT,tMultp,xMaxSpeed,yMaxSpeed,xAccel,yAccel;
    Camera cam;
    PlayerMotor motor;
    GameObject mobUI;
    Image healthSlider;
    int indx;
    public float thrust;
    private int fingerID = -1;
    public List<Transform> itemBox = new List<Transform>();
    public List<Transform> enemy = new List<Transform>();
    void Start() {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        mobUI = StaticMethods.FindInActiveObjectByName("MobUI");
        healthSlider = StaticMethods.FindInActiveObjectByName("MobHpTop").GetComponent<Image>();

        xMaxSpeed = CM_Cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed;
        yMaxSpeed = CM_Cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed;
        xAccel = CM_Cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_AccelTime;
        yAccel = CM_Cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_AccelTime;
    }

    void Update() {
        if (Input.GetKey(KeyCode.E)) { // Hold E BOW
            preShoot();
        }
        if (Input.GetKeyUp(KeyCode.E)) { // When Relase BOW Button
            shoot();
        }
        if (Input.GetKeyDown(KeyCode.G) && itemBox.Count >0) { // Grab item nearest item to player from list
            Grab();
        }
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(!EventSystem.current.IsPointerOverGameObject(fingerID)) {
                if (Physics.Raycast(ray, out hit, 100)) {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("walkable") && canvas.GetComponent<MenuUI>().controlType == 1) {
                        walk(hit);
                    }
                    else if(hit.transform.gameObject.layer == LayerMask.NameToLayer("itemBox")) {
                        itemGrab(hit);
                    }
                    else {
                        //if (hit.transform.name != "Player")
                        //    motor.MoveToPoint(hit.point);

                        Debug.DrawLine(this.transform.position, hit.point, Color.red, 1f);

                        Interactable interactable = hit.collider.GetComponentInParent<Interactable>();
                        if (interactable != null) {
                            SetFocus(interactable);
                        }
                        //Debug.Log("we hit others " + hit.collider.name + hit.point);
                    }
                    itemBox.RemoveAll(Transform => Transform == null);

                }
            }
            
        }
    }
    public void attackNear() {
        enemy.RemoveAll(Transform => Transform == null);
        //find nearest gameobject in list
        float distanceF = 0f, distanceL = 0f;
        for (int i = 0; i < enemy.Count; i++) {
            distanceF = Vector3.Distance(this.transform.position, enemy[i].position);
            if (distanceL == 0)
                distanceL = distanceF;
            if (distanceF < distanceL) {
                distanceL = distanceF;
                indx = i;
            }
        }
        //move nearest gameobject and interact
        if (enemy.Count > 0 && enemy[indx] != null) {
            motor.MoveToPoint(enemy[indx].transform.position);
            hitPrefab.transform.position = enemy[indx].transform.position;
            hitPrefab.transform.position += new Vector3(0f, 0.05f, 0f);
            Interactable interactable = enemy[indx].gameObject.GetComponentInParent<Interactable>();
            if (interactable != null) {
                SetFocus(interactable);
            }
        }


        itemBox.RemoveAll(Transform => Transform == null);
        indx = 0;
    }
    public void itemGrab(RaycastHit hit) {
        motor.MoveToPoint(hit.point);
        Debug.DrawLine(this.transform.position, hit.point, Color.red, 1f);

        Interactable interactable = hit.collider.GetComponentInParent<Interactable>();
        if (interactable != null) {
            SetFocus(interactable);
        }
        Debug.Log("we hit item mask" + hit.collider.name + hit.point);
    }
    public void walk(RaycastHit hit) {
        motor.MoveToPoint(hit.point);
        hitPrefab.transform.position = hit.point;
        hitPrefab.transform.position += new Vector3(0f, 0.1f, 0f);
        Debug.DrawLine(this.transform.position, hit.point, Color.red, 1f);
        Debug.Log("we hit movementmask" + hit.collider.name + hit.point);
        // move our player to what we hit

        // stop focusing any objects
        RemoveFocus();
    }
    public void  preShoot() {
        crossAim.SetActive(true);
        CM_Cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = 60;
        CM_Cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_AccelTime = 0.1f;
        CM_Cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed = 0.8f;
        CM_Cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_AccelTime = 0.4f;
        if (thrust < maxT)
            thrust += tMultp;
        fov = 45 - ((thrust - minT) / (maxT - minT)) * 10;
        CM_Cam.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = Mathf.Lerp(CM_Cam.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView, fov,Time.deltaTime *10);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, cam.transform.rotation, Time.deltaTime * 5);
    }
    public void shoot() {
        crossAim.SetActive(false);
        CM_Cam.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = 45;
        CM_Cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = xMaxSpeed;
        CM_Cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed = yMaxSpeed;
        CM_Cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_AccelTime = xAccel;
        CM_Cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_AccelTime = yAccel;
        fov = (int)CM_Cam.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView;
        Vector3 playerPos = this.transform.position;
        Quaternion playerRot = this.transform.rotation;
        Vector3 shootingPos = posBow.transform.position;
        Quaternion cmRot = cam.transform.rotation;



        if (thrust > maxT)
            thrust = maxT;
        arrow.GetComponent<shotArrow>().thrust = thrust;
        Debug.Log(thrust);
        Instantiate(arrow, shootingPos, cmRot);

        thrust = minT;
     }
    public void Grab() {
        itemBox.RemoveAll(Transform => Transform == null);
        //find nearest gameobject in list
        float distanceF = 0f, distanceL = 0f;
        for (int i = 0; i < itemBox.Count; i++) {
            distanceF = Vector3.Distance(this.transform.position, itemBox[i].position);
            if (distanceL == 0)
                distanceL = distanceF;
            if (distanceF < distanceL) {
                distanceL = distanceF;
                indx = i;
            }
        }
        //move nearest gameobject and interact
        if (itemBox.Count > 0 && itemBox[indx] != null) {
            
            motor.MoveToPoint(itemBox[indx].transform.position);
            hitPrefab.transform.position = itemBox[indx].transform.position;
            hitPrefab.transform.position += new Vector3(0f, 0.05f, 0f);
            Interactable interactable = itemBox[indx].gameObject.GetComponentInParent<Interactable>();
            if (interactable != null) {
                SetFocus(interactable);
                transform.GetComponent<Animator>().SetFloat("Velocity", 1f);
            }
        }


        itemBox.RemoveAll(Transform => Transform == null);
        indx = 0;
    }
    public void SetFocus(Interactable newFocus) {
        if (newFocus != focus) {
            if (focus != null)
                focus.OnDefocused();
            focus = newFocus;
            //motor.FollowTarget(newFocus);
            string foc = newFocus.name.ToString();
            foc = foc.Substring(0, 7);
            if(foc != "itemBox" && foc !="NPCNPCN") {
                StaticMethods.FindInActiveObjectByName("MobUI").SetActive(true);
                StaticMethods.FindInActiveObjectByName("MobName").SetActive(true);
                StaticMethods.FindInActiveObjectByName("MobLvl").SetActive(true);
                StaticMethods.FindInActiveObjectByName("MobHealthUi").SetActive(true);
                StaticMethods.FindInActiveObjectByName("MobMHp").SetActive(true);
                StaticMethods.FindInActiveObjectByName("MobCHp").SetActive(true);
                GameObject.Find("MobName").GetComponent<Text>().text = newFocus.GetComponent<EnemyStats>().mobName;
                GameObject.Find("MobLvl").GetComponent<Text>().text = newFocus.GetComponent<EnemyStats>().Level.ToString();
                GameObject.Find("MobMHp").GetComponent<Text>().text = " /" + newFocus.GetComponent<EnemyStats>().maxHealth.ToString();
                GameObject.Find("MobCHp").GetComponent<Text>().text = newFocus.GetComponent<EnemyStats>().currentHealth.ToString();
            }
            if(foc == "NPCNPCN") {
                StaticMethods.FindInActiveObjectByName("MobUI").SetActive(true); ;
                GameObject.Find("MobName").GetComponent<Text>().text = newFocus.GetComponent<NPC>().npcName;
                StaticMethods.FindInActiveObjectByName("MobLvl").SetActive(false);
                StaticMethods.FindInActiveObjectByName("MobHealthUi").SetActive(false);
                StaticMethods.FindInActiveObjectByName("MobMHp").SetActive(false);
                StaticMethods.FindInActiveObjectByName("MobCHp").SetActive(false);

            }
            if (healthSlider != null && gameObject.name != "Player") {
                float healthPercent = transform.GetComponent<EnemyStats>().currentHealth / (float)transform.GetComponent<EnemyStats>().maxHealth;
                healthSlider.fillAmount = healthPercent;
            }
        }
    
        newFocus.OnFocused(transform);

    }
    public void RemoveFocus() {
        if (focus != null)
            focus.OnDefocused();
        mobUI.SetActive(false);
        focus = null;
        motor.StopFollowingTarget();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 12)
            itemBox.Add(other.transform);
        if (other.gameObject.layer == 11)
            enemy.Add(other.transform);
        if (itemBox.Count > 0)
            StaticMethods.FindInActiveObjectByName("btnGrab").SetActive(true);
        else
            StaticMethods.FindInActiveObjectByName("btnGrab").SetActive(false);

    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("itemBox"))
            itemBox.Remove(other.transform);
        if (other.gameObject.layer == 11)
            enemy.Remove(other.transform);
        if (itemBox.Count == 0)
            StaticMethods.FindInActiveObjectByName("btnGrab").SetActive(false);
    }
    private void Awake() {
#if !UNITY_EDITOR
     fingerID = 0; 
#endif
    }
}
