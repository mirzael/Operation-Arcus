using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TutorialMessageChooser : MonoBehaviour
{
    public GameObject onePlayerTwoHandedControlMessage;
    public GameObject onePlayerOneHandedControlMessage;
    public GameObject multiplayerTwoHandedControlMessage;
    public GameObject multiplayerOneHandedControlMessage;

    protected void Start()
    {
        onePlayerOneHandedControlMessage.SetActive(false);
        onePlayerTwoHandedControlMessage.SetActive(false);
        multiplayerOneHandedControlMessage.SetActive(false);
        multiplayerTwoHandedControlMessage.SetActive(false);

        bool isOneHanded = ControlScheme.isOneHanded;
        bool isMultiplayer = MultiplayerController.GetIsMultiplayer();

        if(onePlayerOneHandedControlMessage!=null && isOneHanded && !isMultiplayer)
        {
            onePlayerOneHandedControlMessage.SetActive(true);
        }
        else if(onePlayerTwoHandedControlMessage!=null && !isOneHanded && !isMultiplayer)
        {
            onePlayerTwoHandedControlMessage.SetActive(true);
        }
        else if(multiplayerOneHandedControlMessage!=null && isOneHanded && isMultiplayer)
        {
            multiplayerOneHandedControlMessage.SetActive(true);
        }
        else if(multiplayerTwoHandedControlMessage!=null && !isOneHanded && isMultiplayer)
        {
            multiplayerTwoHandedControlMessage.SetActive(true);
        }
    }
}

