import os
import time
import msvcrt
import random

def logo():
    print('   ,odOO"bo,   ')
    print(" ,dOOOP'dOOOb, ")
    print(",O3OP'dOO3OO33,")
    print('P",ad33O333O3Ob')
    print('?833O338333P",d')
    print("`88383838P,d38'")
    print(" `Y8888P,d88P' ")
    print('   `"?8,8P""   ')  
    time.sleep(5)
    os.system("cls")

logo()

r,t = 0,0
ox = random.randint(1,6)
oy = random.randint(10,20)
while(True):
    L = [
["_","_","_","_","_","_","_","_","_","_","_","_","_","0",":","0","_","_","_","_","_","_","_","_","_","_","_","_","_","_"],
[".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","."],
[".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","|"],
[".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","|"],
["|",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","|"],
["|",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","."],
["|",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","."],
[".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","."],
["_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_"]
]

    for i in range(1,len(L)-1):
        L[i][14] = "#"


    a =5
    b = 3

    x = 1
    y = 1

    os.system("cls")
    if(max(r,t) == 5):
        if r == max(r,t):
            print("       ***MIRO Won***       ")
        else:
            print("       ***CPU  Won***       ")
        print    ("       Press N to play         ")
        r,t = 0,0
        L[0][13] = "0"
        L[0][15] = "0"
    else: 
        L[ox][oy] = 'O'
        L[0][15] = str(t)
        L[0][13] = str(r)
        for i in L:
            s = "".join(i)
            print(s)
        print    ("       Press N to play        ")

    L[ox][oy] = '.'
    ox = random.randint(1,6)
    oy = random.randint(10,20)
    j = input()
    if(j == "n"):
        os.system("cls")
        while(True):
            L[ox][oy] = 'O'
            key_hit = msvcrt.kbhit()
            
            '''if key_hit:
                chh = msvcrt.getch().decode("utf-8")

                if chh == 'm' and b < len(L) - 2:
                    b += 1
                    L[b + 1][-1] = "|"
                    L[b][0]      = "|"
                    L[b - 2][-1] = "."

                elif chh == 'p' and b > 2:
                    b -= 1
                    L[b - 1][-1] = "|"
                    L[b][0]      = "|"
                    L[b + 2][-1] = "." '''

            if key_hit:
                ch = msvcrt.getch().decode("utf-8")
                
                if ch == 'q' and a < len(L) - 2:
                    a += 1
                    L[a + 1][0] = "|"
                    L[a][0]     = "|"
                    L[a - 2][0] = "."
                elif ch == 'a' and a > 2:
                    a -= 1
                    L[a - 1][0] = "|"
                    L[a][0]     = "|"
                    L[a + 2][0] = "."

            for i in L:
                s = "".join(i)
                print(s)
            print("©Miro_Jlassi")
            L[ox][oy] = '.'

            ox += x
            oy += y

            if(ox == 7):
                x = -1
            if(ox == 1):
                x = 1
            if(oy == 28):
                if(ox not in [b,b+1,b-1]):
                    r+=1
                    L[0][13] = str(r)
                    break
                y = -1
            if(oy == 1):
                if(ox not in [a,a+1,a-1]):
                    t+=1
                    L[0][15] = str(t)
                    break
                y = 1


            for i in range(1,len(L)-1):
                L[i][14] = "#"

            time.sleep(0.1)
            os.system("cls")



