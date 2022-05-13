module Events

open Browser.Dom
open Browser.Event
open Browser.Types
open Browser.MediaQueryList
open Browser.MediaQueryListExtensions
open Fable.Core.JsInterop

let GlobalBus = EventTarget.Create()

let isMobileMediaQuery = window.matchMedia ("(max-width: 768px)")

isMobileMediaQuery.addEventListener (
    "change",
    fun (e) ->
        let e = (e :?> MediaQueryList)

        createCustomEvent ("is-mobile", {| detail = e.matches |})
        |> GlobalBus.dispatchEvent
        |> ignore
)



type Notifications =

    static member notify(message: string, ?variant: string, ?icon: string, ?duration: float) =
        importMember "/toaster.js"
