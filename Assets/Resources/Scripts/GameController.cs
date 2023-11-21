using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviourPunCallbacks
{
    private PhotonView pV;

    public TMP_Text playerNameText;

    private GameObject playerOne;
    private GameObject playerTwo;

    private void Awake()
    {
        pV = GetComponent<PhotonView>();
    }
    
    private void Start()
    {
        playerNameText.text = PhotonNetwork.LocalPlayer.NickName;

        if (PhotonNetwork.PlayerList.Length == 1)
        {
            if (PhotonNetwork.IsMasterClient)
                playerOne = PhotonNetwork.Instantiate("Prefabs/Player1", new Vector3(0.3f,0f,-1.25f), Quaternion.Euler(0f, 65f, 0f), 0);
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
                playerOne = PhotonNetwork.Instantiate("Prefabs/Player1", new Vector3(0.3f,0f,-1.25f), Quaternion.Euler(0f, 65f, 0f), 0);
            else
                playerTwo = PhotonNetwork.Instantiate("Prefabs/Player2", new Vector3(1.25f,0f,-0.3f), Quaternion.Euler(0f, 128f, 0f), 0);
        }
    }
    
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        pV.RPC("KickPlayer", RpcTarget.All);
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Menu");
    }

    [PunRPC]
    void KickPlayer()
    {
        PhotonNetwork.LeaveRoom();
    }
}