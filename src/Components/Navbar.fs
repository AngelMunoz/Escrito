[<RequireQualifiedAccess>]
module Components.Navbar

open Lit
open Types
open Events

let navigateTo page =
    let cevt = createCustomEvent ("navigate-to", {| detail = page |})

    GlobalBus.dispatchEvent cevt

[<LitElement("es-navbar")>]
let private NavBar () =

    LitElement.init (fun config -> config.useShadowDom <- false)
    |> ignore

    html
        $"""
        <sl-button-group>
          <sl-button pill @click={fun _ -> navigateTo Page.Home}>
            <sl-icon name="house-door"></sl-icon>
            Home
          </sl-button>
          <sl-button pill @click={fun _ -> navigateTo Page.About}>
            <sl-icon name="info-lg"></sl-icon>
            About
          </sl-button>
        </sl-button-group>
        """

let register () = ()
