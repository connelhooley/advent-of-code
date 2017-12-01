module Helpers

open System
open System.Text.RegularExpressions

let regexMatch regExp input =
    let regex = Regex regExp
    let regMatch = regex.Match input
    regMatch.Success