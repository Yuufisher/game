#include <iostream>
#include <windows.h>
#include <conio.h>
#include <stdlib.h>
using namespace std;
int map[25][25];
int foodx=8, foody=8;
int maxx=24, maxy=24;
int face = -1,socore = 0;
string name;
bool sw = true;
void startui(){
	cout << "请输入名字:";
	cin >> name;
	system("cls");
	cout << endl << endl<<"\t\t"<<name<<"欢迎游玩";
	Sleep(1000);
	system("cls");
}
void setdesk() {
	for (int i = 0; i < 25; i++) {
		map[0][i] = 2;//2zhang ai
		map[24][i] = 2;
		map[i][0] = 2;
		map[i][24] = 2;
	}
}
void show() {
	Sleep(250);
	system("cls");
	for (int i = 0; i < 25; i++) {
		for (int j = 0; j < 25; j++) {
			if (i == foodx && j == foody)
				cout << "+";
			else if (map[i][j] == 0)
				cout << " ";
			else if (map[i][j]  == 2)
				cout << "#";
			else if (map[i][j] == 1)
				cout << "*";		
		}
		cout << endl;
	}
}
struct snake {
	snake* prior;
	int x;
	int y;
	snake* next;
};
class sk {
public:
	sk() {
		first = new snake;
		snake* s = new snake;
		s->x = 10; s->y = 10;
		map[s->x][s->y] = 1;
		first->next = s;
		s->prior = first;
		s->next = nullptr;
		tail = s;
	}
	void move(int face){
		while (!_kbhit()) {
			cout <<name <<"の分数：" << socore << endl;
			snake* s = new snake;//1 2 3 4 w s a d
			if (face == 1)
			{
				s->x = (first->next)->x - 1; s->y = (first->next)->y;
			}
			if (face == 2)
			{
				s->x = (first->next)->x + 1; s->y = (first->next)->y;
			}
			if (face == 3)
			{
				s->y = (first->next)->y - 1; s->x = (first->next)->x;
			}
			if (face == 4)
			{
				s->y = (first->next)->y + 1; s->x = (first->next)->x;
			}
			s->next = first->next;
			s->prior = first;
			(first->next)->prior = s;
			first->next = s;
			sw = true;
			check(s->x, s->y);
			map[s->x][s->y] = 1;
			if (sw) {
				map[tail->x][tail->y] = 0;
				snake* p = tail;
				tail = tail->prior;
				delete p;
			}
			show();
		}
	}
	void check(int x, int y) {
		if (x == foodx && y == foody) {
			sw = false;
			show();
			crefood();
			socore += 1;
		}
		if (x >= maxx || y >= maxy || y <= 0 || x <= 0) {
			cout << "gameover:edge!" << endl;
			system("pause");
		}
		if (map[x][y] == 1) {
			cout << "gameover:snakebody!" << endl;
			system("pause");
		}
	}
	void crefood() {
		foodx = rand() % 23 + 1;
		foody = rand() % 23 + 1;
	}
private:
	snake* first;
	snake* tail;
};
int main() {
	sk a;
	startui();
	setdesk();
	show();
	char ch;
	while (1) {
		ch = _getch();
		switch (ch) {
		case'w':{
			if (face != 2) { face = 1; a.move(face); } break;
		}
		case's': {
			if (face != 1) { face = 2; a.move(face); } break;
		}
		case'a': {
			if (face != 4) { face = 3; a.move(face); } break;
		}
		case'd': {
			if (face != 3) { face = 4; a.move(face); }break;
		}
		}
	}
	return 0;
}