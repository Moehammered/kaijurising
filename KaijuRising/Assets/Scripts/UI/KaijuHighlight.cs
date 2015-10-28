using UnityEngine;
using System.Collections;

public class KaijuHighlight : MonoBehaviour
{
	private GameObject _selectedKaiju;
	
	public GameObject selectedKaiju
	{
		get
		{
			return _selectedKaiju;
		}
		set
		{
			dimLightSelected();
			_selectedKaiju = value;
			lightSelected();
		}
	}


	public string lightLayerName;

	public void hilightKaiju(GameObject kaiju)
	{
		//Debug.Log(kaiju.name);
		selectedKaiju = kaiju;
		//.play
	}

	void dimLightSelected()
	{
		if(selectedKaiju == null)
			return;

		Renderer[] childRends = selectedKaiju.GetComponentsInChildren<Renderer>();
		foreach(Renderer r in childRends)
		{
			r.gameObject.layer = LayerMask.NameToLayer("Default");
		}
		selectedKaiju.GetComponent<Animator>().SetBool("KaijuClicked",false);
	}

	void lightSelected()
	{
		if(_selectedKaiju == null)
			return;

		Renderer[] childRends = selectedKaiju.GetComponentsInChildren<Renderer>();
		foreach(Renderer r in childRends)
		{
			r.gameObject.layer =  LayerMask.NameToLayer(lightLayerName);
		}
		selectedKaiju.GetComponent<Animator>().SetBool("KaijuClicked",true);
	}
}
