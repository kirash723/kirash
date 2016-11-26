using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TouchController : MonoBehaviour {
	private const int MASS_WID = 3;    //マスの数（幅）
	private const int MASS_HEI = 3;    //マスの数（高さ）
	private const int WALL = 2;			//両端の壁

	public GameObject wall;

	private float instanceX = 0;
	private float instanceY = 0;

	void Awake (){
		addWall ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			
			if (Physics.Raycast (ray, out hit)) {
				GameObject obj = hit.collider.gameObject;
				for (int i = 1; i < MASS_WID + 1; i++) {
					for (int j = 1; j < MASS_HEI + 1; j++) {
						if (obj.name == ( "wall" + j + ","+ i +"(Clone)")) {
							Open (j, i);
						}
					}
				}
			}
		}
	}

	void addWall ( ) {
		for (int i = 1; i < MASS_WID + 1; i++) {
			for (int j = 1; j < MASS_HEI + 1; j++) {
				wall.name = ( "wall" + j + ","+ i );
				Instantiate ( wall, new Vector3 (instanceX, instanceY, 0), Quaternion.identity);
				instanceX = instanceX + 2;
			}
			instanceX = 0;
			instanceY = instanceY + 2;
		}
	}

	void Open(int x, int y )
	{
		if (x == 0 || x == MASS_WID + 1 ||
		    y == 0 || y == MASS_HEI + 1) {
			return;
		}
		if (StageLoader.isOpen [x, y]){
			return;
		}

		StageLoader.isOpen [x, y] = true;

		GameObject wall = GameObject.Find (( "wall" + x + ","+ y +"(Clone)"));
		wall.SetActive (false);
		if (StageLoader.isMine[x,y])
		{
			//爆弾を引いたとき
			Debug.Log("爆弾を弾きました");	
			//爆弾を引いたときの処理
			Debug.Log("Game Over");
			SceneManager.LoadScene ("result");
		}

		if (StageLoader.isGet [x, y]) {
			//出口を引いた時
			Debug.Log ("Game Clear");
			SceneManager.LoadScene ("Main");
		}
		


		//クリア判定
		ClearCheck();
	}

	void ClearCheck()
	{
		if (StageLoader.isGameOver)
			return;

		int cnt = 0;
		for (int i = 1; i < MASS_WID + 1; i++)
		{
			for (int j = 1; j < MASS_HEI + 1; j++)
			{
				if (StageLoader.isOpen[i, j])
					cnt++;
			}
		}

	}
}
