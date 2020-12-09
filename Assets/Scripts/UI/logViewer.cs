using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class logViewer : MonoBehaviour
{
    public GameObject txtPrefab;

    public void entryLog(string txtMsgLog , UnityEngine.Color renk) {
        GameObject logContent = GameObject.Find("logContent");
        GameObject txtLogMessage = Instantiate(txtPrefab,logContent.transform);
        txtLogMessage.GetComponent<Text>().text = "\t"+txtMsgLog;
        txtLogMessage.GetComponent<Text>().color = renk;

    }

}
