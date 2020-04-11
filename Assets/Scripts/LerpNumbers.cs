using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerpNumbers : MonoBehaviour
{
    const int N = 9;
    public float LerpTime = 1f;
    public Color InitialColor;
    public Color LerpColor;

    public IEnumerator LerpOutput(int[] grid)
    {
        for (int i = 0; i < 81; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.GetComponent<InputField>().text == string.Empty)
            {
                gameObject.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().color = LerpColor;
                for (int j = 0; j < grid[i]; j++)
                {
                    gameObject.transform.GetChild(i).gameObject.GetComponent<InputField>().text = grid[i].ToString();
                    yield return new WaitForSeconds(LerpTime);
                }
            }
            gameObject.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().color = InitialColor; ;
        }
    }
}
