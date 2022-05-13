module Components.TextEditor


open Lit

open Browser.Types
open Events
open Types

Fable.Core.JsInterop.importSideEffects "./text-editor.css"

let configure (config: LitConfig<_>) =
    config.props <- {| content = Prop.Of("", "content") |}
    config.useShadowDom <- false

type TextEffect =
    | H of int
    | Bold
    | Italic
    | Ul
    | Ol

[<LitElement("es-text-editor")>]
let private TextEditor () =
    let host, props = LitElement.init configure
    let isMobile, setIsMobile = Hook.useState (isMobileMediaQuery.matches)

    let content, setContent = Hook.useState props.content.Value
    let editorRef = Hook.useRef<SlTextarea> ()


    Hook.useEffectOnce (fun _ ->
        let onIsMobileChange =
            fun (e: Event) -> Option.iter setIsMobile (e :?> CustomEvent<bool>).detail

        GlobalBus.addEventListener ("is-mobile", onIsMobileChange)

        { new System.IDisposable with
            member _.Dispose() : unit =
                GlobalBus.removeEventListener ("is-mobile", onIsMobileChange) })

    let saveText () =
        host.dispatchCustomEvent ("on-text-save", content)

    let addEffect textEfect _ =
        match editorRef.Value with
        | Some editor ->
            let txtArea = editor.TextArea
            let text = content[txtArea.selectionStart .. txtArea.selectionEnd]

            let start, selEnd =
                if text.Length = 0 then
                    content.Length, content.Length
                else
                    txtArea.selectionStart, txtArea.selectionEnd

            let insertBefore = if start + selEnd = 0 then "" else "\n"

            match textEfect with
            | Bold -> editor.setRangeText ($"**{text}**", start, selEnd)
            | Italic -> editor.setRangeText ($"_{text}_", start, selEnd)
            | Ol -> editor.setRangeText ($"{insertBefore}#. {text}", start, selEnd)
            | Ul -> editor.setRangeText ($"{insertBefore}- {text}", start, selEnd)
            | H 1 -> editor.setRangeText ($"{insertBefore}# {text}", start, selEnd)
            | H 2 -> editor.setRangeText ($"{insertBefore}## {text}", start, selEnd)
            | H 3 -> editor.setRangeText ($"{insertBefore}### {text}", start, selEnd)
            | H _ -> ()
        | None -> Fable.Core.JS.console.log ("No editor found")

    html
        $"""
        <nav class="es-text-nav">
            <sl-button-group>
                <sl-tooltip content="Header 1">
                    <sl-button @click={addEffect (H 1)}>
                        <sl-icon name="type-h1"></sl-icon>
                    </sl-button>
                </sl-tooltip>
                <sl-tooltip content="Header 2">
                    <sl-button @click={addEffect (H 2)}>
                        <sl-icon name="type-h2"></sl-icon>
                    </sl-button>
                </sl-tooltip>
                <sl-tooltip content="Header 3">
                    <sl-button @click={addEffect (H 3)}>
                        <sl-icon name="type-h3"></sl-icon>
                    </sl-button>
                </sl-tooltip>
            </sl-button-group>
            <sl-button-group>
                <sl-tooltip content="Bold">
                    <sl-button @click={addEffect Bold}>
                        <sl-icon name="type-bold"></sl-icon>
                    </sl-button>
                </sl-tooltip>
                <sl-tooltip content="Italic">
                    <sl-button @click={addEffect Italic}>
                        <sl-icon name="type-italic"></sl-icon>
                    </sl-button>
                </sl-tooltip>
            </sl-button-group>
            <sl-button-group>
                <sl-tooltip content="Bullet List">
                    <sl-button @click={addEffect Ul}>
                        <sl-icon name="list-ul"></sl-icon>
                    </sl-button>
                </sl-tooltip>
                <sl-tooltip content="Number List">
                    <sl-button @click={addEffect Ol}>
                        <sl-icon name="list-ol"></sl-icon>
                    </sl-button>
                </sl-tooltip>
            </sl-button-group>
            <sl-button-group>
                <sl-button pill @click={saveText}> <sl-icon name="save"></sl-icon> Save Content</sl-button>
            </sl-button-group>
        </nav>
        <sl-split-panel ?vertical={isMobile} class="es-text-panels">
            <section slot="start">
                <sl-textarea
                    {Lit.refValue editorRef}
                    resize="auto"
                    label="Your new text"
                    help-text='use "~name~" to specify "holes" in your text'
                    @sl-input={fun (e: CustomEvent<string>) -> Option.iter setContent (Option.ofObj e.Value)}></sl-textarea>
            </section>
            <section slot="end">
                <sl-textarea
                    resize="auto"
                    label="How it will look"
                    .value={content.Replace("~name~", "____")}
                    help-text="This will be the output of the text"
                    readonly></sl-textarea>
            </section>
        </sl-split-panel>"""


let register () = ()
