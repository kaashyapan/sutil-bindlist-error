module Layout

open System
open Sutil
open Sutil.Core
open Sutil.CoreElements
open Sutil.Styling
open Stores
open Fable.Core
open Router

let view (e: SutilElement) =
    Html.div
        [
            Html.aside
                [
                    Attr.id "logo-sidebar"
                    Attr.className
                        "fixed top-0 left-0 z-40 w-64 h-screen pt-24 lg:pt-0 transition-transform -translate-x-full bg-gray-800 border-r border-gray-700 sm:translate-x-0 dark:bg-white dark:border-gray-200"
                    Attr.custom ("aria-label", "Sidebar")
                    Html.div
                        [
                            Attr.className "h-full px-3 overflow-y-auto bg-gray-800 dark:bg-white"
                            Html.ul
                                [
                                    Attr.className "space-y-2 font-medium"
                                    Html.li [ Html.a [ Attr.href ""; Attr.className "flex hidden lg:inline" ] ]
                                    Html.li
                                        [
                                            Html.a
                                                [
                                                    Attr.href "#"
                                                    Ev.onClick (fun ev -> navigateTo ev Users false)
                                                    Attr.className
                                                        "flex items-center p-2 dark:text-gray-900 rounded-lg text-white hover:text-gray-900 dark:hover:text-white hover:bg-gray-100 dark:hover:bg-gray-700 group"
                                                    Html.span [ Attr.className "ms-3"; Html.text "Users" ]
                                                ]
                                        ]

                                ]
                        ]
                ]
        ]
