
 (*White box af Awari biblotek*)

type pit = int
type board = pit array
type player = Player1 | Player2

 (*White box testning af Ishome*) 

printfn ""
printfn "White -box testing of Ishome"
printfn "%A" (Awari.isHome [|3;3;3;3;3;3;0;3;3;3;3;3;3;0|] Awari.Player1 6=true)
printfn "%A" (Awari.isHome [|3;3;3;3;3;3;0;3;3;3;3;3;3;0|] Awari.Player1 13=false)
printfn "%A" (Awari.isHome [|3;3;3;3;3;3;0;3;3;3;3;3;3;0|] Awari.Player2 6=false)
printfn "%A" (Awari.isHome [|3;3;3;3;3;3;0;3;3;3;3;3;3;0|] Awari.Player2 13=true)

 (*White box testning af IsGameover*) 

printfn ""
printfn "White -box testing of IsGameover"
printfn "%A" (Awari.isGameOver [|3;3;3;0;4;4;1;3;3;3;3;3;3;0|]=false)
printfn "%A" (Awari.isGameOver [|0;0;0;0;0;0;18;3;3;3;0;4;4;1|]=true)
printfn "%A" (Awari.isGameOver [|3;3;3;3;3;3;1;0;0;0;0;0;0;1|]=true)

 (*White box testning af getMove*) 

let rec getMove (b:board) (p:player) (q:string) (a:pit) : pit =  
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
    getMove b Player1 q a
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
    getMove b Player2 q a

printfn ""
printfn "White -box testing of getMove"
printfn "%A" (getMove [|3;3;3;3;3;3;0;3;3;3;3;3;3;0|] Player2 "A string" 1=7) 

printfn "%A" (getMove [|3;3;3;3;3;3;0;3;3;3;3;3;3;0|] Player2 "A string" 2=8) 

printfn "%A" (getMove [|3;3;3;3;3;3;0;3;3;3;3;3;3;0|] Player2 "A string" 3=9) 

printfn "%A" (getMove [|3;3;3;3;3;3;0;3;3;3;3;3;3;0|] Player2 "A string" 4=10) 

printfn "%A" (getMove [|3;3;3;3;3;3;0;3;3;3;3;3;3;0|] Player2 "A string" 5=11) 
printfn "%A" (getMove [|3;3;3;3;3;3;0;3;3;3;3;3;3;0|] Player2 "A string" 6=12) 

printfn "%A" (getMove [|3;3;3;3;3;3;0;3;3;3;3;3;3;0|] Player1 "A string" 1=0)
printfn "%A" (getMove [|3;3;3;3;3;3;0;3;3;3;3;3;3;0|] Player1 "A string" 2=1)
printfn "%A" (getMove [|3;3;3;3;3;3;0;3;3;3;3;3;3;0|] Player1 "A string" 3=2)
printfn "%A" (getMove [|3;3;3;3;3;3;0;3;3;3;3;3;3;0|] Player1 "A string" 4=3)
printfn "%A" (getMove [|3;3;3;3;3;3;0;3;3;3;3;3;3;0|] Player1 "A string" 5=4)
printfn "%A" (getMove [|3;3;3;3;3;3;0;3;3;3;3;3;3;0|] Player1 "A string" 6=5)

 (*White box testning af distribute*) 

printfn ""
printfn "testing af distribute"
printfn "%A" ((Awari.distribute [|0;0;0;0;1;0;0;1;0;0;0;0;0;0|] Awari.Player1 4) = ([|0;0;0;0;0;0;2;0;0;0;0;0;0;0|],Awari.Player1,5)) //branch 1.2.1
printfn "%A" ((Awari.distribute [|0;2;0;0;0;0;0;0;0;0;0;0;0;0|] Awari.Player1 1) = ([|0;0;1;1;0;0;0;0;0;0;0;0;0;0|],Awari.Player1,3)) //branch 1.2.2
printfn "%A" ((Awari.distribute [|0;0;0;0;1;0;0;1;0;0;0;0;0;0|] Awari.Player2 7) = ([|0;0;0;0;0;0;0;0;0;0;0;0;0;2|],Awari.Player2,8)) //branch 2.2.1
printfn "%A" ((Awari.distribute [|0;0;0;0;0;0;0;2;0;0;0;0;0;0|] Awari.Player2 7) = ([|0;0;0;0;0;0;0;0;1;1;0;0;0;0|],Awari.Player2,9)) //branch 2.2.2
