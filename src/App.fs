module MainTabs

open System
open Sutil
open Sutil.Core
open Sutil.CoreElements
open Sutil.Styling
open Stores
open Fable.Core
open Fable.Core.JsInterop
open Router

importSideEffects "./app.css"

let showPage (route: Route) =
    match route with
    | Users -> Users.view ()
    | User userId -> User.view userId
    | NotFound ->
        Html.div
            [
                Attr.className "flex flex-col items-center justify-center"
                Html.p
                    [
                        Attr.className "text-3xl md:text-4xl lg:text-5xl text-gray-800 mt-12"
                        Html.text "Page Not Found"
                    ]
                Html.p
                    [
                        Attr.className "md:text-lg lg:text-xl text-gray-600 mt-8"
                        Html.text "Sorry, the page you are looking for could not be found."
                    ]
                Html.a
                    [
                        Ev.onClick (fun ev -> navigateTo ev Users false)
                        Attr.href "#"
                        Attr.className
                            "flex items-center space-x-2 bg-blue-600 hover:bg-blue-700 text-gray-100 px-4 py-2 mt-12 rounded transition duration-150"
                        Attr.title "Return Home"
                        Html.span [ Html.text "Return Home" ]
                    ]
            ]
    | _ -> text "Loading"

let view () =
    Html.div
        [
            disposeOnUnmount [ router ]
            onMount
                (fun _ ->

                    ignore <| Router.startRouter None
                )
                []

            Bind.el (Router.router, showPage)

        ]

let start (id': string) =
    let app = view ()
    Program.mount (id', app)

ignore <| start "app"
