using EasyUI.PickerWheelUI;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [Space]
    [Header("Picker wheel pieces :")]
    public List<WheelPiece> wheelPieces = new List<WheelPiece>();


    private static UiManager instance;

    public GameObject winnerPanel;
    public GameObject gamePanel;
    public TMP_Text winnerText;

    public TMP_InputField labelInputField;
    public Button addButton;


    public Transform textListContainer; // Drag and drop the container for the text list in the Inspector
    public GameObject textPrefab; // Drag and drop the UI Text prefab in the Inspector

    GameObject newTextObject;
    private WheelPiece wheelPieceInstance;
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

    public void Winner(string name)
    {
        gamePanel.SetActive(false);
        winnerText.text = $"Winner is {name}";
        winnerPanel.SetActive(true);

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            float chanceR = Random.Range(0, 101);
            WheelPiece newWheelPiece = new WheelPiece();
            newWheelPiece.Label = newLabel;
            newWheelPiece.Chance = chanceR; 
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

    public void removePieces()
    {
       
        RemoveWheelPiece(0);
    }


 

    private void RemoveWheelPiece(int index)
    {
        if (index >= 0 && index < wheelPieces.Count)
        {
            WheelPiece removedPiece = wheelPieces[index];
            wheelPieces.RemoveAt(index);
            Debug.Log("WheelPiece removed with label: " + removedPiece.Label);

            // Remove the corresponding UI text object
            if (textListContainer.childCount > index)
            {
                Destroy(textListContainer.GetChild(index).gameObject);
            }
        }
        else
        {
            Debug.Log("Invalid index for removing a wheel piece.");
        }
    }
    private void AddTextToList(string newText)
    {
        

        if (!string.IsNullOrEmpty(newText))
        {
          

            // Instantiate a new UI Text element and set its text
             newTextObject = Instantiate(textPrefab, textListContainer);
            newTextObject.GetComponent<TMP_Text>().text = newText;
            //newTextObject.GetComponentInChildren<Button>().onClick.AddListener(removePieces);
           
            Debug.Log("Text added: " + newText);
        }
        else
        {
            Debug.Log("Input field is empty. Please enter text.");
        }
    }
}






