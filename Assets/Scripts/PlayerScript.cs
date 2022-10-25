using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public int scoreValue = 0;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public Text lives;
    private int livesValue = 3;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    private bool gameOver;
    private bool transformCharacter;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives.text = livesValue.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        musicSource.clip = musicClipOne;
        musicSource.loop = true;
        musicSource.Play();
        gameOver = false;
        transformCharacter = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
        SetTeleport ();
        SetWinText ();
    }

    void Update() 
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
     private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if(collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void SetTeleport()
    {
        if(scoreValue == 4 && transformCharacter == false)
        {
            transformCharacter = true;
            transform.position = new Vector2(44.0f, 0.0f);
            livesValue = 3;
        }
    }

    void SetWinText()
    {
        lives.text = "Lives: " + livesValue.ToString();
        if(livesValue <= 0)
        {
            Destroy(this);
            loseTextObject.SetActive(true);
        }

        if(scoreValue >= 8 && gameOver == false)
        {
            gameOver = true;
            winTextObject.SetActive(true);
            musicSource.clip = musicClipTwo;
            musicSource.loop = false;
            musicSource.Play();
        }

    }

}
