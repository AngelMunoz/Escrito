module Pages.About


open Lit

let View () =
    html
        $"""
        <article>
            <header>
                <h1>Escrito</h1>
                <h5><i>Because nobody needs this...</i></h5>
            </header>
            <p>
                Escrito is a web application that allows you to write and share text fragments with slots,
                so you can create some sort of "fill in the blanks" dialog styles.
            </p>
            <p>
                This is built with <sl-button size="small" variant="text" href="https://github.com/AngelMunoz/Perla" target="_blank">Perla</sl-button size="small">,
                <sl-button size="small" variant="text" href="https://github.com/fable-compiler/Fable.Lit" target="_blank">Fable.Lit</sl-button size="small">, 
                and <sl-button size="small" variant="text" href="https://shoelace.style" target="_blank">Shoelace</sl-button size="small">
                <br>
                Take the power of Custom Elements and Web Components flexibility together with F# type safety and correctness to produce
                fast, efficient, and reliable web applications.
            </p>
            <br>
            <h4>If you want to learn more about this ping me on social media</h4>
            <sl-button-group>
                <sl-button variant="outline" href="https://twitter.com/angel_d_munoz" target="_blank">
                    <sl-icon name="twitter"></sl-icon>
                    twitter
                </sl-button>
                <sl-button variant="outline" href="https://github.com/AngelMunoz" target="_blank">
                    <sl-icon name="github"></sl-icon>
                    github
                </sl-button>
                <sl-button variant="outline" href="https://dev.to/tunaxor" target="_blank">
                    <sl-icon name="journals"></sl-icon>
                    dev.to
                </sl-button>
            </sl-button-group>
        </article>"""
