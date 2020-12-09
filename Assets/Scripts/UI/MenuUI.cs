
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MenuUI : MonoBehaviour
{
    public GameObject menuUI;
    Bloom bloomLayer = null;
    MotionBlur motionBlur = null;
    public float bloom = 10f;
    public int controlType;
 
    void Start()
    {
        int qualityLevel = QualitySettings.GetQualityLevel();
        Debug.Log("qualitylevel : "+qualityLevel);
    }
    void Update()
    {
        if (Input.GetButtonDown("ESC")) {
            
            menuUI.SetActive(!menuUI.activeSelf);

        }
    }
    public void setMute() {
        bool mute = Camera.main.GetComponent<AudioListener>().isActiveAndEnabled;
        if (mute)
            Camera.main.GetComponent<AudioListener>().enabled = false;
        else
            Camera.main.GetComponent<AudioListener>().enabled = true;
    }
    public void SetVolume(float volume) {
        Debug.Log(volume);
    }
    public void SetResolution(int index) {
        
        switch (index) {
            case 0:
                Screen.SetResolution(1920, 1080, true, 60);
                Application.targetFrameRate = 60;
                Debug.Log("0 res");
                break;
            case 1:
                Screen.SetResolution(1920, 1080, true, 30);
                Application.targetFrameRate = 30;
                Debug.Log("1 res");
                break;
            case 2:
                Screen.SetResolution(1280, 720, true, 60);
                Application.targetFrameRate = 60;
                Debug.Log("2 res");
                break;
            case 3:
                Screen.SetResolution(1280, 720, true, 30);
                Application.targetFrameRate = 30;
                Debug.Log("3 res");
                break;
            case 4:
                Screen.SetResolution(800, 600, true, 60);
                Application.targetFrameRate = 60;
                Debug.Log("4 res");
                break;
            case 5:
                Screen.SetResolution(800, 600, true, 30);
                Application.targetFrameRate = 30;
                Debug.Log("5 res");
                break;
        }
        

    }
    public void SetGraphics(int gIndex) {
        switch (gIndex) {
            case 0:
                QualitySettings.SetQualityLevel(0, true);
                break;
            case 1:
                QualitySettings.SetQualityLevel(1, true);
                break;
            case 2:
                QualitySettings.SetQualityLevel(2, true);
                break;
            case 3:
                QualitySettings.SetQualityLevel(3, true);
                break;
            case 4:
                QualitySettings.SetQualityLevel(4, true);
                break;
            case 5:
                QualitySettings.SetQualityLevel(5, true);
                break;
        }
        
    }
    public void SetAA(int aIndex) {
        GameObject cam2 = GameObject.Find("Main Camera");
        PostProcessLayer layer = cam2.GetComponent<PostProcessLayer>();
        
        switch (aIndex) {

            case 0:
                layer.antialiasingMode = PostProcessLayer.Antialiasing.FastApproximateAntialiasing;
                break;
            case 1:
                layer.antialiasingMode = PostProcessLayer.Antialiasing.SubpixelMorphologicalAntialiasing;
                break;
            case 2:
                layer.antialiasingMode = PostProcessLayer.Antialiasing.TemporalAntialiasing;
                break;
            case 3:
                layer.antialiasingMode = PostProcessLayer.Antialiasing.None;
                break;
        }
    }
    public void SetBloom() {
        GameObject cam2 = GameObject.Find("Main Camera");
        PostProcessVolume volume = cam2.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out bloomLayer);
        
        if (bloomLayer.enabled.value) {
            bloomLayer.enabled.value = false;
            bloomLayer.active = false;
        }
        else {
            bloomLayer.enabled.value = true;
            bloomLayer.active = true;
        }
        //bloomLayer.intensity.value = bloom;
    }
    public void SetMotionBlur() {
        GameObject cam2 = GameObject.Find("Main Camera");
        PostProcessVolume volume = cam2.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out motionBlur);
        
        if (motionBlur.enabled.value) {
            motionBlur.enabled.value = false;
            motionBlur.active = false;
        }
        else {
            motionBlur.enabled.value = true;
            motionBlur.active = true;
        }
        //bloomLayer.intensity.value = bloom;
    }
    public void SetShadows(int shadows) {
        GameObject dLight = GameObject.Find("Directional Light");
        switch (shadows) {
            case 0:
                dLight.GetComponent<Light>().shadows = LightShadows.Soft;
                break;
            case 1:
                dLight.GetComponent<Light>().shadows = LightShadows.Hard;
                break;
            case 2:
                dLight.GetComponent<Light>().shadows = LightShadows.None;
                break;
        }
        
    
    
    }

    public void SetContorl(int control) {
        switch (control) {
            case 0:
                controlType = 0;
                break;
            case 1:
                controlType = 1;
                break;
        }
    }
    public void QuitGame() {

        Application.Quit();
    }
}
