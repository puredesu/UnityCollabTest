using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetScript : MonoBehaviour
{
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    private GameObject playerObject;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (playerObject != null)
        {
            agent.destination = playerObject.transform.position;
        }
    }
}
