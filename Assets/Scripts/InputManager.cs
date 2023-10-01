using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	public float forceAmount;
	public Vector3 forceVector = Vector3.zero;
	public Rigidbody rBod;

	private PlayerInput playerInput;
	private InputAction p_TouchPres;
	private InputAction p_TouchPos;
	private InputAction p_Gyroscope;

	private bool useKeys = false;
	private InputAction p_KeyUp;
	private InputAction p_KeyDown;
	private InputAction p_KeyLeft;
	private InputAction p_KeyRight;


	void Awake()
	{
		rBod = GetComponent<Rigidbody>();

		playerInput = GetComponent<PlayerInput>();
		p_TouchPres = playerInput.actions.FindAction("TouchPres");
		p_TouchPos = playerInput.actions.FindAction("TouchPos");

		if(Accelerometer.current != null){
			//p_Gyroscope = playerInput.actions.FindAction("Gyroscope");//would like it to work like this
			InputSystem.EnableDevice(Accelerometer.current);//but curentley only works like this
			//InputSystem.EnableDevice(Gyroscope.current);
		}else{
			//for use in PC
			useKeys = true;
			p_KeyUp = playerInput.actions.FindAction("KeyUp");
			p_KeyDown = playerInput.actions.FindAction("KeyDown");
			p_KeyLeft = playerInput.actions.FindAction("KeyLeft");
			p_KeyRight = playerInput.actions.FindAction("KeyRight");
		}
	}

	public void FixedUpdate()
	{
		if(!useKeys){
			//Vector3 girGyr = p_Gyroscope.ReadValue<Vector3>();//would like it to work like this
			Vector3 girGyr = Accelerometer.current.acceleration.ReadValue();//but curentley only works like this
			//Debug.Log($"{girGyr.x}, {girGyr.y}, {girGyr.z}");
			Move(girGyr);
		}
	}


	private void OnEnable(){
		p_TouchPres.performed += TouchPressed;
		if(useKeys){
			p_KeyUp.performed += AddVluUp;
			p_KeyDown.performed += AddVluDwn;
			p_KeyLeft.performed += AddVluLft;
			p_KeyRight.performed += AddVluRgt;
		}
	}

	private void DisEnable(){
		p_TouchPres.performed -= TouchPressed;
		if(useKeys){
			p_KeyUp.performed -= AddVluUp;
			p_KeyDown.performed -= AddVluDwn;
			p_KeyLeft.performed -= AddVluLft;
			p_KeyRight.performed -= AddVluRgt;
		}
	}

	//private void OnEnable(){p_TouchPres.performed += TouchPressed;}
	//private void DisEnable(){p_TouchPres.performed -= TouchPressed;}
	private void TouchPressed(InputAction.CallbackContext context){
		Vector2 possition = p_TouchPos.ReadValue<Vector2>();
		Debug.Log($"{possition.x}, {possition.y}");
	}

	private void AddVluUp(InputAction.CallbackContext context){
		forceVector = new Vector3(forceAmount, 0, 0);
		Move(forceVector);
	}
	private void AddVluDwn(InputAction.CallbackContext context){
		forceVector = new Vector3(-forceAmount, 0, 0);
		Move(forceVector);
	}
	private void AddVluLft(InputAction.CallbackContext context){
		forceVector = new Vector3(0, 0, forceAmount);
		Move(forceVector);
	}
	private void AddVluRgt(InputAction.CallbackContext context){
		forceVector = new Vector3(0, 0, -forceAmount);
		Move(forceVector);
	}

	public void Move(Vector3 force){
		rBod.AddForce(force);
	}
}
