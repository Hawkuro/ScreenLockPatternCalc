namespace ScreenLockPatternCalc
#l "Utils.fs"

open FSharp.Collections.ParallelSeq
open Utils

module LockPatternPermCounter = 

    let maxThreads = Settings.MaxThreads

    let upperLimitEstimate (a:int) (b:int) =
        let count = (a*b)
        seq {for i in 1..count -> (p count i)} |> Seq.fold (+) (bigint 0)

    let CalculateLockPatternPermCount rows columns =
        let toCoords ind =
            Coords(ind / columns, ind % columns)
        let toIndex (c:Coords) =
            c.x*columns + c.y
        let nodeCount = rows*columns

        let indices = [for i in 0..(nodeCount-1) -> i]
        let coordsList = indices |> List.map toCoords

        let expand a b perm =
            let aC = toCoords a
            let bC = toCoords b
            let diff = bC - aC
            let diffGcd = diff.asTuple ||> gcd |> abs
            if diffGcd = 1 then
                true
            else
                let d = diff / diffGcd
                let fullHead = 
                    seq {for i in 0..diffGcd -> aC + (d*i)}
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

        let rec perms ps =
            seq { for p in ps do
                    yield p
                    yield! followingPerms p |> perms
            }

        let allPerms =
            followingPerms []
            |> List.indexed
            |> List.groupBy (fst >> (fun i -> i % maxThreads))
            |> List.map (fun (m,list) -> List.map snd list)
            |> PSeq.collect perms 

        allPerms |> PSeq.length

