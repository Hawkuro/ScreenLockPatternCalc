# ScreenLockPatternCalc

![](http://i.imgur.com/Ez0vKQX.png)

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
 
 ## Calculation time
 
 The upper limit of possible permutations, i.e. ignoring the snapping if you skip over a node, is ![](https://latex.codecogs.com/svg.latex?%5Csum_%7Br%3D1%7D%5En%20nPr%20%3D%20%5Csum_%7Br%3D1%7D%5En%20%5Cfrac%7Bn%21%7D%7B%28n-r%29%21%7D), i.e. for each possible length of pattern ![](https://latex.codecogs.com/svg.latex?r), the number of permutations, summed.
 
 This sum is according to [this proof](https://math.stackexchange.com/a/161317) equal to ![](https://latex.codecogs.com/svg.latex?%5Cleft%20%5Clfloor%20n%21e%20%5Cright%20%5Crfloor), so we have an ![](https://latex.codecogs.com/svg.latex?O%28n%21%29) run time where ![](https://latex.codecogs.com/svg.latex?n) is the total number of nodes.
