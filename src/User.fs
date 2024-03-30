module User

open System
open Sutil
open Sutil.Core
open Sutil.CoreElements
open Stores
open Fable.Core
open Fable.Core.JsInterop
open Router

let user: IStore<Usr option> = Store.make None
let loading: IStore<bool> = Store.make false
let _name: IStore<string> = Store.make ""
let _email: IStore<string> = Store.make ""

let reset () =
    user <~ None
    _name <~ ""
    _email <~ ""

let userView (p: Usr) =
    Html.div
        [
            Attr.className "p-4 w-full h-full"
            Html.a
                [
                    Attr.href "#"
                    Html.i [ Attr.className "gg-arrow-left" ]
                    Ev.onClick (fun ev ->
                        ev.preventDefault ()
                        ev.stopPropagation ()
                        navigateTo ev (Users) true
                    )
                ]
            Html.div
                [
                    Html.div
                        [
                            Attr.className "w-1/2 h-full"
                            Html.form
                                [
                                    Attr.className "w-full h-full"
                                    Ev.onSubmit (fun ev ->
                                        ev.preventDefault ()

                                        let _p = { p with Name = Store.get _name; Email = Store.get _email }

                                        loading <~ true

                                        promise { return! Promise.sleep 2000 }
                                        |> Promise.map (fun i_ ->
                                            let _usrs =
                                                Stores.users |> Store.get |> List.filter (fun u -> u.Id <> p.Id)

                                            Stores.users <~ _p :: _usrs
                                            Stores.users |> Store.get |> printfn "%A"
                                            loading <~ false
                                        )
                                        |> Promise.start

                                    )

                                    Html.div
                                        [
                                            Attr.className "grid grid-cols-3 py-2 text-gray-900 dark:text-white"
                                            Html.p
                                                [
                                                    Attr.className "text-gray-900 dark:text-white font-bold"
                                                    Html.text "Name : "
                                                ]
                                            Html.div
                                                [
                                                    Attr.className "col-span-2"
                                                    Html.input
                                                        [
                                                            Bind.attr ("value", _name)
                                                            Attr.required true
                                                            Attr.className
                                                                "bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white"

                                                        ]
                                                ]

                                        ]

                                    Html.div
                                        [
                                            Attr.className "grid grid-cols-3 py-2 text-gray-900 dark:text-white"
                                            Html.p
                                                [
                                                    Attr.className "text-gray-900 dark:text-white font-bold"
                                                    Html.text "Email : "
                                                ]
                                            Html.div
                                                [
                                                    Attr.className "col-span-2"
                                                    Html.input
                                                        [
                                                            Bind.attr ("value", _email)
                                                            Attr.required true
                                                            Attr.className
                                                                "bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white"

                                                        ]
                                                ]

                                        ]

                                    Html.div
                                        [
                                            Attr.className "flex flex-row justify-end py-8"
                                            Html.button
                                                [
                                                    Attr.type' "submit"
                                                    Attr.className
                                                        "text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
                                                    Html.text "Save"
                                                ]
                                        ]
                                ]
                        ]
                ]

        ]

let userShow userId =
    Html.div
        [
            Attr.className "w-full h-full p-4"
            Html.div
                [
                    Attr.className "flow-root"
                    onMount
                        (fun _ ->
                            reset ()

                            loading <~ true

                            promise { return! Promise.sleep 2000 }
                            |> Promise.map (fun i_ ->
                                loading <~ false
                                let _users = Stores.users |> Store.get
                                printfn "%A" userId
                                printfn "%A" _users

                                let u = _users |> List.takeWhile (fun u -> u.Id = userId) |> List.head
                                printfn "%A" u
                                user <~ Some u
                                _name <~ u.Name
                                _email <~ u.Email
                            )
                            |> Promise.start

                        )
                        []
                    Bind.el (
                        loading,
                        fun _loading ->
                            if _loading then
                                Html.div
                                    [
                                        Attr.className "flex mx-auto items-center justify-center h-full"
                                        Html.div
                                            [
                                                Attr.role "status"
                                                Attr.className "w-24"
                                                Html.span [ Attr.className "sr-only"; Html.text "Loading..." ]
                                                Html.img [ Attr.src "/loading.svg" ]
                                            ]
                                    ]
                            else
                                Html.div
                                    [
                                        Attr.className "flex w-screen h-full"
                                        Bind.el (
                                            user,
                                            fun u ->
                                                match u with
                                                | Some _u -> userView _u
                                                | _ ->
                                                    let _u = { Id = 0; Name = ""; Email = "" }
                                                    userView _u
                                        )
                                    ]
                    )
                ]
        ]

let view userId =
    Html.div
        [
            disposeOnUnmount [ user; loading ]
            onMount
                (fun _ ->
                    async { reset () } |> Async.Start
                    ignore <| Helper.initFlowbite ()
                )
                []
            Html.section [ Attr.className "min-h-full"; userShow userId ]
        ]
