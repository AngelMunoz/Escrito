[<AutoOpen>]
module Extensions

open Browser.Types
open Fable.Core
open Fable.Core.JsInterop
open Lit
open Types

[<AutoOpen>]
type EventExtensions =

    [<Emit("new Event($0, $1)")>]
    static member createEvent(name: string, ?options: obj) : Event = jsNative

    [<Emit("new CustomEvent($0, $1)")>]
    static member createCustomEvent(name: string, ?options: obj) : CustomEvent<_> = jsNative


type LitElement with
    [<Emit("this.addController($0)")>]
    member _.addController _ = jsNative

type CustomEvent<'T> with
    member this.Value: string =
        let el = (this.target :?> HTMLElement)
        el?value

type SlTextarea with

    member this.TextArea =
        this.shadowRoot.querySelector ("textarea") :?> HTMLTextAreaElement
