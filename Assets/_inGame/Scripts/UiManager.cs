using EasyUI.PickerWheelUI;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class UiManager : MonoBehaviour
{
    [Space]
    [Header("Picker wheel pieces :")]
    public List<WheelPiece> wheelPieces = new List<WheelPiece>();


    private static UiManager instance;

    public InputField labelInputField;
    public Button addButton;


    public Transform textListContainer; // Drag and drop the container for the text list in the Inspector
    public GameObject textPrefab; // Drag and drop the UI Text prefab in the Inspector

    private List<string> textList = new List<string>();

    public static UiManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UiManager>();

                if (instance == null)
                {
                    GameObject singleton = new GameObject("UiManager");
                    instance = singleton.AddComponent<UiManager>();
                }
            }
            return instance;
        }
    }





    private void Start()
    {
        addButton.onClick.AddListener(AddWheelPiece);
    }

    private void AddWheelPiece()
    {
        string newLabel = labelInputField.text;

        if (!string.IsNullOrEmpty(newLabel))
        {
            WheelPiece newWheelPiece = new WheelPiece();
            newWheelPiece.Label = newLabel;
            wheelPieces.Add(newWheelPiece);
            labelInputField.text = string.Empty; // Clear the input field after adding a label
            Debug.Log("WheelPiece added with label: " + newLabel);
            AddTextToList(newLabel);
        }
        else
        {
            Debug.Log("Input field is empty. Please enter a label.");
        }
    }


    private void AddTextToList(string newText)
    {
        

        if (!string.IsNullOrEmpty(newText))
        {
          

            // Instantiate a new UI Text element and set its text
            GameObject newTextObject = Instantiate(textPrefab, textListContainer);
            newTextObject.GetComponent<Text>().text = newText;

            Debug.Log("Text added: " + newText);
        }
        else
        {
            Debug.Log("Input field is empty. Please enter text.");
        }
    }
}






