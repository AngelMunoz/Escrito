[<RequireQualifiedAccess>]
module Components.Navbar

open Lit
open Browser.Types
open Browser.Dom
open Types
open Fable.Core.JS
open Fable.Core.JsInterop


let mutable canUpdate = false

[<LitElement("update-banner")>]
let private NavBar () =

    let host = LitElement.init ()

    let showPrompt () =
        promise {
            let alert = host.shadowRoot.querySelector ("sl-alert")
            do! alert?show ()
            return canUpdate
        }

    Hook.useEffectOnce (fun _ -> host?showUpdatePrompt <- showPrompt)

    let updateShouldHide (willUpdate) =
        canUpdate <- willUpdate
        let alert = host.shadowRoot.querySelector ("sl-alert")
        alert?hide ()

    html
        $"""
        <sl-alert closable>
          An update is available, update now?
          <sl-button-group>
            <sl-button @click={fun _ -> updateShouldHide true}>Yes</sl-button>
            <sl-button @click={fun _ -> updateShouldHide false}>No</sl-button>
          </sl-button-group>
        </sl-alert>"""

let register () = ()
