using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mUI : MonoBehaviour{

    public void UIToggle(GameObject UI) {

        if (UI.activeSelf)
            UI.SetActive(false);
        else
            UI.SetActive(true);
    }
}
