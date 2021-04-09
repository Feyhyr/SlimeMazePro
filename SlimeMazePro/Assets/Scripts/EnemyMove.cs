using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public List<Transform> waypoints;
    public int currentWaypoint = 0;
    public float minimumDistance = 1f;
    [Space(10)]
    public Transform target; // player to find
    public float minimumTargetDistance = 5f;
    public float maximumTargetDistance = 20f;
    public bool chasing = false;
    [Space(10)]
    public Transform raycastPoint;

    public AudioClip SfxAlert;
    public AudioClip SfxLeave;

    public void PlayAudioAlert()
    {
        AudioManager.Instance.Play(SfxAlert);
    }

    public void PlayAudioLeave()
    {
        AudioManager.Instance.Play(SfxLeave);
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[currentWaypoint].position);
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        // If the target is within my distance to chase. I will follow target
        if (distanceToTarget < minimumTargetDistance && !chasing)
        {
            // check with raycast to ensure the npc can see the player
            RaycastHit hit;
            Debug.DrawRay(raycastPoint.position, target.position - raycastPoint.position);
            if (Physics.Raycast(raycastPoint.position, target.position - raycastPoint.position, out hit))
            {
                Debug.Log("I see you");
                PlayAudioAlert();
                chasing = true;
            }
        }
        else if (distanceToTarget > maximumTargetDistance && chasing)
        {
            PlayAudioLeave();
            chasing = false;
        }

        if (chasing)
        {
            agent.SetDestination(target.position);
        }
        else if (!chasing)
        {
            agent.SetDestination(waypoints[currentWaypoint].position);
        }

        float distanceCheck = Vector3.Distance(transform.position, waypoints[currentWaypoint].position);
        if (distanceCheck < minimumDistance && !chasing)
        {
            currentWaypoint++;
            if (currentWaypoint > waypoints.Count - 1)
            {
                currentWaypoint = 0;
            }
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            chasing = false;

            CoinManager coin = GameObject.Find("CoinManager").GetComponent<CoinManager>();
            coin.index = 0;
            coin.PickupEvent();
        }
    }
}
