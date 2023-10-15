using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterScript : MonoBehaviour
{
    public float roamRadius = 10f;
    public float sightDistance = 20f;
    public LayerMask raycastLayers;

    private NavMeshAgent navAgent;
    private Transform playerTransform;
    private Coroutine roamingCoroutine;
    private bool isChasing = false;
    private bool isDistracted = false;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        if (navAgent == null)
        {
            Debug.LogError("MonsterScript requires a NavMeshAgent component.");
            return;
        }

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

        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        RaycastHit hit;

        Debug.DrawRay(transform.position, directionToPlayer * sightDistance, Color.red);

        if (Physics.Raycast(transform.position, directionToPlayer, out hit, sightDistance, raycastLayers) && IsPlayerWithinFieldOfView())
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player spotted!");
                isChasing = true;
                isDistracted = false;
                if (roamingCoroutine != null)
                {
                    StopCoroutine(roamingCoroutine);
                }
                navAgent.SetDestination(playerTransform.position);
            }
            else if (isChasing && !isDistracted)
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

            float remainingDistance;
            do
            {
                remainingDistance = navAgent.remainingDistance;
                yield return null;
            }
            while (navAgent.pathPending || remainingDistance > navAgent.stoppingDistance);
        }
    }

    public IEnumerator Distraction(Vector3 position)
    {
        Debug.Log("Monster has been notified, target position: " + position);
        isDistracted = true;
        navAgent.SetDestination(position);
        Debug.Log("Monster destination set: " + navAgent.destination);

        float remainingDistance;
        do
        {
            remainingDistance = navAgent.remainingDistance;
            if (PlayerInSight())
            {
                Debug.Log("Player spotted during distraction, initiating chase.");
                isDistracted = false;
                isChasing = true;
                navAgent.SetDestination(playerTransform.position);
                yield break;
            }
            yield return null;
        }
        while (navAgent.pathPending || remainingDistance > navAgent.stoppingDistance);

        Debug.Log("Monster reached distraction point, resuming roaming.");
        isDistracted = false;
        StartCoroutine(RoamRandomly());
    }

    public void startDistraction(Vector3 p)
    {
        StartCoroutine(Distraction(p));
    }

    bool PlayerInSight()
    {
        if (playerTransform == null || !IsPlayerWithinFieldOfView()) return false;

        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, directionToPlayer, out hit, sightDistance, raycastLayers))
        {
            return hit.collider.CompareTag("Player");
        }

        return false;
    }

    bool IsPlayerWithinFieldOfView()
    {
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        // Adjust the angle range (e.g., 90 degrees) to fit your desired field of view
        return angleToPlayer <= 90f;
    }
}
