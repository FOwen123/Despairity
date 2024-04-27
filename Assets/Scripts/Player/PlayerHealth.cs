using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    [Header("Health")]
    [SerializeField] private int health;
    private int currentHealth;
    public Image[] hearts;
    public Sprite fullHearts;
    public Sprite emptyHearts;
    private Animator anim;
    private bool dead;

    [Header("IFrames")]//IFrames = Invulneberablity Frames
    [SerializeField] private float iFramesDuration;
    [SerializeField] private float numberOfFlashes;
    [SerializeField] private SpriteRenderer[] spriteRend;

    // Knockback
    private Rigidbody2D rb;

    private void Awake()
    {
        currentHealth = health;
        instance = this;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHearts;
            }
            else
            {
                hearts[i].sprite = emptyHearts;
            }

            if (i < health)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(Invulneberality());

            FindObjectOfType<AudioManager>().Play("Player Hurt");
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("Dead");
                GetComponent<PlayerMovement>().enabled = false;
                rb.bodyType = RigidbodyType2D.Static;
                dead = true;

                FindObjectOfType<AudioManager>().Play("Player Dead");
            }
        }
    }

    public IEnumerator Knockback(float knockDuration, float knockPower, Transform obj)
    {
        float timer = 0;

        while (knockDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            rb.AddForce(-direction * knockPower);
        }
        yield return 0;
    }

    private IEnumerator Invulneberality()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            foreach (var item in spriteRend)
            {
                item.color = new Color(1, 0, 0, 0.5f);
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
                item.color = Color.white;
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            }
        }
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
