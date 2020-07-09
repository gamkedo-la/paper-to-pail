/**
 * Description: Sets a random quote on enabled.
 * Authors: Kornel
 * Copyright: © 2020 Kornel. All rights reserved. For license see: 'LICENSE.txt'
 **/

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Quote : MonoBehaviour
{
	[SerializeField] private Text _label = null;
	[SerializeField] private string[] _quotes = null;

	void Start( )
	{
		Assert.IsNotNull( _label, $"Please assign <b>{nameof( _label )}</b> field on <b>{GetType( ).Name}</b> script on <b>{name}</b> object" );
		Assert.AreNotEqual( _quotes.Length, 0, $"Please assign <b>{nameof( _quotes )}</b> field on <b>{GetType( ).Name}</b> script on <b>{name}</b> object" );
	}

	void OnEnable( )
	{
		_label.text = _quotes[Random.Range( 0, _quotes.Length )];
	}
}
