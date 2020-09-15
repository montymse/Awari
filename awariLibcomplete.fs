
module Awari
//Module types definitions
type pit = int
type board = pit array
type player = Player1 | Player2


/// <summary> IsGameover
/// Check whether the game is over
/// </summary>
/// <param name="b"> A board to check</param>
/// <returns>True if either side has no beans</returns>

let isGameOver (b:board) : bool = 
  b.[0..5] = [|0;0;0;0;0;0|] || b.[7..12] = [|0;0;0;0;0;0|] 

/// <summary> Ishome
/// Check whether a pit is the player's home
/// </summary>
/// <param name="b">A board to check</param>
/// <param name="p">The player, whos home to check</param>
/// <param name="i">A regular or home pit of a player</param>
/// <returns>True if a pit is a player's </returns>

let isHome (b: board) (p: player) (i:pit) : bool =
    match p with 
    |p when p=Player1 -> i=6
    | _ -> i=13

/// <summary> printboard
/// Prints the board
/// </summary>
/// <param name="b"> A board to be printed </param>
/// <returns>() - it just prints</returns>
/// , e.g.,
/// <remarks>
/// Output is for example,
/// <code>
///      3  3  3  3  3  3
///   0                    0
///      3  3  3  3  3  3
/// </code>
/// where player 1 is bottom row and rightmost home
/// </remarks>

let printBoard (b: board) =
    printfn "    %A  %A  %A  %A  %A  %A" b.[12] b.[11] b.[10] b.[9] b.[8] b.[7]
    printfn "%A                      %A" b.[13] b.[6]
    printfn "    %A  %A  %A  %A  %A  %A" b.[0] b.[1] b.[2] b.[3] b.[4] b.[5]

/// <summary> choose
/// Takes an input from the the player
/// </summary>
/// <param name="msg">
///  unused generic variable </param>
/// <returns>return an pit (int) <returns>

let rec choose msg = 
    try
          int (System.Console.ReadLine())
      with
        | :? System.FormatException -> 
          printfn "Choose a number between 1-6" 
          choose msg

/// <summary> getMove
/// Get the pit of next move from the user
/// </summary>
/// <param name="b">The board the player is choosing from</param>
/// <param name="p">The player, whose turn it is to choose</param>
/// <param name="q">The string to ask the player</param>
/// <returns>The pit the player has chosen</returns>

let rec getMove (b:board) (p:player) (q:string) : pit =  
  printfn "%s" q  //Prints string given as argument
  
  let a = choose 0

  printfn "Player has chosen pit %A" a
  match p with 
  | p  when p = Player1 -> 
    match a with 
    | 1 -> 0
    | 2 -> 1
    | 3 -> 2
    | 4 -> 3
    | 5 -> 4
    | 6 -> 5
    | _ -> 
    printfn "Please try again pit %A is not part of your side of the board" a 
    getMove b Player1 q
  | _ -> 
    match a with
    | 1 ->  7
    | 2 ->  8
    | 3 ->  9
    | 4 ->  10
    | 5 ->  11
    | 6 ->  12
    | _ -> 
    printfn "Please try again pit %A is not part of your side of the board" a 
    getMove b Player2 q
   

/// <summary> Whatplayerpit
/// Whatplayerpit matches player and pit together
/// </summary>
/// <param name="e">The pit in the array in which the last bean has landed in</param>
/// <returns> It returns which player's pit the last bean landed in</returns>

let whatplayerpit (e:pit) : player = 
   match e with 
    | e when 0<=e && e<=5 -> Player1
    | e when 7<=e && e<=12 -> Player2
    | _ -> Player1  // in order to avoid incomplete pattern matching then we have this match

/// <summary> Distribute
/// Distributing beans counter clockwise, capturing when relevant
/// </summary>
/// <param name="b">The present statu of the board</param>
/// <param name="p">The player, whos beans to distribute</param>
/// <param name="i">The regular pit to distribute</param>
/// <returns>A new board after the beans of pit i has been distributed, and which player's pit the last bean landed in</returns>

let rec distribute (b : board) (p : player) (i: pit) : board*player*pit  =
    let x=b.[i] //PLAYER MOVE - VALUE
    if x=0 then distribute b p (getMove b p "Pit is empty try again") // man kan ikke vælge et tom felt uden bønner
    else
      let y=(i+x)%14 //Hvor mange felter fremad? - feltet den sidste bønne lander i
      //Incrementér felter
      for j=1 to b.[i] do
            b.[(i+j)%14] <- b.[(i+j)%14]+1
      b.[i] <- 0 //reset chosen pit

      match p with
        |p  when p = Player1 -> //BRANCH 1: PLAYER 1?
          match b with
          | b when y=6 && isGameOver b = false  -> // y=6 AND game is not over? - Sidste felt er Player1's hjemmebane? BRANCH 1.1
              printBoard b
              distribute b Player1 (getMove b Player1 "Again") //Player1 får en tur mere
          | _ ->  //ELSE? Spillet er over ELLER sidste felt er ikke Player1's hjemmebane? BRANCH 1.2
            if b.[y]=1 && b.[12-y] > 0 then //BRANCH - Sidste felt er lig med 1 - feltet var lig med 0 til at starte med BRANCH 1.2.1
              b.[6] <- b.[6]+b.[12-y]+b.[y] //Bønnerne i det aktuelle felt og i det modsatte felt smides i Player1's hjemmebane
              b.[12-y] <-0
              b.[y] <-0
              (b,whatplayerpit y,y)
            else (b,whatplayerpit y,y) //ELSE - BRANCH 1.2.2
        | _  -> //PLAYER2? BRANCH 2
          match b with 
          | b when y=13 && isGameOver b = false -> //BRANCH 2.1
              printBoard b
              distribute b Player2 (getMove b Player2 "Again")
          | _ -> //BRANCH 2.2
            if b.[y]=1 && b.[12-y]>0 then //BRANCH 2.2.1
              b.[13] <- b.[13]+b.[12-y]+b.[y]
              b.[12-y] <-0
              b.[y] <- 0
              (b,whatplayerpit y,y)
            else (b,whatplayerpit y,y) //BRANCH 2.2.2

/// <summary> Turn
/// Interact with the user through getMove to perform a possibly repeated turn of a player
/// </summary>
/// <param name="b">The present state of the board</param>
/// <param name="p">The player, whose turn it is</param>
/// <returns>A new board after the player's turn</returns>

let turn (b : board) (p : player) : board =
  let rec repeat (b: board) (p: player) (n: int) : board =
    printBoard b
    let str =
      if n = 0 then
        sprintf "Player %A's move? " p
      else 
        "Again? "
    let i = getMove b p str
    let (newB, finalPitsPlayer, finalPit)= distribute b p i
    if not (isHome b finalPitsPlayer finalPit) 
       || (isGameOver b) then
      newB
    else
      repeat newB p (n + 1)
  repeat b p 0 

/// <summary> Play
/// Play game until one side is empty
/// </summary>
/// <param name="b">The initial board</param>
/// <param name="p">The player who starts</param>
/// <returns>A new board after one player has won</returns>

let rec play (b : board) (p : player) : board =
  if isGameOver b then
    printfn "done done"
    if b.[6]>b.[13] then printfn "Player1 has won"  // if-else statementet giver os vinderen
    elif b.[6]=b.[13] then printfn "TIE!"
    else printfn "Player2 has won"
    b
  else
    let newB = turn b p
    let nextP =
      if p = Player1 then
        Player2
      else
        Player1
    play newB nextP

