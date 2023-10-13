using UnityEngine;
using UnityEngine.UI;

public class FinallyDisplay : MonoBehaviour
{
    public static GameObject TextObject;
    public static string textField;
    void Start()
    {
        TextObject = gameObject;
        textField = TextObject.GetComponent<Text>().text;
    }

}
