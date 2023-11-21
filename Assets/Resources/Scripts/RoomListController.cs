using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomListController : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text roomName;
    public RoomInfo info;

    public void SetUp(RoomInfo _info)
    {
        info = _info;
        roomName.text = _info.Name;
    }

    public void OnClick()
    {
        if (info.IsOpen && info.PlayerCount <= info.MaxPlayers)
        {
            ConnectionController.instance.JoinRoom(info);   
        }
    }
}