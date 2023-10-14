using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPart : MonoBehaviour
{
    public int width { get; private set; }
    public int height { get; private set; }
    public Transform[] leftConnections { get; private set; }
    public Transform[] rightConnections { get; private set; }
    public Transform[] upConnections { get; private set; }
    public Transform[] downConnections { get; private set; }
}
