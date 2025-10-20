using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GodEntryUI : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text nameText;
    private int index;
    private AlmanacManager manager;

    public void Setup(AlmanacManager mgr, GodData data, int idx)
    {
        manager = mgr;
        index = idx;

        if (iconImage) iconImage.sprite = data.godIcon;
        if (nameText) nameText.text = data.godName;

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(OnClicked);
    }

    void OnClicked()
    {
        manager.ShowGodDescription(index);
    }
}
