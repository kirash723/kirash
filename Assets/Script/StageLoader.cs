using UnityEngine;
using System.Collections;

public class StageLoader : MonoBehaviour {

	public static int MINE_NUM = 1;    //爆弾の数
	public static int MINE_GET = 1;    //あたりの数
	private const int MASS_WID = 3;    //マスの数（幅）
	private const int MASS_HEI = 3;    //マスの数（高さ）
	private const int WALL = 2;			//両端の壁

	public static bool[,] isOpen = new bool[MASS_WID + WALL, MASS_HEI + WALL];
	public static bool[,] isMine = new bool[MASS_WID + WALL, MASS_HEI + WALL];
	public static bool[,] isGet = new bool[MASS_WID + WALL, MASS_HEI + WALL]; 
	public static int[,] mineNum = new int[MASS_WID + WALL, MASS_HEI + WALL];

	public static bool isGameOver = false;
	private bool isInit = false;


    public GameObject m_floor;
	public GameObject Bomm;
	public GameObject Get;
	public GameObject Cube0;
	public GameObject Cube1;
	public GameObject Cube2;
	public GameObject Cube3;
	public GameObject Cube4;
	public GameObject Cube5;
	public GameObject Cube6;
	public GameObject Cube7;
	public GameObject Cube8;

    private float instanceX = 0;
	private float instanceY = 0;

    void Awake (){
		addCube ();
    }

    // Use this for initialization
    void Start () {



    }

    // Update is called once per frame
    void Update () {
		for (int i = 1; i < MASS_WID + 1; i++) {
				for (int j = 1; j < MASS_HEI + 1; j++) {
				
			}
		}
    }

	/// <summary>
	/// マスの初期化
	/// </summary>
	public void Reset()
	{
		//全てのマスを開いていない状態にする

		for (int i = 0; i < MASS_WID + WALL; i++)
		{
			for (int j = 0; j < MASS_HEI + WALL; j++)
			{
				isOpen[i, j] = false;
				isMine[i, j] = false;
				mineNum[i, j] = 0;
				isInit = false;
				isGameOver = false;
				Application.LoadLevel("Main");
			}
		}
	}

	/// <summary>
	/// 爆弾の配置
	/// </summary>
	/// <param name="_x"></param>
	/// <param name="_y"></param>
	void Init(int _x,int _y)
	{
		do
		{
			isInit = true;

			for (int i = 0; i < MASS_WID + WALL; i++)
			{
				for (int j = 0; j < MASS_HEI + WALL; j++)
				{
					isOpen[i, j] = false;
					isMine[i, j] = false;
					mineNum[i, j] = 0;
				}
			}


			for (int i = 0; i < MINE_NUM; i++)
			{
				int x, y = 0;

					x = Random.Range(1, MASS_WID);
					y = Random.Range(1, MASS_HEI);
				 

				isMine[ x , y ] = true;

				while ( ) {

					int x1, y1  = 0;

					x1 = Random.Range(1, MASS_WID);
					y1 = Random.Range(1, MASS_HEI);

					if (  ){


					}
				
				}

			}
		} while (!isInit);

	}

	public void addCube() {
		for (int i = 1; i < MASS_WID + 1; i++) {
			for (int j = 1; j < MASS_HEI + 1; j++) {
				
				if (!isInit) {
					Init (j, i);
				}

				if (isMine [j, i]) {
					Instantiate (Bomm, new Vector3 (instanceX, instanceY, 1), Quaternion.identity);
					Debug.Log("mine " + i.ToString() + " " + j.ToString());
				} else if (isGet [j, i]) {
					Instantiate (Get, new Vector3 (instanceX, instanceY, 1), Quaternion.identity);

				}else if (mineNum [j, i] == 0) {
					Instantiate (Cube0, new Vector3 (instanceX, instanceY, 1), Quaternion.identity);
				} else if (mineNum [j, i] == 1) {
					Instantiate (Cube1, new Vector3 (instanceX, instanceY, 1), Quaternion.identity);
				} else if (mineNum [j, i] == 2) {
					Instantiate (Cube2, new Vector3 (instanceX, instanceY, 1), Quaternion.identity);
				} else if (mineNum [j, i] == 3) {
					Instantiate (Cube3, new Vector3 (instanceX, instanceY, 1), Quaternion.identity);
				} else if (mineNum [j, i] == 4) {
					Instantiate (Cube4, new Vector3 (instanceX, instanceY, 1), Quaternion.identity);
				} else if (mineNum [j, i] == 5) {
					Instantiate (Cube5, new Vector3 (instanceX, instanceY, 1), Quaternion.identity);
				} else if (mineNum [j, i] == 6) {
					Instantiate (Cube6, new Vector3 (instanceX, instanceY, 1), Quaternion.identity);
				} else if (mineNum [j, i] == 7) {
					Instantiate (Cube7, new Vector3 (instanceX, instanceY, 1), Quaternion.identity);
				} else if (mineNum [j, i] == 8) {
					Instantiate (Cube8, new Vector3 (instanceX, instanceY, 1), Quaternion.identity);
				} 
					



					
				instanceX = instanceX + 2;
			}
			instanceX = 0;
			instanceY = instanceY + 2;
		}
	}
}