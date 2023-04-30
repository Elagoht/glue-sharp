# C# Replica of My Project Glue 

![C#](https://shields.io/badge/C%23-239120?logo=csharp&logoColor=white&style=for-the-badge)
![Terminal](https://shields.io/badge/Terminal_Tool-241F31?logo=gnometerminal&logoColor=white&style=for-the-badge)
![CMD](https://shields.io/badge/CMD_Tool-4D4D4D?logo=windowsterminal&logoColor=white&style=for-the-badge)

This is a C# clone of glue commandline tool. Glue is a commandline tool to paste files.
There's a paste command for it but it cannot align texts. Glue does that. Now, I remake
it in C# with OOP. The original tool was written in Python.

## Help Page

```
$ sharpglue --help
```

```
Usage: sharpglue [OPTIONS] [INPUT FILES]
Options:
  -h, --help                 Show this message and exit
  -a, --alignment=VALUE      Determine what alignment will be used
                               Valid values : 
                                  To left   : (default)
                                  To center : 0 | center
                                  To right  : 1 | right
  -n, --noalign              Do not align fields, overwrites alignment option
  -d, --delimiter=VALUE      String value that will split the file contents
  -s, --separator=VALUE      String value that will bind the new parts
  -f, --filler=VALUE         Determine what empty areas will be filled with
  -t, --transpose            Swap columns and rows
      --csv                  Csv with semicolon, same as -t -n -s ";"
      --csv2                 Csv with comma, same as -t -n -s ","

Dev Homepage : https://github.com/Elagoht/sharpglue
Bug Reports  : https://github.com/Elagoht/sharpglue/issues
```

## Examples

try this commands and check file contents to understand working logic.

**Example:**

```sh
$ sharpglue header.csv data.csv -f "_" -s " | " -d ";"
```

```
Full Name | Furkan Baytekin_________
Job______ | Software Developer______
Email____ | furkanbaytekin@gmail.com
Languages | Python, JavaScript, C#__
```

**Example:**

```sh
$ sharpglue header.txt data.txt -t -f "." -a center -s '   '
```

```
...Full Name...   .......Job........   .........Email..........   ......Languages.......
Furkan Baytekin   Software Developer   furkanbaytekin@gmail.com   Python, JavaScript, C#
```

## Under Development!

This project is not finished yet.