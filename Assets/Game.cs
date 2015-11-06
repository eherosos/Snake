using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using System.Collections;

public class Game : MonoBehaviour 
{
	public int LENGTH ;
	public Sprite texture ;
	public GameObject gam,gam2 ;
	public GridLayoutGroup gridPanel ;
	
	private Image pad ;
	private Image[,] pos;
	public List<int> playerX ;
	public List<int> playerY ;
	private int x,y,lx,ly,l2x,l2y ;
	
	private enum State
	{
		up,down,right,left,end
	}
	
	private State state ;
	
	void Start ()
	{
		x = 1;
		y = 1;
		pos = new Image[LENGTH,LENGTH] ;
		SetUpScene ();
		state = State.up;
		InvokeRepeating ("Tail",2f,2f);
		InvokeRepeating ("StateGame",0f,0.5f);
	}
	
	void Tail ()
	{
		playerX.Add (playerX [playerX.Count - 1]);
		playerY.Add (playerY [playerY.Count - 1]);
	}
	
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.UpArrow) && state != State.end)
		{
			state = State.up;
		}
		
		if(Input.GetKeyDown(KeyCode.DownArrow) && state != State.end)
		{
			state = State.down;
		}
		
		if(Input.GetKeyDown(KeyCode.LeftArrow) && state != State.end)
		{
			state = State.left;
		}
		
		if(Input.GetKeyDown(KeyCode.RightArrow) && state != State.end)
		{
			state = State.right;
		}
		
	}
	
	void StateGame ()
	{
		switch (state) 
		{
			case State.up :
				Up() ;
				break;
			case State.right :
				Right() ;
				break;
			case State.left :
				Left() ;
				break;
			case State.down :
				Down() ;
				break;
			case State.end :
				Destroy(gam) ;
				Destroy(gam2) ;
				break;
		}
	}
	
	void SetUpScene ()
	{
		gridPanel.childAlignment = TextAnchor.MiddleCenter;
		gridPanel.cellSize = new Vector2 (Screen.width/LENGTH,Screen.height/LENGTH);
		gridPanel.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
		gridPanel.constraintCount = LENGTH;
		
		for(int i = 0 ; i < LENGTH ; i++)
		{
			for(int l = 0 ; l < LENGTH ; l++)
			{
				GameObject obj = new GameObject("[" + i + ", " + l + "]");
				pad = obj.AddComponent<Image>();
				pad.sprite = texture ;
				pos[i,l] = pad ;
				pad.transform.SetParent(gridPanel.transform);
			}
		}
	}
	
	#region U
	void Up ()
	{
		lx = playerX[0] ;
		ly = playerY[0] ;
		l2x = lx;
		l2y = ly;
		x--;
		if(x < 0 )
		{
			x = LENGTH - 1  ;
			pos[x,y].color = Color.green ;
		}
		else
		{
			pos[x,y].color = Color.green ;
		}
		playerX [0] = x;
		playerY [0] = y;
		CheckGame ();
	}
	#endregion
	
	#region D
	void Down ()
	{
		lx = playerX[0] ;
		ly = playerY[0] ;
		l2x = lx;
		l2y = ly;
		x++;
		if(x > LENGTH - 1)
		{
			x = 0 ;
			pos[x,y].color = Color.green ;
		}
		else
		{
			pos[x,y].color = Color.green ;
		}

		playerX [0] = x;
		playerY [0] = y;
		CheckGame ();
	}
	#endregion
	
	#region R
	void Right ()
	{
		lx = playerX[0] ;
		ly = playerY[0] ;
//		x = lx;
//		y = ly;
		l2x = lx;
		l2y = ly;
		y++;
		if(y > LENGTH - 1 )
		{
			x = 0 ;
			pos[x,y].color = Color.green ;
		}
		else
		{
			pos[x,y].color = Color.green ;
		}
		playerX [0] = x;
		playerY [0] = y;
		CheckGame ();
	}
	#endregion
	
	#region L
	void Left ()
	{
		lx = playerX[0] ;
		ly = playerY[0] ;
//		x = lx;
//		y = ly;
		l2x = lx;
		l2y = ly;
		y--;
		if(y < 0 )
		{
			y = LENGTH - 1 ;
			pos[x,y].color = Color.green ;
		}
		else
		{
			pos[x,y].color = Color.green ;
		}
		playerX [0] = x;
		playerY [0] = y;
		CheckGame ();
	}
	#endregion
	
	void CheckGame ()
	{
		print (playerX.Count);
		for(int i = 1 ; i < playerX.Count ; i++)
		{
			if(playerX[i-1] == playerX[i] && playerY[i-1] == playerY[i])
			{
				break;
			}
			else
			{
				if(playerX[0] == playerX[i] && playerY[0] == playerY[i])
				{
					state = State.end ;
				}
				lx = playerX[i] ;
				ly = playerY[i] ;
				playerX [i] = l2x;
				playerY [i] = l2y;
				l2x = lx;
				l2y = ly;
			}
			pos[playerX[i],playerY[i]].color = Color.green ;
		}
		
		int px = playerX[playerX.Count -1];
		int py = playerY[playerY.Count -1];
		
		pos[px, py].color = Color.white ;
		
	}
	
}
