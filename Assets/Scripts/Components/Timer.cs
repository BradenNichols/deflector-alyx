using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text myText;

    public bool setOnce = false;
    public bool resetTimer = false;
    public bool doNotSetLabel = false;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();

        if (resetTimer == true)
        {
            GlobalGame.Instance.timer = 0;
        }

        UpdateText();
    }

    void UpdateText()
    {
        string minutes = Mathf.Floor(GlobalGame.Instance.timer / 60).ToString("00");
        string seconds = (GlobalGame.Instance.timer % 60).ToString("F2");

        if (seconds.Length == 4)
        {
            seconds = "0" + seconds;
        }

        myText.text = minutes + ":" + seconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (setOnce == true)
            return;

        GlobalGame.Instance.timer += Time.deltaTime;

        if (!doNotSetLabel)
            UpdateText();
    }
}
