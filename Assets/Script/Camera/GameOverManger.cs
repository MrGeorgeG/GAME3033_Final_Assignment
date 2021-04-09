using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManger : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public GameManager GM;

    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth.currentHealth <= 0)
        {
            GM.GameOverUI.SetActive(true);
            anim.SetTrigger("GameOver");
        }

        
    }

}
