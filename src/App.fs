[<RequireQualifiedAccess>]
module App

open Lit
open Browser.Types
open Types
open Components
open Browser.Dom
open Pages
open Events



let navigateTo onNavigateTo (event: Event) =
    let event = (event :?> CustomEvent<Page>)
    event.detail |> Option.iter onNavigateTo

[<HookComponent>]
let private app () =
    let state, setState = Hook.useState Page.Home

    Hook.useEffectOnce (fun _ ->
        let navigateTo = navigateTo setState
        GlobalBus.addEventListener ("navigate-to", navigateTo)

        { new System.IDisposable with
            member _.Dispose() : unit =
                GlobalBus.removeEventListener ("navigate-to", navigateTo) })

    html
        $"""
        <main>
            {match state with
             | Page.Home -> Home.View()
             | Page.About -> About.View()}
        </main>
        <es-navbar></es-navbar>"""

let start () =
    Lit.render (document.querySelector "#lit-app") (app ())
