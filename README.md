# ScreenLockPatternCalc
Calculates number of possible android screen lock patterns

## To build

1. Clone the repo
2. Open the sln in Visual Studio 2015+
3. Build the solution

## Usage example

    > ./ScreenLockPatternCalc.exe 3 3
    Upper limit of 986409 distinct permutations
    A 3 by 3 grid yields 389497 distinct possible permutations
    Calculations took 00:00:00.5622309

## Options

Options can be found in the App.config file

 - maxThreads: Maximum number of parallel threads used when calculating. Upper bound is the total number of nodes in the grid (e.g. for a 3x3 grid, 9)
 - printTime: Whether or not to print the amount of time calculations take
 - printUpperEstimate: Whether or not to print the estimated upper limit of permutations
