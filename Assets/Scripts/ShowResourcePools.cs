using UnityEngine;
using System.Collections;

public class ShowResourcePools : MonoBehaviour {
    public Transform[] pools;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
        for(int a = 0; a < pools.Length; ++a) {
            pools[a].gameObject.SetActive(BuildResource.GetResource((ResourceType)a).Unlocked);
        }
	}
}
