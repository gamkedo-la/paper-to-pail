using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Controller
{
	None,
	XBox,
	PS4orOther,
}

public class FlightController : MonoBehaviour
{
	public float speed = 10f;
	public float bank = 50f;
	public float pull = 30f;
	public float pickup = 10f;

	[SerializeField] private float controllerDeadzone = 0.3f;

	private float gravity = 0.001f;
	private Controller connectedController = Controller.None;
	private Vector2 input = Vector2.zero;

	public AudioClip[] rustleSoundEffects;
	private AudioSource audioSource;
	public float audioTimeOut = 0;

	private void Start() {
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		GetInput( );
		Move( );
		Rotate( );

		audioSource.volume = speed / 40f;

		if (Time.time >= audioTimeOut) {
			float delay = Random.Range(0, 40/speed - 1);
			AudioClip sfxClip = rustleSoundEffects[Random.Range(0, rustleSoundEffects.Length)];
			audioSource.clip = sfxClip;
			audioSource.PlayDelayed(delay);

			audioTimeOut = sfxClip.length + delay + Time.time;
		}
	}

	void FixedUpdate( )
	{
		CheckForControllers( );
	}

	private void GetInput( )
	{
		if ( connectedController == Controller.None )
		{
			input.y = Input.GetAxis( "Vertical" );
			input.x = Input.GetAxis( "Horizontal" );
		}
		else if ( connectedController == Controller.XBox )
		{
			input.y = Input.GetAxis( "VerticalXBox" );
			input.x = Input.GetAxis( "HorizontalXBox" );

			input.y = Mathf.Abs( input.y ) <= controllerDeadzone ? 0f : input.y;
			input.x = Mathf.Abs( input.x ) <= controllerDeadzone ? 0f : input.x;
		}
		else
		{
			input.y = Input.GetAxis( "VerticalPS4orOther" );
			input.x = Input.GetAxis( "HorizontalPS4orOther" );
#if UNITY_WEBGL
            input.y = Input.GetAxis( "VerticalPS4orOtherWebGL" );
			input.x = Input.GetAxis( "HorizontalPS4orOtherWebGL" );
#endif
			input.y = Mathf.Abs( input.y ) <= controllerDeadzone ? 0f : input.y;
			input.x = Mathf.Abs( input.x ) <= controllerDeadzone ? 0f : input.x;
		}
	}

	private void Move( )
	{
		transform.position += transform.forward * Time.deltaTime * speed + Vector3.down * gravity * Time.deltaTime;
		speed -= transform.forward.y * Time.deltaTime * pickup;
	}

	private void Rotate( )
	{
		transform.Rotate( input.y * Time.deltaTime * pull, input.x * Time.deltaTime * bank, 0f);
	}

	private void CheckForControllers( )
	{
		connectedController = Controller.None;
		string[] controllers = Input.GetJoystickNames();

		for ( int i = 0; i < controllers.Length; i++ ) // Is there at least one?
		{
			if ( controllers[i].Length > 0 )
				connectedController = Controller.PS4orOther; // We got a controller (PS4 or other)

			if ( controllers[i].ToLower( ).Contains( "xbox" ) ) // Is it an XBox controller?
			{
				connectedController = Controller.XBox;
				return;
			}
		}
	}
}
