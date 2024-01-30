using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;


public interface IDamageable
{
    void TakeDamage(int damage);
}
public class TroopsView : MonoBehaviour, IDamageable
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
    public GameObject weapon;
    public GameObject currentTarget;
    private bool isFighting;
    public bool isDead;
    public ParticleSystem hiteffect;


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
        if(isDead) return;
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
        if(isDead) return;
        if(other.gameObject.layer == GetDetectionLayerIndex())
        {
            if(isFighting) return;
            currentTarget = other.gameObject;
            agent.isStopped = true;
            Attack();
        }

        if(other.gameObject.CompareTag("PlayerHouse"))
        {
            if(troopType == TroopType.Enemy)
            {
                Attack();
                currentTarget = other.gameObject;
                agent.isStopped = true;
            }
        }
        if(other.gameObject.CompareTag("EnemyHouse"))
        {
            if(troopType == TroopType.Player)
            {
                Attack();
                currentTarget = other.gameObject;
                agent.isStopped = true;
            }
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
        isFighting = true;
    }

    public void StartDamaging()
    {
        if (currentTarget == null)
        {
            Reset();
        }
        if (currentTarget != null)
        {
            // Give damage to House.........
            if(currentTarget.name == Controller.self.troopsManager.playerHouse.name)
            {
                if(troopType == TroopType.Enemy)
                {
                    currentTarget.GetComponent<HouseView>().DecreaseHealth();
                }
            }
            else if(currentTarget.name == Controller.self.troopsManager.enemyHouse.name)
            {
                if(troopType == TroopType.Player)
                {
                    currentTarget.GetComponent<HouseView>().DecreaseHealth();
                }
            }
            else
            { // Give damage to player.........
                hiteffect.Play();
                IDamageable damageableTarget = currentTarget.GetComponent<IDamageable>();
                if (damageableTarget != null)
                {
                    damageableTarget.TakeDamage(giveDamage);
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.gotHit);
        totalHealth -= damage;

        if (totalHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        this.GetComponent<CapsuleCollider>().enabled = false;
        animator.SetBool("Attack", false);
        animator.SetBool("Death", true);
        this.GetComponent<NavMeshObstacle>().enabled = false;
        agent.speed = 0;

        if (currentTarget != null)
        {
            if (!currentTarget.GetComponent<TroopsView>().isDead)
            {
                currentTarget.GetComponent<TroopsView>().Reset();
            }
        }

        if(abilityType == AbilityType.Sword)
        {
            if (troopType == TroopType.Enemy)
            {
                Controller.self.currencyManager.collectedCoin = Controller.self.currencyManager.collectedCoin + 8;
            }
        }

        Invoke("DestroyObj", 4);
        this.enabled = false;
        weapon.transform.parent = null;
        weapon.AddComponent<Rigidbody>();
        weapon.AddComponent<BoxCollider>();
        // weapon.GetComponent<Rigidbody>().isKinematic = false;
        // weapon.GetComponent<MeshCollider>().enabled = true;
    }

    void DestroyObj()
    {
        GameObject effect = Lean.Pool.LeanPool.Spawn(Controller.self.troopsManager.troopDeadEffect);
        effect.transform.position = new Vector3(this.transform.position.x,-10.32f,this.transform.position.z);
        this.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
         {
             Lean.Pool.LeanPool.Despawn(effect,1);
             Destroy(weapon);
             Destroy(this.gameObject);
         });
    }

    public void Reset()
    {
        animator.SetBool("Attack", false);
        currentTarget = null;
        SetDestination();
        isFighting = false;
        agent.isStopped = false;
    }

}
