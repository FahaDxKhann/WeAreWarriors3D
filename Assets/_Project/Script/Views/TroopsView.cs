using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TroopsView : MonoBehaviour
{
    public enum TroopType
    {
        Player,
        Enemy
    }
    public enum AbilityType
    {
        Sword,
        Hammer,
        Ninja
    }

    [SerializeField]
    private TroopType troopType;
    [SerializeField]
    private AbilityType abilityType;
    public LayerMask detectionLayer;
    public float detectionRadius = 5f;
    public int totalHealth;
    public int giveDamage;
    public Animator animator;
    private NavMeshAgent agent;
    public GameObject currentTarget;
    private bool isFighting;


    private void Start() 
    {
        agent = GetComponent<NavMeshAgent>();
        SetDestination();


        if(abilityType == AbilityType.Sword)
        {
            SetSelectedLayerActive(0);
        }
        else if(abilityType == AbilityType.Hammer)
        {
            SetSelectedLayerActive(1);
        }
        else if(abilityType == AbilityType.Ninja)
        {
            SetSelectedLayerActive(2);
        }
    }

    private void Update()
    {
        if(isFighting) return;
        if(currentTarget != null)
        {
            currentTarget = currentTarget.gameObject;
            UpdateDestination();
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

        foreach (Collider collider in colliders)
        {
            currentTarget = collider.gameObject;
            UpdateDestination();
        }
    }

    void SetSelectedLayerActive(int selectedLayerIndex)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            float weight = (i == selectedLayerIndex) ? 1f : 0f;
            animator.SetLayerWeight(i, weight);
        }
    }


    public void SetDestination()
    {
        if (troopType == TroopType.Player)
        {
            agent.SetDestination(Controller.self.troopsManager.enemyHouse.position);
        }
        else
        {
            agent.SetDestination(Controller.self.troopsManager.playerHouse.position);
        }
    }
    public void UpdateDestination()
    {
        agent.SetDestination(currentTarget.transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == GetDetectionLayerIndex())
        {
            currentTarget = other.gameObject;
            agent.isStopped = true;
            Attack();
        }
    }

    public int GetDetectionLayerIndex()
    {
        int layerIndex = -1;

        for (int i = 0; i < 32; i++)
        {
            if ((detectionLayer.value & (1 << i)) != 0)
            {
                layerIndex = i;
                break;
            }
        }
        return layerIndex;
    }

    public void Attack()
    {
        animator.SetBool("Attack", true);
    }
}
