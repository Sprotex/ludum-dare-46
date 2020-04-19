using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;
    public TextMeshProUGUI text;
    private float points = 0f;
    public float Points
    {
        get
        {
            return points;
        } set
        {
            points = value;
            text.SetText(CConstants.Texts.Score + Points.ToString());
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }


}
