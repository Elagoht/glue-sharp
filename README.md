# C# Replica of My Project Glue 

![Build](https://img.shields.io/github/actions/workflow/status/Elagoht/sharpglue/diffs.yml?style=for-the-badge)
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
  -H, --header-divider=VALUE Add a divider after first column/row, overwrites 
                               alignment
  -t, --transpose            Swap columns and rows
  -r, --remove-last-blank    Delete last blank lines to minimize the output
  -b, --border               Add extra separators at the beginning and end of 
                               each line
      --csv                  Csv with semicolon, same as -r -t -n -s ";"
      --csv2                 Csv with comma, same as -r -t -n -s ","
  -m, --markdown             Create markdown table formatted output, same as -
                               r -t -s " | " -b

Dev Homepage : https://github.com/Elagoht/sharpglue
Bug Reports  : https://github.com/Elagoht/sharpglue/issues
```

## Examples

try this commands and check file contents to understand working logic.

**Example:**

```sh
$ sharpglue examples/header.csv examples/data.csv examples/data1.csv -s " & " -d ";" -r -H " values are "
```

```
Full Name values are Furkan Baytekin          & Anonymus                   
Job       values are Software Developer       & Hacker                     
Email     values are furkanbaytekin@gmail.com & anonymus@protonmail.com    
Language  values are Python, JavaScript, C    & C, Shell Script, PowerShell
```

**Example:**

```sh
$ sharpglue examples/header.txt examples/data.txt examples/data1.txt -t -f "." -a center -s ' | '
```

```
...Full Name... | .......Job........ | .........Email.......... | .........Languages.........
Furkan Baytekin | Software Developer | furkanbaytekin@gmail.com | ..Python, JavaScript, C#...
...Anonymus.... | ......Hacker...... | anonymus@protonmail.com. | C, Shell Script, PowerShell
```

**Example:**

```sh
$ sharpglue examples/header.csv data.csv -m
```

```
 | Full Name       | Job                | Email                    | Languages                   | 
 | --------------- | ------------------ | ------------------------ | --------------------------- | 
 | Furkan Baytekin | Software Developer | furkanbaytekin@gmail.com | Python, JavaScript, C#      | 
 | Anonymus        | Hacker             | anonymus@protonmail.com  | C, Shell Script, PowerShell | 
```

**Example:**

```sh
$ sharpglue examples/header.txt examples/data.txt -H " --> "
```

```
Full Name --> Furkan Baytekin         
Job       --> Software Developer      
Email     --> furkanbaytekin@gmail.com
Languages --> Python, JavaScript, C#  
```