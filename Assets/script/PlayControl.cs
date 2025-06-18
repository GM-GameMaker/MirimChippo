using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerControll : MonoBehaviour
{
    private bool canDoubleJump = false;
    private bool hasDoubleJumpItem = false;

    public float gameTime = 60f;
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
    private float safeTimeThreshold = 0.2f; // 0.2초 이상 땅에 있어야 안전 위치로 인정


    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Vector3 lastSafeGroundPosition;

    public GameObject pauseUI; // PauseUI를 유니티에서 연결할 수 있도록 public으로!



    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public Image[] clearIcons;
    private int collectedItemCount = 0;

    public GameObject clearUI; // 클리어 UI 패널

    void Start()
    {

        Time.timeScale = 1f; // 씬 로드시 물리 엔진 다시 작동
        timer = gameTime;

        originalSpeed = maxSpeed;
        currentHealth = maxHealth;
        startPosition = transform.position;

        lastSafeGroundPosition = transform.position;

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

        if (isGrounded)
        {
            safeGroundTime += Time.deltaTime;

            if (safeGroundTime >= safeTimeThreshold)
            {
                lastSafeGroundPosition = transform.position;
            }
        }
        else
        {
            safeGroundTime = 0f;
        }


        // 타이머, 죽음 조건, 낙사 등 기존 로직 유지
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

    public void OnSettingsButtonClicked()
    {
        pauseUI.SetActive(true); // 설정창 보이기
        Time.timeScale = 0f;     // 일시정지
    }

    public void OnClickContinue()
    {
        pauseUI.SetActive(false); // 설정창 닫기
        Time.timeScale = 1f;      // 다시 실행
    }

    public void GoToStartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene"); // ← 너의 시작 씬 이름!
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
        Debug.Log("게임 오버!");
    }

    [SerializeField] private float knockbackForce = 5f; // 유니티에서 조정 가능하게

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("충돌한 오브젝트 태그: " + collision.gameObject.tag);

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
            Debug.Log("FinishFlag에 닿음!");
            Debug.Log($"collectedItemCount: {collectedItemCount}, clearIcons.Length: {clearIcons.Length}");

            if (collectedItemCount >= clearIcons.Length)
            {
                Debug.Log("조건 만족! Clear() 호출!");
                Clear();
            }
            else
            {
                Debug.Log("아직 아이템 부족해서 클리어 안 됨!");
            }
        }


    }



    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;

            // 플랫폼에서 떨어지면 부모 해제
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

        if (other.CompareTag("SlowItem")) // 속도 느려지는 아이템
        {
            ApplySlow(0.5f, 3f); // 50% 속도, 3초 지속
            Destroy(other.gameObject);
        }

        if (other.CompareTag("fastItem")) // 속도 빨라지는 아이템
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
            GameOver(); // 여기서 바로 처리
        }
        else
        {
            Respawn(); // 체력이 남아있으면 리스폰
        }
    }


    void RespawnToStart()
    {
        transform.position = startPosition;
        rigid.velocity = Vector2.zero;
    }

    void RespawnInPlace()
    {
        rigid.velocity = Vector2.zero; // 위치 유지, 멈춤만!
    }

    void Respawn()
    {
        transform.position = lastSafeGroundPosition;
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
        Time.timeScale = 1f; // 정지 풀기
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드
    }

    void Clear()
    {
        Debug.Log("Clear 함수 호출됨!");
        Debug.Log("현재 collectedItemCount: " + collectedItemCount);
        Debug.Log("필요한 아이템 개수: " + clearIcons.Length);
        Time.timeScale = 0f;
        clearUI.SetActive(true);
    }

}