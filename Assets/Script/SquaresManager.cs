using UnityEngine;
using System.Collections;

public class SquaresManager : MonoBehaviour {
	[SerializeField] SpriteRenderer selectObject;
	[SerializeField] SpriteRenderer bombObject;
	bool selectActive = false;

	void Awake() {
		if(selectObject != null && bombObject != null){
			selectObject.gameObject.SetActive(false);
			bombObject.gameObject.SetActive(false);
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			var hit =  Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if(hit != null){
				Debug.Log("Touch Player.");
				selectActive = true;
				selectObject.gameObject.SetActive(selectActive);	///< add here			
			}
		}
	}

	public void InitSelectObject(GameObject SquareObject){
		//if(gameLogicObjectName.name == "GameLogic"){
			selectActive = false;
			selectObject.gameObject.SetActive(selectActive);
		//}
	}

	public void SetBomb(){
		selectObject.gameObject.SetActive(false);
		bombObject.gameObject.SetActive(true);

	}
}
