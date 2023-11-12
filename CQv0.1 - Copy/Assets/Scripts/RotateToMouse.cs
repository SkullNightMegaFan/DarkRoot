using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    public GameObject rotationAnchor;
    public MouseBehaviour mouseBehaviour;
    Vector2 direction;

    private void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        // ROTATION SCRIPT
        Vector3 rotationAnchorPosition = rotationAnchor.transform.position;
        Vector3 mousePosition = mouseBehaviour.mousePosition;
        direction = mousePosition - rotationAnchorPosition;  // find vector toward mouse

        float angle = Vector2.SignedAngle(Vector2.down, direction);  // find angle between start position and mouse vector
        transform.eulerAngles = new Vector3(0, 0, angle);   // set the object�s Z rotation to the angle value
    }
}
