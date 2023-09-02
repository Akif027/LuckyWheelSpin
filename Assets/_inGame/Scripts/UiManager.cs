using EasyUI.PickerWheelUI;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using System.Linq;

public class UiManager : MonoBehaviour
{
    [Space]
    [Header("Picker wheel pieces :")]
    public List<WheelPiece> wheelPieces = new List<WheelPiece>();


    private static UiManager instance;

    public GameObject winnerPanel;
    public GameObject gamePanel;
    public TMP_Text winnerText;
    public Image Winimg;

    public TMP_InputField labelInputField;
    public Button addButton;
    public Button SpinButtton;


    public Transform textListContainer; // Drag and drop the container for the text list in the Inspector
    public GameObject textPrefab; // Drag and drop the UI Text prefab in the Inspector
    public GameObject iconPrefab;
    GameObject newTextObject;

    [SerializeField] GameObject GamePanel;
    [SerializeField] GameObject SetingPanel;

    public VerticalLayoutGroup textContainer; // Reference to the container holding the text elements
    public GameObject textElementPrefab; // Reference to the prefab for each text element

    private List<Transform> textElements = new List<Transform>();

    int TotalNames = 0;
    int totalImage = 0;
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

    private void Update()
    {
        foreach (Transform child in textContainer.transform)
        {
            textElements.Add(child);
        }
        if(totalImage > 0 || TotalNames > 0)
        {
             SpinButtton.interactable = true;
           
        }else
        if (totalImage <= 0 || TotalNames <= 0)
        {
            SpinButtton.interactable = false;
            
        }
      
      
    }
    public void SortTextAlphabetically()
    {
        textElements = textElements.OrderBy(textElement => textElement.GetComponent<TMP_Text>().text).ToList();

        // Rearrange the UI elements based on the new order
        RearrangeUIElements();
    }

    private void RearrangeUIElements()
    {
        foreach (Transform textElement in textElements)
        {
            textElement.SetParent(null);
        }

        foreach (Transform textElement in textElements)
        {
            textElement.SetParent(textContainer.transform);
        }
    }
    public void SortWheelPiecesAlphabetically()
    {
        wheelPieces = wheelPieces.OrderBy(piece => piece.Label).ToList();

        SortTextAlphabetically();
    }

    public void Winner(string name,Sprite sprite)
    {
        gamePanel.SetActive(false);
  
        winnerText.text = $"Winner is {name}";
        Winimg.sprite = sprite;
        winnerPanel.SetActive(true);

    }

    public void SelectImage()
    {
        string imagePath = UnityEditor.EditorUtility.OpenFilePanel("Select Image", "", "png,jpg,jpeg");
        if (!string.IsNullOrEmpty(imagePath) && totalImage < 9)
        {
            StartCoroutine(LoadImage(imagePath));
        }
    }
    IEnumerator LoadImage(string path)
    {
        var www = new WWW("file://" + path);
        yield return www;
      

        Texture2D loadedTexture = www.texture;

        // Convert the Texture2D to a Sprite
        Sprite loadedSprite = Sprite.Create(loadedTexture, new Rect(0, 0, loadedTexture.width, loadedTexture.height), Vector2.one * 0.5f);

        WheelPiece newWheelPiece = new WheelPiece();
        newWheelPiece.Icon = loadedSprite;

        wheelPieces.Add(newWheelPiece);
       
        AddImgoList(newWheelPiece.Icon);

    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void resetTextandImg()
    {
        totalImage = 0;
        TotalNames = 0;
        gamePanel.SetActive(true);
        SetingPanel.SetActive(false);
    }


    private void AddWheelPiece()
    {
     
        string newLabel = labelInputField.text;
    
        if (!string.IsNullOrEmpty(newLabel) && TotalNames < 9)
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
        Debug.Log(TotalNames);

        if (!string.IsNullOrEmpty(newText) && TotalNames <9)
        {
          

            // Instantiate a new UI Text element and set its text
             newTextObject = Instantiate(textPrefab, textListContainer);
            newTextObject.GetComponent<TMP_Text>().text = newText;
            //newTextObject.GetComponentInChildren<Button>().onClick.AddListener(removePieces);
            TotalNames++;
            Debug.Log("Text added: " + newText);
        }
        else
        {
            Debug.Log("Input field is empty. Please enter text.");
        }
    }


    private void AddImgoList(Sprite img)
    {


        if (img !=null && totalImage<=9)        {


            // Instantiate a new UI Text element and set its text
            newTextObject = Instantiate(iconPrefab, textListContainer);
            newTextObject.GetComponent<Image>().sprite = img;
            //newTextObject.GetComponentInChildren<Button>().onClick.AddListener(removePieces);
            totalImage++;
            Debug.Log("img added: " + img);
        }
        else
        {
            Debug.Log("Input field is empty. Please enter img.");
        }
    }
}






