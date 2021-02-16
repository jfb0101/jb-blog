namespace fsharp.Controllers

open Microsoft.AspNetCore.Mvc

module Performance = 
    let measurePerformance fn = 
        let stopWatch = System.Diagnostics.Stopwatch.StartNew()
        let result = fn()
        
        stopWatch.Stop()

        (result,stopWatch.Elapsed.TotalMilliseconds)


[<ApiController>]
[<Route("[controller]")>]
type PlayWithFsharpController () = 
    inherit ControllerBase()

    [<HttpGet("TestParallel")>]
    member _.Parallel() =
        let oneBigArray = [|0 .. 100000|]

        let rec computeSomeFunction x = 
            if x <= 2 then 1
            else computeSomeFunction (x - 1) + computeSomeFunction(x - 2)

        let computeResults inParallel = 
            let mapFn = if inParallel then Array.Parallel.map else Array.map

            oneBigArray |> mapFn (fun x -> computeSomeFunction (x % 20))

        let (resultNonParallel,timeNonParallel) = Performance.measurePerformance (fun () -> computeResults false)
        
        let (resultParallel,timeParallel) = Performance.measurePerformance (fun () -> computeResults true)

        $"resultNonParallel: %A{resultNonParallel} in {timeNonParallel} ms\n
        resultParallel: %A{resultParallel} in {timeParallel} ms"

        // $"result: %A{result}"

        

