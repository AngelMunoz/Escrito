module Events

open Browser.Event
open Fable.Core.JsInterop

let GlobalBus = EventTarget.Create()


type Notifications =

    static member notify(message: string, ?variant: string, ?icon: string, ?duration: float) =
        importMember "/toaster.js"
