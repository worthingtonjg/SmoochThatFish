using UnityEngine;
using System.Collections;

public class movescript : MonoBehaviour {

    private Rigidbody2D body;
    private Animator animator;
    new private SpriteRenderer renderer;
    new private CircleCollider2D collider;
    
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float jumpHeight = 10f;

    [SerializeField]
    private float slideSpeed = 2f;

    public float fireSpeed = 1f;
    public float projectileSpeed = 50f;
    public float projectileLife = 3f;
    public bool autoFire;

    public GameObject weaponPort;
    public GameObject bulletPrefab;
    public AudioClip clip;

    private AudioSource audioSource;
    private float lastFire;

    // Use this for initialization
    void Awake() {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collider = GetComponent<CircleCollider2D>();
	}

    void Start()
    {
        lastFire = Time.time - fireSpeed;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (GameManagerScript.Instance.GameOver) return;

        bool grounded = collider.IsTouchingLayers(LayerMask.GetMask("ground"));

        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
        {
            body.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, body.velocity.y);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            renderer.flipX = false;

            if (grounded)
            {
                animator.SetInteger("state", 1);
            }
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            renderer.flipX = true;

            if (grounded)
            {
                animator.SetInteger("state", 1);
            }
        }
        else
        {
            if (body.velocity.magnitude > slideSpeed)
            {
                if (collider.IsTouchingLayers(LayerMask.GetMask("ground")))
                {
                    animator.SetInteger("state", 2);
                }
            }
            else
            {
                    animator.SetInteger("state", 0);
            }
        }

        if (Input.GetAxis("Jump") > 0)  //makes player jump
        {
            if (collider.IsTouchingLayers(LayerMask.GetMask("ground")))
            {
                Debug.Log("Jump");
                animator.SetInteger("state", 3);
                body.AddForce(new Vector3(body.velocity.x, jumpHeight), ForceMode2D.Impulse);
            }
        }

        if(Input.GetButton("Fire1"))
        {
            Shoot();
        }

        //if (body.velocity.magnitude > moveSpeed)
        //{
        //    body.velocity = body.velocity.normalized * moveSpeed;
        //}
    }



    public void Shoot()
    {
        Debug.Log("Shoot Pressed");
        if (Time.time >= lastFire + fireSpeed)
        {
            lastFire = Time.time;
            var clone = Instantiate(bulletPrefab, weaponPort.transform.position, weaponPort.transform.rotation) as GameObject;
            //clone.GetComponent<Rigidbody>().velocity = gameObject.transform.TransformDirection(new Vector3(0, 0, projectileSpeed));

            if (renderer.flipX)
            {
                clone.GetComponent<Rigidbody2D>().velocity = gameObject.transform.TransformDirection(new Vector2(-projectileSpeed, 0));
            }
            else
            {
                clone.GetComponent<Rigidbody2D>().velocity = gameObject.transform.TransformDirection(new Vector2(projectileSpeed, 0));
            }

            Destroy(clone, projectileLife);
            audioSource.PlayOneShot(clip);
            Debug.Log("Shooting");
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            GameManagerScript.Instance.LoseLife();
        }
    }
}



