module Router

open System
open Browser
open Browser.Url
open Sutil
open Sutil.Core
open Fable.Core
open Stores

let router: IStore<Route> = Store.make Init

let makeUri (route: Route) : string =
    match route with
    | Users -> "/"
    | User id -> $"/{id}"
    | _ -> "/notfound"

let makeRoute (url: Types.URL) : Route =
    match url.pathname with
    | "" -> Users
    | "/" -> Users
    | "/#" -> Users
    | "/notfound" -> NotFound
    | path -> path.TrimStart('/') |> int |> User

let updateRoute (route: Route) : unit = if router.Value = route then ignore 0 else router <~ route

let updateTitle (route: Route) : unit =
    let title =
        match route with
        | Users -> "Users"
        | User _ -> "User"
        | _ -> "Page Not Found"

    Dom.document.title <- title

let navigateTo (ev: Browser.Types.Event) (route: Route) (replace: bool) : unit =
    ev.preventDefault () |> ignore
    ev.stopPropagation () |> ignore
    ev.stopImmediatePropagation () |> ignore
    updateRoute route
    let uri = makeUri route
    if replace then history.replaceState (history.state, "", uri) else history.pushState (history.state, "", uri)
    updateTitle route

let urlChange () =
    let uriString = Browser.Dom.document.URL
    let currentDoc = Browser.Dom.window.document
    let uri = URL.Create(currentDoc.location.href)
    let route = uri |> makeRoute
    route |> updateRoute
    route |> updateTitle

let startRouter (r: Route option) =
    match r with
    | Some route ->
        route |> updateRoute
        route |> updateTitle
    | None -> urlChange ()

    Dom.window.onpopstate <- (fun ev -> urlChange ())
