using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {
    public static GameManagerScript Instance;

    public int PopsciclesCount;
    public int FishCount;
    public int LifeCount = 3;

    public Text PopscicleText;
    public Text FishText;
    public Text LifeText;

    public GameObject WinText;
    public GameObject GameOverText;

    public AudioClip popciclePickupClip;
    public AudioClip fishPickupClip;
    public AudioClip splashClip;
    public AudioClip hitClip;

    public Rigidbody2D penguin;
    public Animator penguinAnim;

    public bool GameOver;

    private AudioSource audioSource;
    private bool winTriggered;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        PopsciclesCount = 0;
        FishCount = PlayerPrefsManager.GetFish();
        LifeCount = PlayerPrefsManager.GetLives();

        UpdateCounter(PopscicleText, PopsciclesCount);
        UpdateCounter(FishText, FishCount);
        UpdateCounter(LifeText, LifeCount);
    }

    void Update()
    {
        if (FishCount > 0 && !winTriggered)
        {
            winTriggered = true;
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        WinText.SetActive(true);
         
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator RestartGame()
    {
        if (LifeCount <= 0)
            GameOverText.SetActive(true);

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(0);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PickupPopscle()
    {
        UpdateCounter(PopscicleText, ++PopsciclesCount);
        audioSource.PlayOneShot(popciclePickupClip);
    }

    public void PickupFish()
    {
        UpdateCounter(FishText, ++FishCount);
        audioSource.PlayOneShot(fishPickupClip);
    }

    public void LoseLife()
    {
        UpdateCounter(LifeText, --LifeCount);

        penguinAnim.SetInteger("state", 9);
        penguin.isKinematic = true;
        audioSource.PlayOneShot(splashClip);

        PlayerPrefsManager.SetLives(LifeCount);

        if (LifeCount <= 0)
        {
            StartCoroutine(RestartGame());
        }
        else RestartLevel();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayHitClip()
    {
        audioSource.PlayOneShot(hitClip);
    }


    private void UpdateCounter(Text textControl, int count)
    {
        textControl.text = "x " + count;
    }


}
