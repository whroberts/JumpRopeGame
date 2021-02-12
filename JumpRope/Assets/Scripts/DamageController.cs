using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DamageController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject character;
    [SerializeField] ParticleSystem hurtEffect;
    [SerializeField] TMP_Text _finalScoreText;
    [SerializeField] GameObject _finalScorePanel;

    GameSetup _gameSetup;
    Animator anim;
    PlayerScoreCounter score_counter;
    UIController uIController;

    void Start()
    {
        _gameSetup = FindObjectOfType<GameSetup>();
        score_counter = character.GetComponent<PlayerScoreCounter>();
        anim = character.GetComponent<Animator>();
        

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Rope"))
        {
            anim.SetBool("trip", true);
            score_counter.DamagePlayer(10);
            hurtEffect.Play();
            if (score_counter.curHealth == 0)
            {
                StartCoroutine("Death");
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Rope"))
        {
            anim.SetBool("trip", false);
        }
    }

    IEnumerator Death()
    {
        //character dying effect

        _finalScoreText.text = score_counter._finalScore.ToString();
        _finalScorePanel.SetActive(true);
        print("1");
        _gameSetup.Kill();
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(2);
        print("p");
        //play some effect

        // I want something to flash on the screen
        // Something like Dark Souls, "GAME... OVER...
        // Combine with UI controller

        // stop the rope from spinning

        //yield return new WaitForSeconds(5);
    }
}
