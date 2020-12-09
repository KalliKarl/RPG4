
using UnityEngine;

public class moveUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveUI() {
        this.transform.position = new Vector3(Input.mousePosition.x +30,Input.mousePosition.y-130,0);
    }
}
