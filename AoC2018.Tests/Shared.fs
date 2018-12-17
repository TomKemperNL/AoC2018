[<AutoOpen>]
module Shared
open System.Reflection
open System.IO

let readFile fileName =
    let path = Path.Combine(Assembly.GetEntryAssembly().Location, fileName)
    System.IO.File.ReadAllText path

let readFileLines fileName =
    let path = Path.Combine(Assembly.GetEntryAssembly().Location, fileName)
    System.IO.File.ReadAllLines path