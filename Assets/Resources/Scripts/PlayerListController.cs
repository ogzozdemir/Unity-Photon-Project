using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerListController : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text playerNick;
    private Player player;

    public void SetUp(Player _player)
    {
        player = _player;
        playerNick.text = _player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
            Destroy(gameObject);
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
