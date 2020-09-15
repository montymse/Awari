Awari
------------------------
Sådan oversættes og køres programmet:
------------------------

1)
For at generere et biblotek ud af de inkluderede .fsi og .fs filer, så skal der i consolen skrives: fsharpc -a awariLibcomplete.fsi awariLibcomplete.fs
Heraf fås et biblotek i form af en .dll -fil, som nu kan anvendes til applikationsprogammet. 
2)
For at anvende den genererede biblotek til en applikation, så skrives: fsharpc -r awariLibcomplete.dll playawari.fsx && mono playawari.exe
Hermed har du et applikationsprogram kørende som tager brug i Awari-bibloteket, og du kan nu spille Awari.
3) 
Nyd spillet!

Note: programmerne kræver mono og f# for at køre :)
