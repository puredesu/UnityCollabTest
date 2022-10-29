using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = playerObject.transform.position;
        pos.x += 20;
        pos.y += 45;
        pos.z += -20;
        transform.position = pos;
    }
}
