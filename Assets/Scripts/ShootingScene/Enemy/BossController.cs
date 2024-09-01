using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BossController : MonoBehaviour
{
    private GameObject player;
    PlayerController playerController;
    
    public float hpFirst; // green
    public float hpSecond; // red

    private Animator animator;

    private bool onDead;
    private bool isMoving;

    private int score;

    private float time;

    private Transform spawnMovePos;

    private float speed;
    
    // bullet position
    public Transform LAttackPos;
    public Transform RAttackPos;
    // bullet
    public GameObject bossBullet;
    // bullet delay
    float fireDelay;
    
    // status
    // 0: idle, move
    // 1: L attack
    // 2: R attack
    // 3: Dead
    private int animationNumber;

    // 피격 시
    public SpriteRenderer spriteRenderer;
    private Color currentColor;

    void Awake()
    {
        hpFirst = 150;
        hpSecond = 150;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        spawnMovePos = GameObject.Find("BossSpawn").GetComponent<Transform>();

        animator = GetComponent<Animator>();

        onDead = false;
        isMoving = true;

        score = 1000;

        speed = 10;

        currentColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Move();
        }
        if (onDead)
        {
            time += Time.deltaTime;
        }

        if (time > 0.6f)
        {
            Destroy(gameObject);
        }
        
        if (player == null && GameManager.instance.isGameOver == false)
        {
            FindPlayer();
        }
        
        FireBullet();
        AnimationSystem();
    }

    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    void FireBullet()
    {
        if (isMoving) return;

        if (IsFirstPhase())
        {
            fireDelay += Time.deltaTime;
            
            if (fireDelay > 1.0f && animationNumber != 1)
            {
                animationNumber = 1;
                fireDelay -= fireDelay;
            }
        }

        if (IsSecondPhase())
        {
            fireDelay += Time.deltaTime;
            if (fireDelay > 1.0f && animationNumber != 2)
            {
                animationNumber = 2;
                fireDelay -= fireDelay;
            }
        }
    }

    private bool IsFirstPhase()
    {
        return hpFirst > 0;
    }

    private bool IsSecondPhase()
    {
        return hpFirst <= 0 && hpSecond > 0;
    }

    void AnimationSystem()
    {
        if (animationNumber == 0)
        {
            StartCoroutine(Co_Idle());
        }
        if (animationNumber == 1)
        {   
            StartCoroutine(Co_LAttack());
        }
        if (animationNumber == 2)
        {
            StartCoroutine(Co_RAttack());
        }
    }

    IEnumerator Co_Idle()
    {
        animationNumber = -1;
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(0.6f);
    }
    
    IEnumerator Co_LAttack()
    {
        animationNumber = -1;
        animator.SetTrigger("LAttack");
        yield return new WaitForSeconds(0.6f);
    }
    
    IEnumerator Co_RAttack()
    {
        animationNumber = -1;
        animator.SetTrigger("RAttack");
        yield return new WaitForSeconds(0.6f);
        animator.SetTrigger("RAttack");
        yield return new WaitForSeconds(0.6f);
        animator.SetTrigger("RAttack");
        yield return new WaitForSeconds(0.6f);
    }

    void LAttack()
    {
        if (player == null) return;
        Instantiate(bossBullet, LAttackPos.position, Quaternion.identity);
        fireDelay -= 1f;
    }
    
    
    void RAttack()
    {
        if (player == null) return;
        Instantiate(bossBullet, RAttackPos.position, Quaternion.identity);
        fireDelay -= 1f;
    }

    private void SetDead()
    {
        onDead = true;
        if (gameObject.tag != "Untagged")
        {
            UIController.instance.AddScore(score);
            SoundManager.instance.enemyDeadSound.Play();
        }

        gameObject.tag = "Untagged";

        UIController.instance.isBossSpawn = false;
        UIController.instance.BossHpBarHide();
    }
    
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, spawnMovePos.position, speed * Time.deltaTime);
        if (transform.position == spawnMovePos.position)
        {
            isMoving = false;
        }
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (hpFirst > 0)
            {
                hpFirst -= playerController.damage;
            }
            else
            {
                hpSecond -= playerController.damage;
            }
            
            StartCoroutine(OnDamagedEffect());
        }
        
        if (other.CompareTag("Explosion"))
        {
            if (hpFirst > 0)
            {
                hpFirst -= playerController.boomDamage;
            }
            else
            {
                hpSecond -= playerController.boomDamage;
            }
            
            StartCoroutine(OnDamagedEffect());
        }
        
        if (hpFirst <= 0 && hpSecond <= 0)
        {
            animator.SetTrigger("Die");
            SetDead();
        }
        
        
        if (other.CompareTag("BlockCollider"))
        {
            Disappear();
        }
    }
    
    
    private void Disappear()
    {
        Destroy(gameObject);
    }
    
    IEnumerator OnDamagedEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = currentColor;
    }
}
