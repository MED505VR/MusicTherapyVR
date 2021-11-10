using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]

public partial class MovModel

{
    [RealtimeProperty(1, true, true)]
    bool _movPlay;
}