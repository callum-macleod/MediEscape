using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class GuardAI : MonoBehaviour
{
    public EnemyInfo enemyData;
    
    [Header("Field of View (FOV)")]
    public float fovAngle = 60f;
    private float fovRange;

    // [Header("Proximity Detection")]
    private float proximityRadius;

    [Header("Patrolling")]
    [SerializeField] public Transform patrolAreaCenter;
    [SerializeField] public float patrolAreaRadius = 5f;
    [SerializeField] public float minPatrolDistance = 2f;
    [HideInInspector] public Transform[] waypoints;
    private int currentWaypoint;

    [Header("Attacking")]
    private Animator animator;
    private float attackingRange;
    private int damage;
    private bool isAttacking = false;

    [Header("Target")]
    [SerializeField] public Transform target;

    [Header("Debug")]
    [SerializeField] private bool drawGizmos = false;

    private NavMeshAgent agent;
    private bool targetEscaped = false;
    private bool wasInFOV = false;
    private float searchTimer = 0f;
    private Vector2 lastKnownDirection = Vector2.right;
    [HideInInspector] public string STATE;


    [SerializeField] List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    Vector3 lastPos = Vector3.zero;
    int facingDirection = 1;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        if(animator == null)
            animator = GetComponent<Animator>();

        attackingRange = agent.stoppingDistance;

        enemyData = Instantiate(enemyData);
        fovRange = enemyData.FOVRange;
        proximityRadius = enemyData.poximityRadius;
    }

    void FixedUpdate()
    {
        if(agent.velocity.sqrMagnitude > 0.01f)
            lastKnownDirection = agent.velocity.normalized;
        
        if(target != null){
            if(isPlayerInFOV() || isPlayerInProximity()){
                targetEscaped = false;
                wasInFOV = true;
                agent.SetDestination(target.position);
                STATE = "CHASE";

                if(Vector3.Distance(transform.position, target.position) <= attackingRange)
                    Attack();
            }else if(wasInFOV && targetEscaped){
                SearchForTarget();
                STATE = "SEARCH";
            }else{
                Patrol();
                STATE = "PATROL";
            }
        }

        Debug.Log($"Guard {transform.name} is in {STATE} state");

        SetFacingDirection();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // some walls (front and back) require us to dynamically update the draw order of sprites
        // while in contact with such walls:
        if (collision.tag == "RequiresDrawOrder")
        {
            int tilemapOrder = collision.GetComponent<TilemapRenderer>().sortingOrder;  // get draw order

            // is wall above or below player?
            if (collision.bounds.center.y > transform.position.y)
                tilemapOrder++;
            else
                tilemapOrder--;

            // update player sprites draw order
            foreach (SpriteRenderer sprite in spriteRenderers)
            {
                sprite.sortingOrder = tilemapOrder;
            }
        }
    }
    void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public bool isPlayerInFOV()
    {
        if(target == null) return false;

        Vector3 eyePosition = transform.position + new Vector3(0f, 1.5f, 0f);

        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float angleToTarget = Vector3.Angle(lastKnownDirection, directionToTarget);

        if(angleToTarget < fovAngle/2f){
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if(distanceToTarget <= fovRange){
                // Debug.DrawRay(eyePosition, directionToTarget * fovRange, Color.red, 1f);
                int layerMask = LayerMask.GetMask("Player", "Walls");
                RaycastHit2D hit = Physics2D.Raycast(eyePosition, directionToTarget, fovRange, layerMask);
                if(hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player")){
                    // Debug.DrawRay(eyePosition, directionToTarget * fovRange, Color.green, 5f);
                    return true;
                }
            }
        }

        if(!targetEscaped && wasInFOV)
            targetEscaped = true;
        return false;
    }

    bool isPlayerInProximity()
{
    if (target == null) return false;

    float distanceToTarget = Vector3.Distance(transform.position, target.position);
    if (distanceToTarget < proximityRadius)
    {
        // Vector3 eyePosition = transform.position + new Vector3(0f, 1.5f, 0f);
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        int layerMask = LayerMask.GetMask("Player", "Walls"); 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, layerMask);

        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Debug.DrawRay(transform.position, directionToTarget * distanceToTarget, Color.blue, 1f);
            return true;
        }
    }

    if (!targetEscaped && wasInFOV)
    {
        targetEscaped = true;
    }
    
    return false;
}

    void Patrol()
    {
        if(Vector3.Distance(transform.position, agent.destination) < 2f)
            agent.SetDestination(GetRandomWaypoint());
    }

    Vector2 GetRandomWaypoint()
    {
        Vector2 randomPoint = Random.insideUnitCircle * patrolAreaRadius;
        Vector3 randomPos = new Vector3(randomPoint.x, 0f, randomPoint.y) + patrolAreaCenter.transform.position;

        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPos, out hit, patrolAreaRadius, NavMesh.AllAreas))
            return hit.position;

        return transform.position;
    }

    void SearchForTarget()
    {
        float searchTimeLimit = 5f;
        searchTimer += Time.deltaTime;

        if(searchTimer >= searchTimeLimit){
            targetEscaped = false;
            wasInFOV = false;
            searchTimer = 0f;
            Patrol();
        }else{
            agent.SetDestination(target.position);
        }
    }

    void Attack()
    {
        if(animator != null && !isAttacking){
            isAttacking = true;
            animator.SetTrigger("Attack");

            if(target != null){
                // Deal damage
            }
        }

        Invoke("ResetAttack", 1f);
    }

    void ResetAttack() => isAttacking = false;

    void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        // Get guard's eye position
        Vector3 eyePosition = transform.position + new Vector3(0f, 1.5f, 0f);

        // Draw Proximity Circle
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, proximityRadius);
        
        // Draw FOV cone
        Gizmos.color = Color.yellow;
        float halfFOV = fovAngle / 2f;
        Vector3 facingDirection = new Vector3(lastKnownDirection.x, lastKnownDirection.y, 0f);
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.forward);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.forward);

        Vector3 leftRayDirection = leftRayRotation * facingDirection;
        Vector3 rightRayDirection = rightRayRotation * facingDirection;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(eyePosition, leftRayDirection * fovRange);
        Gizmos.DrawRay(eyePosition, rightRayDirection * fovRange);
    }

    int SetFacingDirection()
    {
        // calculate movement since last physics frame
        float xmov = lastPos.x - transform.position.x;
        lastPos = transform.position;
        print(xmov);
        // if not moving horiztonally, change nothing.
        if (Mathf.Abs(xmov) <= 0.01)
            return facingDirection;

        // return 1 if moving left, return -1 if moving right;
        int newDirection = (int)(xmov / Mathf.Abs(xmov));

        if (newDirection == facingDirection)
            return facingDirection;

        // flip character if necessary and return (and set) new direction value
        transform.localScale = new Vector3(newDirection, 1, 1);
        return facingDirection = newDirection;
    }
}
