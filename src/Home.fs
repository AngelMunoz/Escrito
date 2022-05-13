module Pages.Home

open Lit
open Events
open Browser.Types
open Types
open Components


Fable.Core.JsInterop.importSideEffects "./home.css"

let textsTpl (onSelected: EsText option -> unit) =

    let tpl (text: EsText) =
        html $"""<sl-menu-item value={text._id} @click={fun _ -> onSelected (Some text)}>{text._id}</sl-menu-item>"""

    promise {
        do! Promise.sleep (1000)
        let! texts, count = Database.getTexts ()

        if texts.Length > 0 then
            return texts |> Lit.mapUnique (fun t -> t._id) tpl
        else
            return html $"""<sl-menu-item disabled>No texts found</sl-menu-item>"""
    }


[<HookComponent>]
let View () =
    let currentText, setCurrentText = Hook.useState None
    let texts, setTexts = Hook.useState [||]

    let textList =
        Lit.ofPromise (textsTpl setCurrentText, html $"""<sl-menu-item>{spinner Large}</sl-menu-item>""")

    html
        $"""
        <h1>Escrito</h1>
        <sl-tab-group placement="start">
            <sl-tab slot="nav" panel="new">New Text</sl-tab>
            <sl-tab slot="nav" panel="existing" active>Custom</sl-tab>

            <sl-tab-panel name="new">
                <header>
                    <h4>Create and Save a new text</h4>
                </header>
                <es-text-editor
                    .content={currentText
                              |> Option.map (fun t -> t.text)
                              |> Option.defaultValue ""}
                    @on-text-save={fun (e: CustomEvent<string>) -> printfn $"{e.detail}"}>
                </es-text-editor>
            </sl-tab-panel>
            <sl-tab-panel name="existing">
                <header>
                    <h4>Review Existing texts</h4>
                </header>
                <sl-menu>
                    {textList}
                </sl-menu>
            </sl-tab-panel>
        </sl-tab-group>"""
