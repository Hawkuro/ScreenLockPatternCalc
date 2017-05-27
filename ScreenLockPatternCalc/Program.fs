﻿// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

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

let CalculateLockPatternPermCount rows columns =
    let toCoords ind =
        ind / columns, ind % columns
    let toIndex ((x,y) as coords) =
        x*columns + y
    let nodeCount = rows*columns
    let indexLimit = nodeCount-1
    let generator state = 
        if state < nodeCount then
            Some(state,state+1)
        else 
            None
    let indices = Seq.unfold generator 0 |> Seq.toList
    let coordsList = indices |> List.map toCoords

    let expand a b perm =
        let aC = toCoords a
        let bC = toCoords b
        let diff = subtractCoords bC aC
        let diffGcd = diff ||> gcd |> abs
        if diffGcd = 1 then
            true
        else
//            printfn "%A" aC
//            printfn "%A" bC
//            printfn "%A" diff
//            printfn "%A" diffGcd
            let d = divideCoords diff diffGcd
            let fullHead = 
                seq {for i in 0..diffGcd -> addCoords aC (multiplyCoords d i)}
                |> Seq.map toIndex
                |> Seq.toList
            ((Set.ofList fullHead) - (Set.ofList perm) = Set.empty)

    let keepPerm perm =
        // Assume tail is full
        match perm with
        | a::[] ->  true
        | [] ->  true
        | a::b::tail ->  expand a b perm

    let followingPerms perm = 
        indices
        |> List.except perm
        |> List.map (fun i -> i::perm) 
        |> List.filter keepPerm
//        |> List.map (fun i -> 
//            printfn "%A" i
//            i)

    let rec perms ps =
//        printfn "%A" ps
        seq { for p in ps do
                yield! followingPerms p |> perms
                yield p
        }

    let allPerms = perms (indices |> List.map (fun i -> [i]))

//    printfn "%A" allPerms
    
    allPerms |> Seq.length

let p n r = 
    seq{for i in (n-r+1)..n -> bigint(i)} |> Seq.fold (*) (bigint(1))
let memcost (a:int) (b:int) =
    let count = (a*b)
    seq {for i in 1..count -> bigint(i)*(p count i)} |> Seq.fold (+) (bigint(0)) |> (*) (bigint(4))

[<EntryPoint>]
let main argv = 
    match argv with
    | [|Integer a; Integer b|] ->
        memcost a b |> printfn "%A"
        let start = System.DateTime.Now;
        CalculateLockPatternPermCount a b |> printfn "%A"
        printfn "Calculations took %A" (System.DateTime.Now - start)
        0
    | _ -> failwith "arguments must be two integers"
