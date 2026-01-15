using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] Button hostButton;
    [SerializeField] Button clientButton;

    [Header("Connection Settings")]
    [SerializeField] private string serverIp = "192.168.1.113";
    [SerializeField] private ushort serverPort = 7777;

    private void Awake()
    {
        hostButton.onClick.AddListener (StartHostWithDebug);
        clientButton.onClick.AddListener (StartClientWithIP);
    }
    private void StartHostWithDebug()
    {
        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        if (transport == null)
        {
            Debug.LogError("[HOST] UnityTransport component not found!");
            return;
        }

        Debug.Log("[HOST DEBUG] Preparing to start host...");

        transport.SetConnectionData(
            serverIp,  // Client address
            serverPort,
            "0.0.0.0"   // Server listen address (CRITICAL)
        );

        Debug.Log($"[HOST DEBUG] Host listening on {serverIp}:{serverPort}");

        NetworkManager.Singleton.StartHost();

        Debug.Log("[HOST] StartHost() called.");
    }

    private void StartClientWithIP()
    {
        if (string.IsNullOrWhiteSpace(serverIp))
        {
            Debug.LogError("[UI] Server IP is empty in Inspector!");
            return;
        }

        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        if (transport == null)
        {
            Debug.LogError("[UI] No UnityTransport component found!");
            return;
        }

        transport.SetConnectionData(serverIp.Trim(), serverPort);

        Debug.Log($"[CLIENT UI] Starting client → connecting to {serverIp}:{serverPort}");

        NetworkManager.Singleton.StartClient();
    }
}
