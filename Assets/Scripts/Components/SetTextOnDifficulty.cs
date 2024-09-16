using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTextOnDifficulty : MonoBehaviour
{
    private TextMesh myText;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<TextMesh>();

        if (GlobalGame.Instance.difficulty == "Insane")
        {
            myText.text = "INSANE - 1hp, no checkpoints";
            myText.color = new Color(1, 0.1f, 0);
        }
    }
}
