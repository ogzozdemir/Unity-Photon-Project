using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance { get; private set; }
    
    public TMP_Text statusText;
    public TMP_InputField playerTextInput;
    public TMP_InputField roomTextInput;
    
    [Header("Tabs")]
    public GameObject playerTab;
    public GameObject roomCreateTab;
    [SerializeField] private GameObject roomListTab;
    [SerializeField] private GameObject playerPreviewTab;
    [SerializeField] private GameObject playerListTab;
    
    [Header("Player List")]
    public GameObject playerNamePrefab;
    public Transform playerNamePrefabGrid;
    
    [Header("Room List")]
    public GameObject roomNamePrefab;
    public Transform roomNamePrefabGrid;
    
    [Header("Room and Chat")]
    public TMP_InputField messageTextInput;
    public GameObject messagePrefab;
    public Transform messagePrefabGrid;
    public GameObject startGameButton;

    private void Awake()
    {
        if (instance == null) instance = this;

        HideAllTabs();
    }

    public void ChangeStatusText(string status)
    {
        statusText.text = status;
    }
    
    public void ShowPlayerTab()
    {
        HideAllTabs();
        
        playerTab.SetActive(true);
    }
    
    public void ShowLobbyTabs()
    {
        HideAllTabs();
        
        roomCreateTab.SetActive(true);
        roomListTab.SetActive(true);
    }
    
    public void ShowRoomTabs()
    {
        HideAllTabs();

        playerPreviewTab.SetActive(true);
        playerListTab.SetActive(true);
    }

    public void HideAllTabs()
    {
        playerTab.SetActive(false);
        roomCreateTab.SetActive(false);
        roomListTab.SetActive(false);
        playerPreviewTab.SetActive(false);
        playerListTab.SetActive(false);
    }
}
