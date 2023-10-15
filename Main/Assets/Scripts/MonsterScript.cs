using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterScript : MonoBehaviour
{
    public float roamRadius = 10f;
    public float sightDistance = 20f; //Sets how far away the monster can see you
    public LayerMask raycastLayers; //Must be set to everything in the component

    private NavMeshAgent navAgent;
    private Transform playerTransform; // Reference to the player's transform component

    private bool isChasing = false;
    private Coroutine roamingCoroutine; // Store the reference to the roaming


    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        if (navAgent == null)
        {
            Debug.LogError("MonsterScript requires a NavMeshAgent component.");
            return;
        }

        // Try to find the player's transform when the game starts
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        roamingCoroutine = StartCoroutine(RoamRandomly());
    }

    void Update()
    {
        if (playerTransform == null) return;

        // Raycasting towards the player
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        RaycastHit hit;

        // Debug ray to visualize the monster's sight
        Debug.DrawRay(transform.position, directionToPlayer * sightDistance, Color.red);

        if (Physics.Raycast(transform.position, directionToPlayer, out hit, sightDistance, raycastLayers))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player spotted!");
                isChasing = true;
                if (roamingCoroutine != null)
                {
                    StopCoroutine(roamingCoroutine); // Stop the roaming
                }
                navAgent.SetDestination(playerTransform.position);
            }
            else if (isChasing)
            {
                Debug.Log("Player lost!");
                isChasing = false;
                roamingCoroutine = StartCoroutine(RoamRandomly());
            }
        }
    }

    IEnumerator RoamRandomly()
    {
        while (!isChasing)
        {
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            randomDirection += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1))
            {
                Vector3 finalPosition = hit.position;
                navAgent.SetDestination(finalPosition);
            }

            // Wait till we reach the current destination before picking another.
            float remainingDistance;
            do
            {
                remainingDistance = navAgent.remainingDistance;
                yield return null;
            }
            while (navAgent.pathPending || remainingDistance > navAgent.stoppingDistance);
        }
    }

    public void startDistraction(Vector3 p)
    {
        StartCoroutine(Distraction(p));
    }

    IEnumerator Distraction(Vector3 position)
    {
        isChasing = true;
        navAgent.SetDestination(position);
        // Wait till we reach the distracted destination before roaming
        float remainingDistance;
        do
        {
            remainingDistance = navAgent.remainingDistance;
            yield return null;
        }
        while (navAgent.pathPending || remainingDistance > navAgent.stoppingDistance);
        StartCoroutine(RoamRandomly());
    }
}
