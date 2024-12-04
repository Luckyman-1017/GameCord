using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class QuitButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("TitleScene");
        });
    }
}
