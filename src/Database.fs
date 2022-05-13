module Database

open Types
open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

[<ImportMember "./database.js">]
let getTexts () : Promise<EsText [] * float> = jsNative
