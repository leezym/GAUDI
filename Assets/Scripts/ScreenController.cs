using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class ScreenController : MonoBehaviour
{
    public static ScreenController Instance;

    [Header("Referencias a Pantallas")]
    public GameObject pantallaMain;
    public GameObject pantallaCategories;
    public GameObject pantallaBuilding;
    public GameObject pantallaDetail;

    [Header("Referencias a Botones")]
    public Button backButton;
    public Button exitButton;

    private GameObject currentScreen;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        ShowScreen(pantallaMain);
    }

    public void ShowScreen(GameObject screen)
    {
        if (currentScreen != null)
            currentScreen.SetActive(false);

        currentScreen = screen;
        currentScreen.SetActive(true);
    }
    public void OnExitGame()
    {
        // letrero estas seguro de salir?
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        // letrero estas seguro de salir
    }
}