using UnityEngine;
using Photon.Chat;

public class ChatManager : MonoBehaviour, IChatClientListener 
{
    ChatClient cliente;
    [SerializeField] string userID;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cliente = new ChatClient(this);
        cliente.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(userID));
    }

    // Update is called once per frame
    void Update()
    {
        cliente.Service();
    }
}
