using UnityEngine;

public class PlayerPrefsHacker : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            print("Reset");
            PlayerPrefs.DeleteAll();
        }
    }
}
