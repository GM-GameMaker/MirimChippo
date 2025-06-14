using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    private bool canDoubleJump = false;
    private bool hasDoubleJumpItem = false;

    public float gameTime = 40f;
    private float timer;
    public TMP_Text timerText;

    public GameObject gameOverUI;

    public float maxSpeed;
    public float boostedSpeed = 10f;
    public float boostDuration = 5f;
    private float originalSpeed;
    private Coroutine boostCoroutine;
    private Coroutine slowCoroutine;
    private Coroutine doubleJumpCoroutine;
    public float doubleJumpDuration = 3f;

    public float jumpPower;
    public int maxHealth = 3;
    private int currentHealth;
    private bool isGrounded = false;
    private Vector3 startPosition;
    private float safeGroundTime = 0f;
    private float safeTimeThreshold = 0.2f; // 0.2�� �̻� ���� �־�� ���� ��ġ�� ����


    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Vector3 lastSafePosition;


    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public Image[] clearIcons;
    private int collectedItemCount = 0;

    public GameObject clearUI; // Ŭ���� UI �г�

    void Start()
    {

        Time.timeScale = 1f; // �� �ε�� ���� ���� �ٽ� �۵�
        timer = gameTime;

        originalSpeed = maxSpeed;
        currentHealth = maxHealth;
        startPosition = transform.position;

        lastSafePosition = startPosition; // ���� ��ġ�� �ʱ� ���� ��ġ�� ����

        UpdateHearts();
        timer = gameTime;
        gameOverUI.SetActive(false);
        clearUI.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Clear();
        }

        // ���� ������ �ð� ����
        if (isGrounded)
        {
            safeGroundTime += Time.deltaTime;

            // ����� ���� �־����� ���� ��ġ ����
            if (safeGroundTime >= safeTimeThreshold)
            {
                lastSafePosition = transform.position;
            }
        }
        else
        {
            safeGroundTime = 0f;
        }

        // Ÿ�̸�, ���� ����, ���� �� ���� ���� ����
        timer -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.CeilToInt(timer).ToString();

        if (timer <= 0f || (collectedItemCount < clearIcons.Length && timer <= 0f))
        {
            GameOver();
        }

  
        if (transform.position.y < -10f)
        {
            TakeDamage();
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
            else if (hasDoubleJumpItem && canDoubleJump)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                canDoubleJump = false;
                hasDoubleJumpItem = false;
            }
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(0.5f * rigid.velocity.normalized.x, rigid.velocity.y);
        }

        if (Input.GetButtonDown("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        animator.SetBool("isWalking", Mathf.Abs(rigid.velocity.x) > 0.1f);
    }

    public void BoostSpeed()
    {
        if (boostCoroutine != null)
            StopCoroutine(boostCoroutine);

        boostCoroutine = StartCoroutine(SpeedBoostRoutine());
    }

    public void ApplySlow(float slowMultiplier, float duration)
    {
        if (slowCoroutine != null)
            StopCoroutine(slowCoroutine);

        slowCoroutine = StartCoroutine(SlowDownRoutine(slowMultiplier, duration));
    }

    private IEnumerator SlowDownRoutine(float multiplier, float duration)
    {
        maxSpeed = originalSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        maxSpeed = originalSpeed;
        slowCoroutine = null;
    }

    IEnumerator SpeedBoostRoutine()
    {
        maxSpeed = boostedSpeed;
        yield return new WaitForSeconds(boostDuration);
        maxSpeed = originalSpeed;
        boostCoroutine = null;
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
        Debug.Log("���� ����!");
    }

    [SerializeField] private float knockbackForce = 5f; // ����Ƽ���� ���� �����ϰ�

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("�浹�� ������Ʈ �±�: " + collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            if (collision.gameObject.name.Contains("MovingPlatform"))
            {
                transform.parent = collision.transform;
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 knockDir = (transform.position.x < collision.transform.position.x) ? Vector2.left : Vector2.right;
            rigid.AddForce(new Vector2(knockDir.x * knockbackForce, 2f), ForceMode2D.Impulse);
            TakeDamage();
        }

        if (collision.gameObject.CompareTag("FinishFlag"))
        {
            Debug.Log("FinishFlag�� ����!");
            Debug.Log($"collectedItemCount: {collectedItemCount}, clearIcons.Length: {clearIcons.Length}");

            if (collectedItemCount >= clearIcons.Length)
            {
                Debug.Log("���� ����! Clear() ȣ��!");
                Clear();
            }
            else
            {
                Debug.Log("���� ������ �����ؼ� Ŭ���� �� ��!");
            }
        }


    }



    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;

            // �÷������� �������� �θ� ����
            if (collision.gameObject.name.Contains("MovingPlatform"))
            {
                transform.parent = null;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            collectedItemCount++;
            UpdateClearIcons();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("DoubleJumpItem"))
        {
            EnableDoubleJumpForLimitedTime();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("SlowItem")) // �ӵ� �������� ������
        {
            ApplySlow(0.5f, 3f); // 50% �ӵ�, 3�� ����
            Destroy(other.gameObject);
        }

        if (other.CompareTag("fastItem")) // �ӵ� �������� ������
        {
            BoostSpeed();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("HealthItem"))
        {
            if (currentHealth < maxHealth)
            {
                currentHealth++;
                UpdateHearts();
            }
            Destroy(other.gameObject);
        }


        if (other.CompareTag("plustimeItem"))
        {
            timer += 10f;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("minustimeItem"))
        {
            timer -= 10f;
            Destroy(other.gameObject);
        }


    }

    public void EnableDoubleJumpForLimitedTime()
    {
        hasDoubleJumpItem = true;
        canDoubleJump = true;
    }

    void UpdateClearIcons()
    {
        for (int i = 0; i < clearIcons.Length; i++)
        {
            Color iconColor = clearIcons[i].color;
            if (i < collectedItemCount)
            {
                iconColor.a = 1f;
            }
            else
            {
                iconColor.a = 0.5f;
            }
            clearIcons[i].color = iconColor;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < -maxSpeed)
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
    }

    void TakeDamage()
    {
        currentHealth--;
        UpdateHearts();

        if (currentHealth <= 0)
        {
            GameOver(); // ���⼭ �ٷ� ó��
        }
        else
        {
            Respawn(); // ü���� ���������� ������
        }
    }


    void RespawnToStart()
    {
        transform.position = startPosition;
        rigid.velocity = Vector2.zero;
    }

    void RespawnInPlace()
    {
        rigid.velocity = Vector2.zero; // ��ġ ����, ���㸸!
    }

    void Respawn()
    {
        transform.position = lastSafePosition; // ������ ���� ��ġ�� �̵�
        rigid.velocity = Vector2.zero;
    }


    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }

    public void RetryGame()
{
    Time.timeScale = 1f; // ���� Ǯ��
    SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���� �� �ٽ� �ε�
}

    void Clear()
    {
        Debug.Log("Clear �Լ� ȣ���!");
        Debug.Log("���� collectedItemCount: " + collectedItemCount);
        Debug.Log("�ʿ��� ������ ����: " + clearIcons.Length);
        Time.timeScale = 0f;
        clearUI.SetActive(true);
    }


}
