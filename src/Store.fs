module Stores

open Sutil
open Sutil.Core
open Fable.Core
open Fable.Core.JsInterop
open System

type Usr = { Name: string; Email: string; Id: int }

type Route =
    | Users
    | User of int
    | NotFound
    | Init

let users: IStore<Usr list> = Store.make []
