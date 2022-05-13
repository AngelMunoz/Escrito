[<AutoOpen>]
module Components.Shared


open Lit

type SpinnerSize =
    | Small
    | Medium
    | Large

    member this.Value =
        match this with
        | Small -> 1
        | Medium -> 2
        | Large -> 3

[<AutoOpen>]
type Shared =
    static member spinner(?size: SpinnerSize) =
        let value = defaultArg size Small

        html
            $"""
            <sl-spinner style="font-size: {value.Value}rem;"></sl-spinner>
        """
