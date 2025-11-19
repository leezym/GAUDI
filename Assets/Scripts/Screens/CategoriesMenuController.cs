using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class SelectedModoInfo
{
    public string modo;
    public string nombre;
    public string texto;
    public string codigo;
}

public class CategoriesMenuController : MonoBehaviour
{
    [Header("Referencias UI")]
    public CanvasGroup grid;
    public Button[] botonesCategorias;

    [Header("Pantalla Building")]
    public BuildingMenuController buildingMenuController;
    public BuildingDetailMenuController buildingDetailMenuController;

    [Header("Tiempos")]
    public float slideDuration = 2f;
    public float fadeDuration = 1f;

    private void OnEnable()
    {
        grid.alpha = 0;

        ScreenController.Instance.backButton.GetComponent<CanvasGroup>().alpha = 1;
        ScreenController.Instance.backButton.GetComponent<Button>().onClick.AddListener(() => ScreenController.Instance.ShowScreen(ScreenController.Instance.pantallaMain));

        foreach (Button b in botonesCategorias)
        {
            b.onClick.AddListener(() => OnCategoriaSeleccionada(b));
        }

        StartCoroutine(ShowCategorias());
    }

    private void OnDisable()
    {
        ScreenController.Instance.backButton.GetComponent<Button>().onClick.RemoveAllListeners();

        foreach (Button b in botonesCategorias)
            b.onClick.RemoveAllListeners();
    }

    IEnumerator ShowCategorias()
    {
        yield return TransitionManager.Instance.FadeCanvasGroup(grid, 0, 1, fadeDuration);
    }

    public void OnCategoriaSeleccionada(Button button)
    {
        string text = button.GetComponentInChildren<TMP_Text>().text;
        string[] building = text.Split(' ');
        
        OnItemClicked(string.Join(" ", building.Skip(1)));
    }

    void OnItemClicked(string selectedModo)
    {
        if (!string.IsNullOrEmpty(selectedModo) && EdificiosDataModel.Instance != null)
        {
            List<ModoData> modos = EdificiosDataModel.Instance.GetData();
            ModoData matchingModo = modos.FirstOrDefault(m => m.modo.ToLower() == selectedModo.ToLower());
            if (matchingModo != null)
            {
                buildingMenuController.items = matchingModo.datos.ToArray();
                buildingMenuController.modo.text = selectedModo.ToUpper();
                buildingMenuController.onItemSelected = OnItemSelected;
                buildingMenuController.index = 0;
                buildingMenuController.SetupItems();
            }
        }

        ScreenController.Instance.ShowScreen(ScreenController.Instance.pantallaBuilding);
    }

    private void OnItemSelected(EdificioData data)
    {
        buildingDetailMenuController.SetDetailTexts(data);

        ScreenController.Instance.ShowScreen(ScreenController.Instance.pantallaDetail);
    }
}