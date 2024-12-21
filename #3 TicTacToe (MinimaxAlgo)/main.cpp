#include <bits/stdc++.h>
using namespace std;
// ✖ 🟣
void gridVisualiserV2(vector<long long> v){
    for(int i = 0;i<9;i++){
        if(v[i] == 1) cout<<"✖";
        if(v[i] == 2) cout<<"🟣";
        if(v[i] == 3) cout<<"➖";
        if(i == 2 || i == 5 || i == 8){
            cout<<"\n";
        }else{
            cout<<"|";
        }
    }
    cout<<"\n"<< std::flush;;
}

void gridVisualiser(vector<long long> v){
    for(int i = 0;i<9;i++){
        if(v[i] == 1) cout<<" X ";
        if(v[i] == 2) cout<<" O ";
        if(v[i] == 3) cout<<" - ";
        if(i == 2 || i == 5 || i == 8){
            cout<<"\n";
        }else{
            cout<<"|";
        }
    }
    cout<<"\n"<< std::flush;;
}

int evaluateGame(vector<long long> v){
    if(v[0] == 1 && v[1] == 1 && v[2] == 1) return 1;
    if(v[3] == 1 && v[4] == 1 && v[5] == 1) return 1;
    if(v[6] == 1 && v[7] == 1 && v[8] == 1) return 1;

    if(v[0] == 1 && v[3] == 1 && v[6] == 1) return 1;
    if(v[1] == 1 && v[4] == 1 && v[7] == 1) return 1;
    if(v[2] == 1 && v[5] == 1 && v[8] == 1) return 1;

    if(v[0] == 1 && v[4] == 1 && v[8] == 1) return 1;
    if(v[2] == 1 && v[4] == 1 && v[6] == 1) return 1;

    if(v[0] == 2 && v[1] == 2 && v[2] == 2) return 2;
    if(v[3] == 2 && v[4] == 2 && v[5] == 2) return 2;
    if(v[6] == 2 && v[7] == 2 && v[8] == 2) return 2;

    if(v[0] == 2 && v[3] == 2 && v[6] == 2) return 2;
    if(v[1] == 2 && v[4] == 2 && v[7] == 2) return 2;
    if(v[2] == 2 && v[5] == 2 && v[8] == 2) return 2;

    if(v[0] == 2 && v[4] == 2 && v[8] == 2) return 2;
    if(v[2] == 2 && v[4] == 2 && v[6] == 2) return 2;

    return 3;
}

vector<pair<vector<long long>, vector<long long>>> generateTurn(vector<long long> v){
    vector<pair<vector<long long>, vector<long long>>> layer; // vector of grids
    vector<long long> o;
    vector<long long> pos;
    for(int i = 0;i<9;i++){
        o = v;   
        if(v[i] == 3){
            pos.push_back(i);
            o[i] = 2;
            layer.push_back({o,pos});
        }         
    }
    return layer;
}

vector<vector<long long>> generateMoves(vector<long long> v, int currentPlayer) {
    vector<vector<long long>> moves;
    for (int i = 0; i < 9; i++) {
        if (v[i] == 3) { 
            vector<long long> o = v;
            o[i] = currentPlayer;
            moves.push_back(o);
        }
    }
    return moves;
}

// FIXME
/*void generateGames(vector<pair<vector<long long>, vector<long long>>> v){
    if(v.size() == 1){
        if(evaluateGame(v[0].first[0]) == 2){
            return;
        }
    }
    for(auto e:v){
        vector<pair<vector<long long>, vector<long long>>> u = generateTurn(e.first);
        generateGames(u);
    }
}*/

void displayWinner(int x){
    //cout<<"🏆 ";
    /*if(x == 1) cout<<"✖️";
    if(x == 2) cout<<"🟣";
    if(x == 3) cout<<"➖";*/
    //cout<<" 🏆";

    if(x == 1) cout<<"X";
    if(x == 2) cout<<"O";
    if(x == 3) cout<<"Draw";
    cout<< std::flush;
}

int userInput(vector<long long> v){
    cout<<"Your turn "<< std::flush;
    int userPos;
    cin>>userPos;
    userPos--;
    while(v[userPos] != 3 || userPos < 0 || userPos >= 9){
        cout<<"Invalid move! Try again: "<< std::flush;;
        cin>>userPos;
        userPos--;
    }
    return userPos-1;  
}

pair<int, vector<long long>> dfs(vector<long long> v, int currentPlayer) {
    int gameState = evaluateGame(v);
    if (gameState != 3) {
        return {gameState == 2 ? 1 : (gameState == 1 ? -1 : 0), v}; // AI win, User win, or Draw
    }

    vector<vector<long long>> moves = generateMoves(v, currentPlayer);
    int bestScore = (currentPlayer == 2) ? INT_MIN : INT_MAX; // Max for AI, Min for User
    vector<long long> bestMove;

    for (auto move : moves) {
        int currentScore = dfs(move, 3 - currentPlayer).first;
        if ((currentPlayer == 2 && currentScore > bestScore) || 
            (currentPlayer == 1 && currentScore < bestScore)) {
            bestScore = currentScore;
            bestMove = move;
        }
    }
    return {bestScore, bestMove};
}

void solve(int t) { 
    vector<long long> v(9, 3);

    // AI places it's first game randomly #turn 0
    srand(static_cast<unsigned int>(time(0)));
    int randomNumber = rand()%9;
    v[randomNumber] = 2;
    gridVisualiser(v);
    // User inputs it's first move #turn 1
    v[userInput(v)] = 1;
    gridVisualiser(v);


    int turn = 3;
    while(true){
        if(evaluateGame(v) != 3 || turn == 9){
            gridVisualiser(v);
            displayWinner(evaluateGame(v));
            break;
        }

        if(turn%2){ // AI's Turn
            cout << "AI is thinking...\n";
            v = dfs(v,2).second;
            gridVisualiser(v);
        }else{ // User's Turn
            v[userInput(v)] = 1;
            gridVisualiser(v);
        }

        turn++;
    }
}





int main() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);
    /*
    #ifndef ONLINE_JUDGE
        freopen("input.txt", "r", stdin);
        freopen("output.txt", "w", stdout); 
    #endif
    */
    int testcases = 1;
    //cin >> testcases;
    while (testcases--) {
        solve(testcases);
    }
    return 0;
}


