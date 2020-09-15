
module Awari
///  Each player has a set of regular pits and one home pit. A pit holds zero or more beans
type pit = int
/// A board consists of pits.
type board = pit array 
/// A game is played between two players
type player = Player1 | Player2


val printBoard : b:board -> unit


val isHome : b:board -> p:player -> i:pit -> bool


val isGameOver : b:board -> bool


val getMove : b:board -> p:player -> q:string -> pit


val distribute : b:board -> p:player -> i:pit -> board * player * pit


val turn : b:board -> p:player -> board


val play : b:board -> p:player -> board
