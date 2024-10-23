#include <iostream>
#include <windows.h>
#include <conio.h>
using namespace std;
int ppoint = 10;//10 - 3 = 7
int face = 1;
int destination[] = { 9,8 };
int endnums = 2;
char map[] =
"######\n\
#..p #\n\
#00  #\n\
#    #\n\
######\n";
bool Check_Gameover() {
	for (int i = 0; i < endnums; i++) {
		if (map[destination[i]] != 'o')
			return 0;
	}
	return 1;
}
bool Checkmo() {

	if (map[ppoint] == '#') {
		return 0;
	}
	if (map[ppoint] == '0'|| map[ppoint] == 'o') {
		if (face == 1) {
			if (ppoint - 7 >= 0 && map[ppoint - 7] != '#' && map[ppoint - 7] != '0' && map[ppoint - 7] != 'o') {
				if (map[ppoint - 7] == '.') {
					map[ppoint - 7] = 'o';
					return 1;
				}
				map[ppoint - 7] = '0';
				return 1;
			}
			else
				return 0;
		}        
		if (face == 2) {
			if (ppoint + 7 <= 36 && map[ppoint + 7] != '#' && map[ppoint + 7] != '0' && map[ppoint + 7] != 'o') {
				if (map[ppoint + 7] == '.') {
					map[ppoint + 7] = 'o';
					return 1;
				}
				map[ppoint + 7] = '0';
				return 1;
			}
			else
				return 0;
		}
		if (face == 3) {
			if (ppoint - 1 >= 0 && map[ppoint - 1] != '#' && map[ppoint - 1] != '0' && map[ppoint - 1] != 'o') {
				if (map[ppoint -1] == '.') {
					map[ppoint -1] = 'o';
					return 1;
				}
				map[ppoint - 1] = '0';
				return 1;
			}
			else
				return 0;
		}
		if (face == 4) {
			if (ppoint + 1 <= 36 && map[ppoint + 1] != '#' && map[ppoint + 1] != '0'&& map[ppoint + 1]!='o') {
				if (map[ppoint + 1] == '.') {
					map[ppoint +1] = 'o';
					return 1;
				}
				map[ppoint + 1] = '0';
				return 1;
			}
			else
				return 0;
		}
	}
	return 1;
}
int fp = 1000 / 60;
void Drawmap() {
	system("cls");
	for (int i = 0; i < endnums; i++) {
		if (map[destination[i]] != 'p' && map[destination[i]] != 'o') {
			map[destination[i]] = '.';
		}
	}
	cout << map;
	cout << "wasd控制移动，将0推到.的位置";
	Sleep(fp);//fps
}
void Updata() {
	char ch;
	switch (ch=_getch()) {
	case'w': {
		face = 1;
		if(ppoint -= 6>=0)
			ppoint-=6;
		if (!Checkmo()) {
			ppoint+=7;
		}
		else {
			map[ppoint + 7] = ' ';
			map[ppoint] = 'p';
		}
		break;
	}
	case's': {
		face = 2;
		if (ppoint += 6 <= 35)
			ppoint += 6;
		if (!Checkmo()) {
			ppoint -= 7;
		}
		else if(ppoint-7>=0) {
			map[ppoint - 7] = ' ';
			map[ppoint] = 'p';
		}
		break;
	}
	case'a': {
		face = 3;
		ppoint--;
		if (!Checkmo()) {
			ppoint++;
		}
		else {
			map[ppoint+1] = ' ';
			map[ppoint] = 'p';
		}
		break;
	}
	case'd': {
		face = 4;
		ppoint++;
		if (!Checkmo()) {
			ppoint--;
		}
		else {
			map[ppoint-1] = ' ';
			map[ppoint] = 'p';
		}
		break;
	}
	}
}
int main() {
	while (true) {
		Drawmap();
		if (Check_Gameover()) {
			cout << "\nyou win!";
			break;
		}
		Updata();
	}
	return 0;
}