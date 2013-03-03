﻿#r "../../lib/FSharp.Compiler.dll"

// Open the namespace with InteractiveChecker type
open System
open Microsoft.FSharp.Compiler.SourceCodeServices

// Create an interactive checker instance (ignore notifications)
let checker = InteractiveChecker.Create(NotifyFileTypeCheckStateIsDirty ignore)

let parse input file =
    // Get compiler options for a single script file
    let checkOptions = checker.GetCheckOptionsFromScriptRoot(file, input, DateTime.Now, [||])
    // Run the first phase (untyped parsing) of the compiler
    let untypedRes = checker.UntypedParse(file, input, checkOptions)

    match untypedRes.ParseTree with
    | Some tree ->  sprintf "Got tree %A" tree
    | None -> sprintf "Nothing"

// Sample input for the compiler service
let input = """
  module Tests
  /// This is a foo function
  let foo() = 
    // Line comment
    let msg = "Hello world"
    printfn "%s" msg """
let file = "/home/user/Test.fs"

let test = "let product = List."

#time "on";;
printfn "%s" <| parse test file