Awari

<h2> How to run </h2>

1) Generate the libraries from the .fsi and .fs files

```
fsharpc -a awariLibcomplete.fsi awariLibcomplete.fs
```

This command will create a .dll  library, which now can be used for the applicationprogram 

2) Use the generated library and run the application

```
fsharpc -r awariLibcomplete.dll playawari.fsx && mono playawari.exe
```

3) Play the game!

