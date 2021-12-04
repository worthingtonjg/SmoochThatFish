using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class CrabAI : MonoBehaviour {
    public List<GameObject> waypoints;
    public enum EnumState { idle, patrolling, attacking, death }
    public EnumState currentState;
    public bool intransit;
    public GameObject lastWaypoint;
    public GameObject currentWaypoint;
    public float moveSpeed = 10f;
    public GameObject EnemySprite;

    private Animator animator;
    private Rigidbody2D body;
    private Collider2D[] colliders;
    new private SpriteRenderer renderer;
    
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        colliders = GetComponents<Collider2D>();

        animator = EnemySprite.GetComponent<Animator>();
        renderer = EnemySprite.GetComponent<SpriteRenderer>();
        currentState = EnumState.idle;

        StartCoroutine(EnemyAI());
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(intransit)
        {
            if(transform.position.x > currentWaypoint.transform.position.x)
            {
                body.velocity = new Vector2(-moveSpeed, body.velocity.y);
                renderer.flipX = true;
                print("moving left: " + body.velocity);
            } 
            else if(transform.position.x < currentWaypoint.transform.position.x)
            {
                body.velocity = new Vector2(moveSpeed, body.velocity.y);
                renderer.flipX = false;
                print("moving right" + body.velocity);
            }

        }
	}

    IEnumerator EnemyAI()
    {
        yield return new WaitForSeconds(Random.Range(2,5));

        if (!intransit && currentState != EnumState.death)
        {

            if (currentState == EnumState.patrolling || currentState == EnumState.attacking)
            {
                currentState = EnumState.idle;
                animator.SetInteger("state", (int)currentState);
            }
            else
            {
                currentState = (EnumState)Random.Range(0, 3); 
                animator.SetInteger("state", (int)currentState);

                if (currentState != EnumState.idle)
                {
                    intransit = true;
                    if (lastWaypoint == null)
                    {
                        currentWaypoint = waypoints[Random.Range(0, waypoints.Count)];
                    }
                    else
                    {
                        currentWaypoint = waypoints.FirstOrDefault(w => w.name != lastWaypoint.name);
                    }
                }
            }
        }

        StartCoroutine(EnemyAI());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!intransit) return;

        if(other.tag == "Waypoint")
        {
            lastWaypoint = currentWaypoint;
            currentWaypoint = null;
            intransit = false;
            animator.SetInteger("state", (int)EnumState.idle);
        }
    }

    public void Die()
    {
        if (currentState == EnumState.death) return;
        intransit = false;
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
        
        currentState = EnumState.death;
        animator.SetTrigger("die");
        Destroy(transform.parent.gameObject, 1f);
    }
}
