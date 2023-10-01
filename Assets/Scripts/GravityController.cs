using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
	private Vector3 up = new Vector3(0, 9.81f, 0);
	private Vector3 left = new Vector3(9.81f, 0, 0);
	private Vector3 front = new Vector3(0, 0, 9.81f);

	// Start is called before the first frame update
	void Start()
	{
		
	}

	public void ChangeGrav2UP(){Physics.gravity = up;}

	public void ChangeGrav2Down(){Physics.gravity = up * -1;}

	public void ChangeGrav2Left(){Physics.gravity = left;}

	public void ChangeGrav2Rught(){Physics.gravity = left * -1;}

	public void ChangeGrav2Front(){Physics.gravity = front;}

	public void ChangeGrav2Back(){Physics.gravity = front * -1;}
}
