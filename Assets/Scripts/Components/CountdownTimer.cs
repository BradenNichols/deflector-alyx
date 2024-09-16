using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    private Text myText;

    [SerializeField]
    private Stats playerStats;

    public float initialTimer = 5;
    public float timeAddPerDeflect = 0.5f;

    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
        timer = initialTimer;

        UpdateText();
    }

    void UpdateText()
    {
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("F2");

        if (seconds.Length == 4)
            seconds = "0" + seconds;

        myText.text = minutes + ":" + seconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.isDead) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            playerStats.Kill();
        }    

        UpdateText();
    }
}
