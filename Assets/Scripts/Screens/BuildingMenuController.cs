using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;

public class BuildingMenuController : MonoBehaviour
{
    [Header("Referencias UI")]
    public CanvasGroup grid;
    public TMP_Text modo;
    public Button background;
    public TMP_Text title;
    public Image image;
    public Button arrowLeft;
    public Button arrowRight;

    [Header("Tiempos")]
    public float slideDuration = 2f;
    public float fadeDuration = 1f;

    [HideInInspector] public EdificioData[] items;
    [HideInInspector] public UnityAction<EdificioData> onItemSelected;
    
    public int index = 0;

    private void OnEnable()
    {
        grid.alpha = 0;
        
        ScreenController.Instance.backButton.GetComponent<CanvasGroup>().alpha = 1;
        ScreenController.Instance.backButton.GetComponent<Button>().onClick.AddListener(() => ScreenController.Instance.ShowScreen(ScreenController.Instance.pantallaCategories));

        StartCoroutine(ShowBuildings());
    }

    private void OnDisable()
    {
        ScreenController.Instance.backButton.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    IEnumerator ShowBuildings()
    {
        yield return TransitionManager.Instance.FadeCanvasGroup(grid, 0, 1, fadeDuration);
    }

    public void ArrowLeft()
    {
        if (index > 0)
            index--;
        else
            index = items.Length - 1;

        SetupItems();
    }
    
    public void ArrowRight()
    {
        if (index < items.Length - 1)
            index++;
        else
            index = 0;

        SetupItems();
    }

    public void SetupItems()
    {
        if (items != null && items.Length > 0)
        {
            title.text = items[index].nombre;
            image.sprite = items[index].silueta;

            if (background != null)
            {
                background.onClick.RemoveAllListeners();
                background.onClick.AddListener(() => OnItemClicked(index));
            }
        }
        else
        {
            Debug.LogError("BuildingMenuController: Items not assigned.");
        }
    }

    private void OnItemClicked(int index)
    {
        string selectedCodigo = items[index].codigo;

        EdificioData foundMeta = null;
        if (EdificiosDataModel.Instance != null)
        {
            List<ModoData> modos = EdificiosDataModel.Instance.GetData();
            ModoData modoData = modos.Find(m => m.modo.ToLower() == modo.text.ToLower());
            if (modoData != null)
            {
                foundMeta = modoData.datos.Find(m => m.codigo == selectedCodigo);
            }
        }

        if (foundMeta != null)
        {
            // Trigger event
            onItemSelected?.Invoke(foundMeta);
        }
    }
}
