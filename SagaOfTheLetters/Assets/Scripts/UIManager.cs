using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private Text wordText;
    [SerializeField] private TextMeshProUGUI findedWordsText;
    [SerializeField] private Image findenWordImage;

    public Text WordText
    {
        get{return wordText;}
        set{wordText = value;}
    }

    public void SetWordText(string sentence)
    {
        wordText.text = sentence.ToUpper().ToString();
    }
    public void SetColorOfWordText(Color color)
    {
        wordText.color = color;
    }
    public void SetFindedText(string sentence)
    {
        findenWordImage.gameObject.SetActive(true);
        findedWordsText.text = (sentence != null) ? sentence.ToUpper().ToString() : "Not FOUND ANY WORD!";
    }
}
