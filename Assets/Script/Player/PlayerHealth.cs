using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public int firstAIDNumber;
    public int Number = 0;
    public Text firstAIDText;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    private Animator anim;
    private AudioSource playerAudio;
    private PlayerMovement playerMovement;
    private PlayerShooting playerShooting;
    private bool isDead;
    private bool damaged;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
        firstAIDNumber = Number;
    }

    // Update is called once per frame
    void Update()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
        firstAIDText.text = "x " + firstAIDNumber;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(currentHealth < startingHealth && firstAIDNumber > 0)
            {
                TakeHealth();
            }
        }
    }

    public void TakeHealth()
    {
        firstAIDNumber -= 1;

        currentHealth += 20;

        healthSlider.value = currentHealth;

        if (currentHealth >= startingHealth)
        {
            currentHealth = startingHealth;
        }

        playerAudio.Play();

    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play();

        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Health" && firstAIDNumber < 3)
        {
            Destroy(other.gameObject);
            firstAIDNumber += 1;
        }
    }

    private void Death()
    {
        isDead = true;

        anim.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

}
