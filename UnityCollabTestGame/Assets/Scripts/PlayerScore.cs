using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textScore;

    // Update is called once per frame
    void Update()
    {
        textScore.text = "Score: " + GameManager.Instance.Score;
    }
}
