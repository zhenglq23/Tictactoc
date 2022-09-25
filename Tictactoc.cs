using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tictactoc : MonoBehaviour
{
    // 声明变量,用于控制游戏进程
    private int[,] board = new int[3, 3];
    private int mode = 1; // 标识执子玩家，1代表当前由玩家1执子，2则为玩家2
    private int state = 0; // 标识游戏状态，0表示开始界面，1代表游戏进行中，2代表结算界面
    private int result = 1; // 标识游戏结果，1代表玩家1胜出，2代表玩家2胜出，0代表平局
    private int turn = 0; // 标识轮次
    
    // 声明变量，用于提示玩家
    private string s1 = "玩家1执子";
    private string s2 = "玩家2执子";
    private string r1 = "玩家1胜出";
    private string r2 = "玩家2胜出";
    private string r0 = "平局";
    
    // 实体
    public Texture2D Background;
    public Texture2D X;
    public Texture2D O;
    public Texture2D _;

    // 设置棋盘边界
    private int board_left = 175;
    private int board_top = 60;
    private int board_size = 80;

    // 声明字体风格
    GUIStyle small = new GUIStyle(); // 小字体
    GUIStyle big = new GUIStyle(); // 大字体

    // 游戏初始化
    void Init()
    {
        for(int i=0; i<3; i++)
        {
            for(int j=0; j<3; j++)
            {
                board[i, j] = 0;
            }
        }
        turn = 0;
    }

    // 落子操作
    void Put(int i, int j)
    {
        board[i,j] = mode;
        turn++;
        mode = (mode % 2) + 1;
    }

    // 判断游戏是否结束及其结果，返回-1则游戏继续，返回0，1，2则为结果
    int Judge()
    {   
        for(int i=0; i<3; i++)
        {
            // 横向检测
            if(board[i,0]!=0 && board[i,0]==board[i,1] && board[i,1]==board[i,2])
                return board[i,0];
            // 竖向检测
            if(board[0,i]!=0 && board[0,i]==board[1,i] && board[1,i]==board[2,i])
                return board[0,i];
        }
        // 斜向检测
        if(board[0,0]!=0 && board[0,0]==board[1,1] && board[1,1]==board[2,2])
            return board[0,0];
        if(board[1,1]!=0 && board[0,2]==board[1,1] && board[1,1]==board[2,0])
            return board[1,1];
        if (turn==9)
            return 0;
        return -1;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 小字体初始化
        small.normal.textColor = Color.black;
        small.normal.background = null;
        small.fontSize = 20;

        // 大字体初始化
        big.normal.textColor = Color.black;
        big.normal.background = null;
        big.fontSize = 40;

        // 游戏开始界面
        state = 0;
    }

    // OnGUI is called once per frame
    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 800, 1000), Background);

        if (state==0) // 游戏开始界面
        {
            GUI.Label(new Rect(175, 10, 100, 50), "井字棋小游戏", big);
            if (GUI.Button(new Rect(235, 200, 100, 75), "开始游戏")){
                Init();
                state = 1;
            }
        }
        else if(state==1) //游戏运行中
        {
            // 提示玩家落子语句
            if (mode==1)
                GUI.Label(new Rect(175, 10, 100, 50), s1 , small);
            else
                GUI.Label(new Rect(175, 10, 100, 50), s2 , small);
            // 判断哪个按钮被点击, 落子
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 0)
                    {
                        if (GUI.Button(new Rect(board_left + i * board_size, board_top + j * board_size, board_size, board_size), _))
                        {
                            Put(i, j);
                        }
                    }
                    if (board[i, j] == 1)
                    {
                        GUI.Button(new Rect(board_left + i * board_size, board_top + j * board_size, board_size, board_size), X);
                    }
                    if (board[i, j] == 2)
                    {
                        GUI.Button(new Rect(board_left + i * board_size, board_top + j * board_size, board_size, board_size), O);
                    }
                }
            }
            // 判断对局是否结束
            result = Judge();
            if(result!=-1)
                state = 2;
        }
        else if (state==2)//结算界面
        {
            if (result==1)
                GUI.Label(new Rect(175, 10, 100, 50), r1, big);
            else if (result==2)
                GUI.Label(new Rect(175, 10, 100, 50), r2, big);    
            else
                GUI.Label(new Rect(175, 10, 100, 50), r0, big);
            if (GUI.Button(new Rect(235, 200, 100, 75), "重新游戏")){
                Init();
                state = 1;
            }
        }
    }
}
