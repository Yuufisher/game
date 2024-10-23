#include <iostream>
#include <graphics.h>
#include <easyx.h>
#include <conio.h>
#include <cmath>
#include <cstdlib>
#include <ctime>
#include <thread>
#include <stdio.h>
using namespace std;
int nums = 0;//分数
char str[1024] = { 0 };//类型转换
int timedown = 70;//处理时间(下落时间，可用于增加难度)
int shape = rand() % 7, form = rand() % 4;//形态与状态
int x = -1, y = 8;//start x y
int map[24][20] = { 0 };//地图数组
int minx = 24, maxy = 0, maxx =0;//记录移动区块中最大x
COLORREF randomColor() {
	// 生成3个随机数，范围为0到255
	int r = rand() % 256;
	int g = rand() % 256;
	int b = rand() % 256;
	// 合并成一个COLORREF类型的颜色值
	return RGB(r, g, b);
}
struct block {
	int block[4][4];//block paint
}space[7][4];//blcok type and block form
void setbk() {//生成背景板
	initgraph(700, 602);
	setbkcolor(BLACK);
	setlinecolor(GREEN);
	cleardevice();
	line(501, 0, 501, 601);//600*500  25 1 block
	line(0, 601, 501, 601);
	line(500, 300, 700, 300);
	outtextxy(550, 100, "分数:");
	outtextxy(530, 150, "下一个图形↓");
	outtextxy(600, 100, str);
	outtextxy(510, 320, "左右移动:A D");
	outtextxy(510, 350, "旋转:x");
	outtextxy(510, 380, "向下移动:S");
	outtextxy(510, 410, "暂停游戏:p");
	outtextxy(510, 440,"重新开始:r");
}
void Creblock() {//生成物块
	//t形
	for (int i = 0; i <= 2; i++)
		space[0][0].block[1][i] = 1;
	space[0][0].block[2][1] = 1;
	//l形
	for (int i = 0; i <= 3; i++)
		space[1][0].block[i][1] = 1;
	space[1][0].block[3][2] = 1;
	//反l形
	for (int i = 1; i <= 3; i++)
		space[2][0].block[i][2] = 1;
	space[2][0].block[3][1] = 1;
	//z形
	for (int i = 0; i <= 1; i++) {
		//z型
		space[3][0].block[1][i] = 1;
		space[3][0].block[2][i + 1] = 1;
		//反z形
		space[4][0].block[1][i + 1] = 1;
		space[4][0].block[2][i] = 1;
		//o形状
		space[5][0].block[1][i + 1] = 1;
		space[5][0].block[2][i + 1] = 1;
	}
	//i形
	for (int i = 0; i <= 3; i++)
		space[6][0].block[i][1] = 1;
	//转换形态
	int temp[4][4];//间接模子
	for (int shape = 0; shape < 7; shape++) {
		for (int form = 0; form < 3; form++) {
			for (int i = 0; i < 4; i++) {
				for (int j = 0; j < 4; j++) {
					temp[i][j] = space[shape][form].block[i][j];
				}
			}
			//将form进行旋转
			for (int i = 0; i < 4; i++) {
				for (int j = 0; j < 4; j++) {
					space[shape][form + 1].block[i][j] = temp[3 - j][i];
				}
			}
		}
	}
}
//去除与显示物块
void easyxshow_disappear(int x,int y ) {
	for (int i = 0; i < 4; i++) {
		for (int j = 0; j < 4; j++) {
			if (space[shape][form].block[i][j] == 1) {
				clearrectangle((y + j) * 25, (x + i) * 25, (y + j) * 25 + 25, (x + i) * 25 + 25);
			}
		}
	}
}
void easyxshow_blank(int x, int y) {
		for (int i = 0; i < 4; i++) {	
			for (int j = 0; j < 4; j++) {
				if (space[shape][form].block[i][j] == 1) {
						setfillcolor(WHITE);
						fillrectangle((y + j) * 25, (x + i) * 25, (y + j) * 25 + 25, (x + i) * 25 + 25);
						maxx = max(maxx, x+i);
				}
			}
		}
}
void paintmap(int marki) {//检测地图数组并生成固定物块
	for (int i = marki; i >= minx; i--) {
		for (int j = 0; j < 20; j++) {
			if (map[i][j] == 1)
				fillrectangle(j * 25, i * 25, j * 25 + 25, i * 25 + 25);
			else {
				//cout << y * 25 << " " << x * 25 << " " << y * 25 + 25 << " " << x * 25 + 25 << endl;
				clearrectangle(j * 25, i * 25, j * 25 + 25, i * 25 + 25);
			}
		}
	}
}
void clearmp() {//标记地图数组
	for (int i = 0; i < 4; i++) {
		for (int j = 0; j < 4; j++) {
			if (space[shape][form].block[i][j] == 1) {
				map[x + i][y + j] = 1;
				minx = min(minx, x + i);
			}
		}
	}
}
void mpclear(int marki) {//去行后处理
	for (int i =marki; i>minx; i--) {
		for (int j = 0; j < 20; j++) {
			swap(map[i][j], map[i-1][j]);
		}
	}
	paintmap(marki);
}
void gameover() {//游戏结束处理
	setfillcolor(GREEN);
	fillrectangle(290, 290, 450, 350);
	outtextxy(300, 300, "gameover");
	outtextxy(300, 330, "按r重新开始一局");
}
bool snew = 1;//阻断变量
void newround() {//主要链接函数
	x = -1; y = 8; maxx = 0; 
	snew = 1;
	for (int i = 0; i < 20; i++)
		if (map[0][i] == 1) {
			snew = 0;
			gameover();
			break;
		}
	if (snew) {
		for (int i = minx; i <= 23; i++) {
			bool cl = 1;
			for (int j = 0; j < 20; j++) {
				if (map[i][j] == 0) {
					cl = 0;
					break;
				}
			}
			if (cl) {
				nums += 100;
				sprintf_s(str, "%d", nums);
				outtextxy(600, 100, str);
				for (int k = 0; k < 20; k++) {
					//cout << map[i][k] <<"clear" << endl;
					map[i][k] = 0;
				}
				mpclear(i);
			}
		}
	}
}
int tempf, temps;//临时变量
void shownext() {//显示下一个物块
	int snx = 7, sny = 22;
	setfillcolor(BLACK);
	solidrectangle(510,175,700,275);
	for (int i = 0; i < 4; i++) {
		for (int j = 0; j < 4; j++) {
			if (space[temps][tempf].block[i][j] == 1) {
				setfillcolor(WHITE);
				fillrectangle((sny + j) * 25, (snx + i) * 25, (sny + j) * 25 + 25, (snx + i) * 25 + 25);
			}
		}
	}
}
void sstoptonext() {//中点站
	//setfillcolor(randomColor());
	newround();
	if (snew) {
		srand(time(NULL));
		form = tempf;
		shape = temps;
		easyxshow_blank(x, y);
		tempf = rand() % 4;
		temps = rand() % 7;
		shownext();
	}
}
bool allcheck(int x, int y) {//障碍检测
	if (maxx == 23) {
		clearmp();
		sstoptonext();
		return 0;
	}
	for (int i = 0; i < 4; i++)
		for (int j = 0; j < 4; j++) {
			if (!snew)
				return 0;
			if (space[shape][form].block[i][j] == 1) {
				if (map[x + i][y + j] == 1) {
					clearmp();
					Sleep(100);
					sstoptonext();
					return 0;

				}
				if ((y + j) * 25 + 25 > 500 || (y + j) * 25 < 0) {
					//cout << (x+i )* 25+25 << "x+i" << endl;
					return 0;
				}
			}
		}
	return 1;
}
void trancheck(int n,int x,int y) {//旋转障碍检测
	int tmp = form;
	n++;
	if (n >= 4)
		n = 0;
	form = n;
	if (allcheck(x, y)) {
		cout << "转！" << endl;
		form = tmp;
		easyxshow_disappear(x, y);
		form = n;
		easyxshow_blank(x, y);
	}
	else {
		cout << "不准转！"<<endl;
		form=tmp;
	}
}
void show() {//显示函数
	Sleep(600);                                           //printf x
	easyxshow_disappear(x-1, y);
	easyxshow_blank(x, y);
	//checkh();
}
void movedown() {//自动移动
	while (!_kbhit()) {
		//Sleep(1000);
		int m = x + 1;
		if (allcheck(m, y)) {
			x++;
		}
		show();
	}
}
int main() {//控制
	Creblock();
	sprintf_s(str, "%d", nums);
	setbk();
	srand(time(NULL));
	shape = rand() % 7, form = rand() % 4;
	//Sleep(200);
	tempf = rand() % 4; temps = rand() % 7;
	easyxshow_blank(x, y);
	shownext();
	while (true) {
		char ch;
		ch = _getch();
		switch (ch) {
		case'a': {
			if (allcheck(x, y - 1)) {
				cout << "←" << endl;
				Sleep(timedown);
				easyxshow_disappear(x, y);
				y--;
				easyxshow_blank(x, y);
			}
			movedown();
			break;
		}
		case'd':{
			if (allcheck(x, y + 1)) {
				cout << "→" << endl;
				Sleep(timedown);
				easyxshow_disappear(x, y);
				y++;
				easyxshow_blank(x, y);
			}
			movedown();
			break;
		}
		case's': {
			if (allcheck(x + 1, y)) {
				cout << "↓" << endl;
				Sleep(timedown - 20);
				easyxshow_disappear(x, y);
				x++;
				easyxshow_blank(x, y);
			}
			movedown();
			break;
		}
		case'x': {
			trancheck(form, x, y);
			movedown();
			break;
		}
		case'r': {
			system("cls");
			nums = 0;
			sprintf_s(str, "%d", nums);
			outtextxy(600, 100, str);
			memset(map, 0, sizeof(map));
			shape = rand() % 7, form = rand() % 4;
			x = -1; y = 8;
			cleardevice();
			setbk();
			easyxshow_blank(x, y);
			newround();
			shownext();
			break;
		}
		case'p': {
			for (int i = 0; i < 24; i++) {
				for (int j = 0; j < 20; j++)
					cout << map[i][j] << " ";
				cout << endl;
			}
			system("pause");
			break;
		}
		}
	}
}