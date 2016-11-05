using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// エディタ拡張でマインスイーパーをやる
/// </summary>
public class MineSweeperEditor : EditorWindow{

	private const int MINE_NUM = 15;    //爆弾の数
	private const int MASS_WID = 10;    //マスの数（幅）
	private const int MASS_HEI = 10;    //マスの数（高さ）
	private const int WALL = 2;			//両端の壁
	private const float MASS_SIZE = 25.0f;

	private bool[,] isOpen = new bool[MASS_WID + WALL, MASS_HEI + WALL];
	private bool[,] isMine = new bool[MASS_WID + WALL, MASS_HEI + WALL];
	private int[,] mineNum = new int[MASS_WID + WALL, MASS_HEI + WALL];

	private bool isGameOver = false;
	private bool isInit = false;


	/// <summary>
	/// ウィンドウを開く
	/// </summary>
	[MenuItem("Games/MineSweeper")]
	static void OpenWindow()
	{
		MineSweeperEditor window = GetWindow<MineSweeperEditor>("MineSweeper");
		window.position = new Rect(300, 50, 295, 320);
		window.Show();
	}


	/// <summary>
	/// 表示処理
	/// </summary>
	void OnGUI()
	{
		EditorGUILayout.Space();

		if (GUILayout.Button("Reset"))
		{
			Reset();
		}
		EditorGUILayout.Space();

		for (int i = 1; i < MASS_WID + 1; i++)
		{
			EditorGUILayout.BeginHorizontal();
			for (int j = 1; j < MASS_HEI + 1; j++)
			{
				GUIContent style = new GUIContent();

				if (isOpen[j, i])
				{
					if (isMine[j, i])
						style.text = "爆";
					else
					{
						style.text = mineNum[j, i] == 0 ? "" : mineNum[j, i].ToString();
					}
					GUI.color = new Color(0.8f, 0.8f, 0.8f);
				}
				else
				{
					style.text = (isGameOver && isMine[j, i]) ? "爆" : "";
					GUI.color = (isGameOver && isMine[j, i]) ? Color.red : Color.white;

				}

				if (GUILayout.Button(style, GUILayout.Width(MASS_SIZE), GUILayout.Height(MASS_SIZE)))
				{
					if (isGameOver)
						return;

					
					if (!isInit)
						Init(j, i);
			
					Open(j, i);
				}

			}
			EditorGUILayout.EndHorizontal();
		}

	}
	
	/// <summary>
	/// マスの初期化
	/// </summary>
	void Reset()
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
			Debug.Log("初期化");

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
				do
				{
					x = Random.Range(1, MASS_WID);
					y = Random.Range(1, MASS_HEI);
				} while (isMine[x, y] ||
				(x == _x + 1 && y == _y + 1) ||
				(x == _x && y == _y + 1) ||
				(x == _x - 1 && y == _y + 1) ||
				(x == _x + 1 && y == _y) ||
				(x == _x - 1 && y == _y) ||
				(x == _x + 1 && y == _y - 1) ||
				(x == _x && y == _y - 1) ||
				(x == _x + 1 && y == _y - 1));

				isMine[x, y] = true;
				mineNum[x + 1, y + 1]++;
				mineNum[x + 1, y]++;
				mineNum[x + 1, y - 1]++;
				mineNum[x, y + 1]++;
				mineNum[x, y - 1]++;
				mineNum[x - 1, y + 1]++;
				mineNum[x - 1, y]++;
				mineNum[x - 1, y - 1]++;
			}

			//最初に押した所が爆弾だったらもう一度初期化
			//最初に押した所が0じゃないときもう一度初期化
			if (isMine[_x, _y] || mineNum[_x,_y]!=0)
			{
				isInit = false;
			}

		} while (!isInit);

	}

	/// <summary>
	/// マスを開く
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	void Open(int x, int y)
	{
		if (x == 0 || x == MASS_WID + 1 ||
			y == 0 || y == MASS_HEI + 1)
			return;

		if (isOpen[x, y])
			return;

		isOpen[x, y] = true;

		if (isMine[x, y])
		{
			//爆弾を引いたとき

			//メッセージボックス表示
			EditorUtility.DisplayDialog("GameOver", "You Lost!", "OK");
			isGameOver = true;
		}
		else if (mineNum[x, y] == 0)
		{
			//開いたマスが0の時
			//全方位のマスを開ける

			Open(x + 1, y + 1);
			Open(x + 1, y);
			Open(x + 1, y - 1);
			Open(x, y + 1);
			Open(x, y - 1);
			Open(x - 1, y + 1);
			Open(x - 1, y);
			Open(x - 1, y - 1);

		}

		//クリア判定
		ClearCheck();
	}

	/// <summary>
	/// クリアチェック
	/// </summary>
	void ClearCheck()
	{
		if (isGameOver)
			return;

		int cnt = 0;
		for (int i = 1; i < MASS_WID + 1; i++)
		{
			for (int j = 1; j < MASS_HEI + 1; j++)
			{
				if (isOpen[i, j])
					cnt++;
			}
		}

		//爆弾以外のマスを全て開いたとき
		if (cnt >= (MASS_WID * MASS_HEI) - MINE_NUM)
		{
			//メッセージボックス表示
			EditorUtility.DisplayDialog("GameClear!", "You Win!", "OK");
			isGameOver = true;
		}
	}
}