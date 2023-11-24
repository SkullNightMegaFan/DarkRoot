using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    protected internal GameObject player;
    public GameObject rotationAnchor;
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 rotationAnchorPosition = transform.position;
        Vector3 playerPosition = player.transform.position;
        LookToPlayer(playerPosition, rotationAnchorPosition);
    }
    void LookToPlayer(Vector3 target, Vector3 anchor)
    {
        direction = target - anchor;

        float angle = Vector2.SignedAngle(Vector2.down, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
