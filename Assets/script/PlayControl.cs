using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayControl : MonoBehaviour
{
    float moveX;

    Rigidbody2D rb;

    [Header("이동 속도")]
    [SerializeField][Range(100f, 2000f)] float moveSpeed = 400f;

    [Header("점프 강도")]
    [SerializeField][Range(100f, 800f)] float jumpForce = 400f;

    public AudioSource mySfx;

    public AudioClip jumpSfx;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        rb.velocity = new Vector2(moveX, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rb.velocity.y == 0)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);

                JumpSound();
            }
        }
    }

    public void JumpSound()
    {
        mySfx.PlayOneShot(jumpSfx);
    }

}