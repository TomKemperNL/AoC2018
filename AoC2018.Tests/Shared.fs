[<AutoOpen>]
module Shared
open System.Reflection
open System.IO

let private getPath name = 
    let f = new FileInfo(Assembly.GetExecutingAssembly().Location)
    Path.Combine(f.DirectoryName, name)

let readFile fileName =   
    System.IO.File.ReadAllText (getPath fileName)

let readFileLines fileName =    
    System.Console.WriteLine (getPath fileName)
    System.IO.File.ReadAllLines (getPath fileName)