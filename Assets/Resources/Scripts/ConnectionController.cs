using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class ConnectionController : MonoBehaviourPunCallbacks
{
    public static ConnectionController instance;
    private PhotonView pV;

    private void Awake()
    {
        if (instance == null) instance = this;
        pV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (isConnectedToInternet())
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (UIController.instance.playerTab.activeSelf)
            {
                JoinWithUsername();
            }
            else if (UIController.instance.roomCreateTab.activeSelf)
            {
                CreateRoom(); 
            }
        }
    }

    public override void OnConnectedToMaster()
    {
        UIController.instance.ChangeStatusText("Connected");

        UIController.instance.ShowPlayerTab();
    }

    public override void OnJoinedLobby()
    {
        UIController.instance.ChangeStatusText("Hello, " + PhotonNetwork.NickName + "!");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trans in UIController.instance.roomNamePrefabGrid)
        {
            Destroy(trans.gameObject);
        }
        
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(UIController.instance.roomNamePrefab, UIController.instance.roomNamePrefabGrid, false).GetComponent<RoomListController>().SetUp(roomList[i]);
        }
    }

    public override void OnJoinedRoom()
    {
        UIController.instance.ChangeStatusText("Welcome to " + PhotonNetwork.CurrentRoom.Name);
        
        UIController.instance.ShowRoomTabs();

        foreach (Transform child in UIController.instance.playerNamePrefabGrid)
        {
            Destroy(child.gameObject);
        }
        
        foreach (Transform child in UIController.instance.messagePrefabGrid)
        {
            Destroy(child.gameObject);
        }
        
        Player[] players = PhotonNetwork.PlayerList;
        
        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(UIController.instance.playerNamePrefab, UIController.instance.playerNamePrefabGrid, false).GetComponent<PlayerListController>().SetUp(players[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(UIController.instance.playerNamePrefab, UIController.instance.playerNamePrefabGrid, false).GetComponent<PlayerListController>().SetUp(newPlayer);
    }
    
    #region Methods
    
    public void StartGamePUN()
    {
        if (PhotonNetwork.PlayerList.Length >= 1)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            pV.RPC("StartGame", RpcTarget.AllBufferedViaServer);
        }
    }

    [PunRPC]
    void StartGame()
    {
        PhotonNetwork.LoadLevel("Game");
    }
    
    public void JoinWithUsername()
    {
        if (!string.IsNullOrWhiteSpace(UIController.instance.playerTextInput.text))
        {
            PhotonNetwork.NickName = UIController.instance.playerTextInput.text;
            PhotonNetwork.JoinLobby();
            UIController.instance.ShowLobbyTabs();
        }
    }
    
    public void CreateRoom()
    {
        if (!string.IsNullOrWhiteSpace(UIController.instance.roomTextInput.text))
        {
            PhotonNetwork.CreateRoom(UIController.instance.roomTextInput.text, new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
            UIController.instance.HideAllTabs();
        }
    }
    
    public void JoinRandomRoom()
    {
        if (!string.IsNullOrWhiteSpace(UIController.instance.playerTextInput.text))
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public void JoinRoom(RoomInfo info)
    {
        if (!string.IsNullOrWhiteSpace(UIController.instance.playerTextInput.text))
        {
            PhotonNetwork.JoinRoom(info.Name);
        }
    }
    
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        
        foreach (Transform child in UIController.instance.messagePrefabGrid)
        {
            Destroy(child.gameObject);
        }
    }
    
    private bool isConnectedToInternet()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
    
    #endregion
    
    #region Failed methods
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        UIController.instance.ShowLobbyTabs();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        UIController.instance.ShowLobbyTabs();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        UIController.instance.ShowLobbyTabs();
    }
    
    #endregion
}
