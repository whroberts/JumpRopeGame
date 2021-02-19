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
    [SerializeField] GameObject _deathScreen;
    [SerializeField] GameObject _rope;

    GameSetup _gameSetup;
    Animator _anim;
    PlayerScoreCounter score_counter;
    UIController _uiController;

    void Start()
    {
        _gameSetup = FindObjectOfType<GameSetup>();
        score_counter = character.GetComponent<PlayerScoreCounter>();
        _anim = character.GetComponent<Animator>();
        _uiController = FindObjectOfType<UIController>();
        

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Rope"))
        {
            score_counter.DamagePlayer(10);
            hurtEffect.Play();
            if (score_counter.curHealth == 0)
            {
                StartCoroutine("DeathAnimation");
            } else if (score_counter.curHealth > 0)
            {
                _anim.SetBool("trip", true);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Rope"))
        {
            _anim.SetBool("trip", false);
        }
    }

    IEnumerator Death()
    {
        //character dying effect
        _finalScoreText.text = score_counter._finalScore.ToString();
        _finalScorePanel.SetActive(true);
        _gameSetup.Kill();
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(2);
        //play some effect

        // I want something to flash on the screen
        // Something like Dark Souls, "GAME... OVER...
        // Combine with UI controller

        // stop the rope from spinning

        //yield return new WaitForSeconds(5);
    }

    IEnumerator DeathAnimation()
    {
        float animation_length = 0;
        _anim.SetBool("death", true);
        _deathScreen.SetActive(true);
        AnimationClip[] clips = _anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Death":
                    animation_length = clip.length;
                    break;
            }
        }
        yield return new WaitForSeconds(animation_length+4);
        StartCoroutine("Death");
    }
}
