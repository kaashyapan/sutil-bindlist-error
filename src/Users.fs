module Users

open System
open Sutil
open Sutil.Core
open Sutil.CoreElements
open Stores
open Fable.Core
open Fable.Core.JsInterop
open Router

let loading: IStore<bool> = Store.make false

let reset () = Stores.users <~ []

let usersView (u: IObservable<Usr>) : SutilElement =
    let p = u |> Store.current

    Html.tr
        [
            Attr.className
                "w-full bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600"
            Html.td [ Attr.className "px-6 py-4"; Html.text p.Name ]
            Html.td [ Attr.className "px-6 py-4"; Html.p p.Id ]
            Html.td [ Attr.className "px-6 py-4"; Html.p p.Email ]
            Html.td
                [
                    Html.a
                        [
                            Attr.href "#"
                            Ev.onClick (fun ev ->
                                ev.preventDefault ()
                                navigateTo ev (User p.Id) false
                            )
                            Html.i [ Attr.className "gg-pen" ]
                        ]
                ]
        ]

let usersIndex =
    Html.div
        [
            Attr.className "w-full h-full p-4"
            Html.div
                [
                    Attr.className "flex items-center justify-between pb-4"
                    Html.div
                        [
                            Attr.className "flex items-center justify-start gap-4"
                            Html.h5
                                [
                                    Attr.className "text-xl font-bold leading-none text-gray-900 dark:text-white"
                                    Html.text "Users"
                                ]
                        ]
                ]

            Html.div
                [
                    Attr.className "flow-root"
                    onMount
                        (fun _ ->
                            loading <~ true
                            printfn "%A" "Mount"

                            promise { return! Promise.sleep 2000 }
                            |> Promise.map (fun _ ->
                                let _users =
                                    [
                                        { Usr.Name = "Donald Trump"; Id = 1; Email = "trump@usa.com" }
                                        { Usr.Name = "Joe Biden"; Id = 2; Email = "biden@usa.com" }
                                    ]

                                Stores.users <~ _users
                                loading <~ false
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
                                        Attr.className "flex mx-auto items-center justify-center h-[calc(100svh-10rem)]"
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
                                        Attr.className "flex w-screen"
                                        Html.div
                                            [
                                                Attr.className "p-4 flex flex-col"
                                                Html.div
                                                    [
                                                        Attr.className "relative pt-2 overflow-x-auto"
                                                        Html.table
                                                            [
                                                                Attr.className
                                                                    "w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400"
                                                                Html.thead
                                                                    [
                                                                        Attr.className
                                                                            "text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400"
                                                                        Html.tr
                                                                            [

                                                                                Html.th
                                                                                    [
                                                                                        Attr.className
                                                                                            "px-6 py-4 text-gray-900 whitespace-nowrap dark:text-white"
                                                                                        Html.text "Name"
                                                                                    ]
                                                                                Html.th
                                                                                    [
                                                                                        Attr.className
                                                                                            "px-6 py-4 text-gray-900 whitespace-nowrap dark:text-white"
                                                                                        Html.text "ID"
                                                                                    ]
                                                                                Html.th
                                                                                    [
                                                                                        Attr.className
                                                                                            "px-6 py-4 text-gray-900 whitespace-nowrap dark:text-white"
                                                                                        Html.text "Email"
                                                                                    ]
                                                                            ]
                                                                    ]
                                                                Html.tbody
                                                                    [
                                                                        Bind.each (
                                                                            Stores.users,
                                                                            (fun u -> usersView u),
                                                                            (fun u -> u.Id)
                                                                        )
                                                                    ]
                                                            ]
                                                    ]
                                            ]
                                    ]
                    )
                ]

        ]

let view () =
    Html.div
        [
            disposeOnUnmount [ loading ]
            onMount
                (fun _ ->
                    async { reset () } |> Async.Start
                    ignore <| Helper.initFlowbite ()
                )
                []
            Html.section [ Attr.className "min-h-full"; usersIndex ]
        ]
