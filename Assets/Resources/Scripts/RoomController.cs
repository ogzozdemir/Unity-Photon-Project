using System;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class RoomController : MonoBehaviour
{
    private PhotonView pV;

    private void Awake()
    {
        pV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
;           UIController.instance.startGameButton.SetActive(true);
        }
        else
        {
            UIController.instance.startGameButton.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendMessagePUN();
        }
    }

    public void SendMessagePUN()
    {
        if (!string.IsNullOrWhiteSpace(UIController.instance.messageTextInput.text))
        {
            string sender = PhotonNetwork.LocalPlayer.NickName;
            string message = UIController.instance.messageTextInput.text;
            
            pV.RPC("SendMessage", RpcTarget.AllBufferedViaServer, sender, message);

            UIController.instance.messageTextInput.text = "";
        }
    }

    [PunRPC]
    void SendMessage(string sender, string message)
    {
        GameObject messageObject = Instantiate(UIController.instance.messagePrefab, UIController.instance.messagePrefabGrid, false);
        messageObject.transform.GetChild(0).GetComponent<TMP_Text>().text = sender + ": " + message;
    }
}
