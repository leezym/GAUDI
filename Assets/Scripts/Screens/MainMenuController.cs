using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using Unity.Mathematics;

public class MainMenuController : MonoBehaviour
{
    [Header("Referencias UI")]
    public CanvasGroup grid;

    [Header("Tiempos")]
    public float slideDuration = 2f;
    public float fadeDuration = 1f;

    private EdificiosDataModel dataModel;

    private void OnEnable()
    {
        grid.alpha = 0;

        ScreenController.Instance.backButton.GetComponent<CanvasGroup>().alpha = 0;

        StartCoroutine(ShowMenu());
    }

    private void OnDisable()
    {
        ScreenController.Instance.backButton.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    IEnumerator ShowMenu()
    {
        yield return TransitionManager.Instance.FadeCanvasGroup(grid, 0, 1, fadeDuration);
    }

    public void OnStartGame()
    {
        // Initialize EdificiosDataModel
        dataModel = new EdificiosDataModel(Path.Combine(Application.persistentDataPath, "edificios.json"));
        dataModel.Initialize();

        ScreenController.Instance.ShowScreen(ScreenController.Instance.pantallaCategories);
    }
}