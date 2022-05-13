module Types

open Browser.Types
open Browser.Dom
open Fable.Core

type EsText =
    {| _id: string
       _rev: string
       text: string
       holes: string [] |}

[<RequireQualifiedAccess>]
type Page =
    | Home
    | About


type SlTextarea =
    inherit HTMLElement
    abstract member setRangeText: string * int * int * string -> unit
    abstract member setRangeText: string * int * int -> unit
    abstract member setRangeText: string * int -> unit
    abstract member setRangeText: string -> unit
