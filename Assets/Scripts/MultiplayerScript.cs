using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the MultiplayerManager and it
/// is the foundation for our multiplayer system.
/// </summary>

public class MultiplayerScript : MonoBehaviour
{
    // Variables Start_____________________________________________________________________________
    private string titleMessage = "GTGD Series 1 Prototype";
    private string connectToIP = "127.0.0.1";
    private int connectionPort = 26500;
    private bool useNAT = false;
    private string ipAddress;
    private string port;
    private int numberOfPlayers = 10;
    public string playerName;
    public string serverName;
    public string serverNameForClient;
    private bool iWantToSetupAServer = false;
    private bool iWantToConnectToAServer = false;

    // These variables are used to define the main
    // window.
    private Rect connectionWindowRect;
    private int connectionWindowWidth = 400;
    private int connectionWindowHeight = 280;
    private int buttonHeight = 60;
    private int leftIndent;
    private int topIndent;
    // Variables End_______________________________________________________________________________

	// Use this for initialization
	void Start ()
    {
        serverName = PlayerPrefs.GetString("serverName");
        if (serverName == "")
        {
            serverName = "Server";
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void ConnectWindow(int windowID)
    {
        // Leave a gap from the header.
        GUILayout.Space(15);

        // When the palyers launches the game they have the option
        // to create a server or join a server. The variables
        // iWantToSetupAServer and iWantToConnectToAServer start as
        // false so the player is presented with two buttons 
        // "Setup my server" and "Connect to a server".
        if (iWantToSetupAServer == false && iWantToConnectToAServer == false)
        {
            if (GUILayout.Button("Setup a server", GUILayout.Height(buttonHeight)))
            {
                iWantToSetupAServer = true;
            }
            GUILayout.Space(10);
            if (GUILayout.Button("Connect to a server", GUILayout.Height(buttonHeight)))
            {
                iWantToConnectToAServer = true;
            }
            GUILayout.Space(10);
            if (Application.isWebPlayer == false && Application.isEditor == false)
            {
                if (GUILayout.Button("Exit Prototype", GUILayout.Height(buttonHeight)))
                {
                    Application.Quit();
                }
            }
        }

        if (iWantToSetupAServer == true)
        {
            // The user can type a name for their server into
            // the textfield.
            GUILayout.Label("Enter a name for your server");
            serverName = GUILayout.TextField(serverName);
            GUILayout.Space(5);

            // The user can type a port number for their server
            // into the textfield. We definde a default value above in the
            // variables as 26500.
            GUILayout.Label("Server Port");
            connectionPort = int.Parse(GUILayout.TextField(connectionPort.ToString()));
            GUILayout.Space(10);
            if (GUILayout.Button("Start my own server", GUILayout.Height(30)))
            {
                // Create the server.
                Network.InitializeServer(numberOfPlayers, connectionPort, useNAT);

                // Save the serverName using PlayerPrefs.
                PlayerPrefs.SetString("serverName", serverName);
                iWantToSetupAServer = false;
            }
            if (GUILayout.Button("Go Back", GUILayout.Height(30)))
            {
                iWantToSetupAServer = false;
            }
        }
    }

    void OnGUI()
    {
        // If the player is disconnected then run the ConnectWindow function.
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            // Determine the position of the window based on the width and 
            // height of the screen. The window will be placed in the middle 
            // of the screen.
            leftIndent = Screen.width / 2 - connectionWindowWidth / 2;
            topIndent = Screen.height / 2 - connectionWindowHeight / 2;
            connectionWindowRect = new Rect(leftIndent, topIndent, connectionWindowWidth, connectionWindowHeight);
            connectionWindowRect = GUILayout.Window(0, connectionWindowRect, ConnectWindow, titleMessage);
        }
    }
}
