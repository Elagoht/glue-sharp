# C# Replica of My Project Glue 

![C#](https://shields.io/badge/C%23-239120?logo=csharp&logoColor=white&style=for-the-badge)
![Terminal](https://shields.io/badge/Terminal_Tool-241F31?logo=gnometerminal&logoColor=white&style=for-the-badge)
![CMD](https://shields.io/badge/CMD_Tool-4D4D4D?logo=windowsterminal&logoColor=white&style=for-the-badge)

This is a C# clone of glue commandline tool. Glue is a commandline tool to paste files.
There's a paste command for it but it cannot align texts. Glue does that. Now, I remake
it in C# with OOP. The original tool was written in Python.

## Examples

try this commands and check file contents to understand working logic.

**Example:**

```sh
$ sharpglue header.csv data.csv -f "_" -s " | " -d ";"
```

**Result:**

```
Full Name | Furkan Baytekin_________
Job______ | Software Developer______
Email____ | furkanbaytekin@gmail.com
Languages | Python, JavaScript, C#__
```

**Example:**

```sh
sharpglue header.txt data.txt -t -f "." -a center -s '   '
```

**Result:**

```
...Full Name...   .......Job........   .........Email..........   ......Languages.......
Furkan Baytekin   Software Developer   furkanbaytekin@gmail.com   Python, JavaScript, C#
```

## Under Development!

This project is not finished yet.