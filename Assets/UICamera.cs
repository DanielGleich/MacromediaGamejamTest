using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 cursorPos;
        if (Vector3.Distance(cameraCenter, mousePos) < radius)
            cursorPos = new Vector2(mousePos.x, mousePos.y);
        else {
           Vector3 maxRadiusPos = Vector3.MoveTowards(cameraCenter, mousePos, radius);
            cursorPos = new Vector2(maxRadiusPos.x, maxRadiusPos.y);
        }
            gameObject.GetComponent<RectTransform>().position = cursorPos;
    }
}
