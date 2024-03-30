module Helper

open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop
open Fetch
open Sutil
open System

[<Import("initAccordions", "flowbite")>]
let initAccordions: unit -> unit = jsNative

[<Import("initTabs", "flowbite")>]
let initTabs: unit -> unit = jsNative

[<Import("initDials", "flowbite")>]
let initDials: unit -> unit = jsNative

[<Import("initModals", "flowbite")>]
let initModals: unit -> unit = jsNative

[<Import("initDrawers", "flowbite")>]
let initDrawers: unit -> unit = jsNative

[<Import("initTooltips", "flowbite")>]
let initTooltips: unit -> unit = jsNative

[<Import("initPopovers", "flowbite")>]
let initPopovers: unit -> unit = jsNative

[<Import("initCollapses", "flowbite")>]
let initCollapses: unit -> unit = jsNative

[<Import("initCarousels", "flowbite")>]
let initCarousels: unit -> unit = jsNative

[<Import("initDismisses", "flowbite")>]
let initDismisses: unit -> unit = jsNative

[<Import("initDropdowns", "flowbite")>]
let initDropdowns: unit -> unit = jsNative

[<Import("initInputCounters", "flowbite")>]
let initInputCounters: unit -> unit = jsNative

[<Import("initCopyClipboards", "flowbite")>]
let initCopyClipboards: unit -> unit = jsNative

[<Import("initFlowbite", "flowbite")>]
let initFlowbite: unit -> unit = jsNative
