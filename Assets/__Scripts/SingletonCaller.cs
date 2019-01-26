using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonCaller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		var mPlayah = MusicPlayer.Instance;
    }
}
