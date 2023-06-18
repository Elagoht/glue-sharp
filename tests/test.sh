#!/bin/env bash

# Build program
dotnet build

# Variables
sharpglue="./bin/Debug/net7.0/sharpglue"

# Create output files
mkdir -p tests/results
$sharpglue examples/header.csv examples/data.csv examples/data1.csv -s " & " -d ";" -r -H " values are " > tests/results/result1
$sharpglue examples/header.txt examples/data.txt examples/data1.txt -r -t -f "." -a center -s " | "  > tests/results/result2
$sharpglue examples/header.csv examples/data.csv examples/data1.txt -m > tests/results/result3
$sharpglue examples/header.txt examples/data.txt examples/data1.txt -r -H " --> " -s " - " > tests/results/result4
$sharpglue examples/header.txt examples/data.txt examples/data1.txt -H " --> " -s " - " > tests/results/result5

# Compare with results
diff tests/results/result1 tests/checks/check1 > tests/diffs/diff1 &&
diff tests/results/result2 tests/checks/check2 > tests/diffs/diff2 &&
diff tests/results/result3 tests/checks/check3 > tests/diffs/diff3 &&
diff tests/results/result4 tests/checks/check4 > tests/diffs/diff4 &&
diff tests/results/result5 tests/checks/check5 > tests/diffs/diff5

if [ $? -ne 0 ]
then 
    echo "==> Test failed. Check logs"
    exit 1
else
    echo "==> Test passed successfully."    
    exit 0
fi