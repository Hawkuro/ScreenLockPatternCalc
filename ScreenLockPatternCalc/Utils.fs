namespace ScreenLockPatternCalc

open FSharp.Configuration

module Utils = 
    type Settings = AppSettings<"App.config">

    let p n r = 
        seq{for i in (n-r+1)..n -> bigint i} |> Seq.fold (*) (bigint 1)

    let (|Integer|_|) (str: string) =
       let mutable intvalue = int 0
       if System.Int32.TryParse(str, &intvalue) then Some(intvalue)
       else None

    let rec gcd a b =
        if b = 0 then
            a
        else gcd b (a%b)

    let subtractCoords (x1,y1) (x2,y2) =
        (x1-x2,y1-y2)

    let addCoords (x1,y1) (x2,y2) =
        (x1+x2,y1+y2)

    let divideCoords (x,y) d = (x/d,y/d)

    let multiplyCoords (x,y) m = (x*m,y*m)