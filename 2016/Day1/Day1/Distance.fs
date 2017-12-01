module Distance

open System
open Types

let calculate position = 
    Math.Abs(position.x) + Math.Abs(position.y)