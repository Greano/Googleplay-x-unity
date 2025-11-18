using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance {get; private set;}
    private bool isMainmenuShowing = true;
    public CanvasGroup canvasGroup;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this);
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ToggleMenu()
    {
        if (isMainmenuShowing)
        {
            isMainmenuShowing = false;
            canvasGroup.alpha = 0;
            Time.timeScale = 1;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            return;
        }
        isMainmenuShowing = true;
        canvasGroup.alpha = 1;
        Time.timeScale = 0;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }

    public void LevelSelect(int level)
    {
        SceneManager.LoadScene(level);
        isMainmenuShowing = false;
        canvasGroup.alpha = 0;
        Time.timeScale = 1;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
