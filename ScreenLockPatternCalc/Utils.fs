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

    type Coords(x:int,y:int) =
        member this.x = x
        member this.y = y
        static member (*) (c : Coords, m:int) =
            Coords(m * c.x, m * c.y)
        static member (/) (c : Coords, d:int) =
            Coords(c.x / d, c.y / d)
        static member (+) (a: Coords, b: Coords) = 
            Coords(a.x+b.x,a.y+b.y)
        static member (-) (a: Coords, b: Coords) = 
            Coords(a.x-b.x,a.y-b.y)
        member this.asTuple = (this.x,this.y)
