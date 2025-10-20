using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class AlmanacManager : MonoBehaviour
{
    [Header("God List Data")]
    public List<GodData> gods = new List<GodData>();

    [Header("UI References")]
    public Transform contentParent;        // The Grid or Vertical Layout container
    public GameObject godEntryPrefab;      // Prefab for small icons/names

    [Header("Big Display UI")]
    public Image bigImage;
    public TMP_Text bigName;
    public TMP_Text bigDescription;

    private List<GodEntryUI> entries = new List<GodEntryUI>();

    void Start()
    {
        PopulateList();
    }

    void PopulateList()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        entries.Clear();

        for (int i = 0; i < gods.Count; i++)
        {
            var entryObj = Instantiate(godEntryPrefab, contentParent);
            var entry = entryObj.GetComponent<GodEntryUI>();
            entry.Setup(this, gods[i], i);
            entries.Add(entry);
        }

        // Optionally, show first god by default
        if (gods.Count > 0)
            ShowGodDescription(0);
    }

    public void ShowGodDescription(int index)
    {
        if (index < 0 || index >= gods.Count) return;

        GodData data = gods[index];
        bigImage.sprite = data.godImage;
        bigName.text = data.godName;
        bigDescription.text = data.godDescription;
    }
}
