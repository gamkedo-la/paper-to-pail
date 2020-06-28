using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		LevelManager.Instance.AddGoal();
    }

	public void Complete() {
		LevelManager.Instance.RemoveGoal();
		Destroy(gameObject);
	}
}
