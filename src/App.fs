[<RequireQualifiedAccess>]
module App

open Lit
open Browser.Types
open Types
open Components
open Browser.Dom
open Pages.Home
open Events



[<HookComponent>]
let private app () =
    let state, setState = Hook.useState Page.Home

    let getPage page =
        match page with
        | Page.Home -> Home()

    html $"""{getPage state}"""

let start () =
    Lit.render (document.querySelector "#lit-app") (app ())
