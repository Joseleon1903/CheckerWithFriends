using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

public class ImageDropDownSelectionBehavour : MonoBehaviour
{
    enum SelectionIndex
    {
        Element0 = 0,
        Element1=  1
    }

    [SerializeField] private Image selectImage;

    [SerializeField] private Dropdown dropdown;

    [SerializeField] private Sprite[] optionImage;

    private void Awake()
    {
        int defaultIndex = (int)EnumHelper.GetEnumValue<SelectionIndex>(SelectionIndex.Element0.ToString());
        Sprite defaultImage = optionImage[defaultIndex];
        selectImage.sprite = defaultImage;
    }

    public void OnValueChange(Dropdown change) {

        Debug.Log("Value change "+ change.value);

        switch (change.value) {

            case (int)SelectionIndex.Element0:
                int defaultIndex = (int)EnumHelper.GetEnumValue<SelectionIndex>(SelectionIndex.Element0.ToString());
                selectImage.sprite = optionImage[defaultIndex];
                break;

            case (int)SelectionIndex.Element1:
                int index = (int)SelectionIndex.Element1;
                selectImage.sprite = optionImage[index];
                break;
        }
    }
}
