using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDetailMenuController : MonoBehaviour
{
    [Header("Referencias UI")]
    public CanvasGroup grid;
    public ScrollRect scrollRect;
    public TMP_Text nombre;
    public TMP_Text texto;
    public Image color;
    public RectTransform BN;
    public Image blanco_negro;
    public Image coordenadas;

    [Header("Tiempos")]
    public float slideDuration = 2f;
    public float fadeDuration = 1f;

    private void OnEnable()
    {
        grid.alpha = 0;
        scrollRect.verticalNormalizedPosition = 1f;

        ScreenController.Instance.backButton.GetComponent<CanvasGroup>().alpha = 1;
        ScreenController.Instance.backButton.GetComponent<Button>().onClick.AddListener(() => ScreenController.Instance.ShowScreen(ScreenController.Instance.pantallaBuilding));

        StartCoroutine(ShowDetails());
    }

    private void OnDisable()
    {
        ScreenController.Instance.backButton.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    IEnumerator ShowDetails()
    {
        // Adjust text component height based on content
        if (texto != null)
        {
            texto.ForceMeshUpdate();
            float preferredHeight = texto.GetPreferredValues(texto.text, texto.GetComponent<RectTransform>().rect.width, 0).y;
            texto.GetComponent<RectTransform>().sizeDelta = new Vector2(texto.GetComponent<RectTransform>().sizeDelta.x, preferredHeight);
        }

        // Adjust image sizes maintaining aspect ratio
        if (color.sprite != null)
        {
            float width = color.GetComponent<RectTransform>().rect.width;
            float aspectRatio = color.sprite.rect.height / color.sprite.rect.width;
            float height = width * aspectRatio;
            color.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        }

        /*if (blanco_negro.sprite != null)
        {
            float width = blanco_negro.GetComponent<RectTransform>().rect.width;
            float aspectRatio = blanco_negro.sprite.rect.height / blanco_negro.sprite.rect.width;
            float height = width * aspectRatio;
            blanco_negro.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        }

        if (coordenadas.sprite != null)
        {
            float width = coordenadas.GetComponent<RectTransform>().rect.width;
            float aspectRatio = coordenadas.sprite.rect.height / coordenadas.sprite.rect.width;
            float height = width * aspectRatio;
            coordenadas.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        }*/

        // Adjust BN height based on child elements
        if (BN != null)
        {
            BN.sizeDelta = new Vector2(BN.sizeDelta.x, coordenadas.sprite.rect.height);
        }

        yield return TransitionManager.Instance.FadeCanvasGroup(grid, 0, 1, fadeDuration);
    }

    public void SetDetailTexts(EdificioData data)
    {
        if (nombre != null) nombre.text = data.nombre;
        if (texto != null) texto.text = data.texto;
        if (color != null) color.sprite = data.color;
        if (blanco_negro != null) blanco_negro.sprite = data.blanco_negro;
        if (coordenadas != null) coordenadas.sprite = data.coordenadas;
    }
}
