using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AboutMenuController : MonoBehaviour
{
    [Header("Referencias UI")]
    public CanvasGroup grid;

    [Header("Tiempos")]
    public float slideDuration = 2f;
    public float fadeDuration = 1f;
    
    private void OnEnable()
    {
        grid.alpha = 0;

        ScreenController.Instance.backButton.GetComponent<CanvasGroup>().alpha = 1;
        ScreenController.Instance.backButton.GetComponent<Button>().onClick.AddListener(() => ScreenController.Instance.ShowScreen(ScreenController.Instance.pantallaMain));
        StartCoroutine(ShowInfo());
    }

    private void OnDisable()
    {
        ScreenController.Instance.backButton.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    IEnumerator ShowInfo()
    {
        yield return TransitionManager.Instance.FadeCanvasGroup(grid, 0, 1, fadeDuration);
    }
}
