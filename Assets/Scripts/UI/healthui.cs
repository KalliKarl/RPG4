using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthui : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform target;
    float countDown;
    Transform ui;
    Image healthSlider;
    Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        foreach ( Canvas c in FindObjectsOfType<Canvas>()) {
            if (c.renderMode == RenderMode.WorldSpace) {
                ui = Instantiate(uiPrefab,c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break; 
            }
        }

        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChaned;

    }

    void OnHealthChaned(int maxHealth, int currentHealth) {
        if(ui != null) { 
            ui.gameObject.SetActive(true);
            float healthPercent = currentHealth / (float)maxHealth;
            healthSlider.fillAmount = healthPercent;
            countDown = 15;
            if (currentHealth <= 0) {
                Destroy(ui.gameObject);
            }
        }
    }

    // Update is called once per frame
    void LateUpdate(){

        if (ui != null) {
            ui.position = target.position;
            ui.forward = -cam.forward;
        }

        if(countDown > 0) {
            countDown -= Time.deltaTime;
        }
        else {
            if (ui != null) {
                ui.gameObject.SetActive(false);
            }
        }
    }
}
