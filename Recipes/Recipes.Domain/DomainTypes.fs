namespace Recipes.Domain

open System;
open Recipes.Entities

[<AutoOpen>]
module DomainTypes = 

    type Ingredient = Ingredient of string

    type RecipeIngredient = {Ingredient: Ingredient; Qty:decimal; Unit:Entities.Unit}

    type PictureLocation = {Thumbnail:Uri; FullSize:Uri}

    type Recipe = {
        Name: string; 
        Ingredients: RecipeIngredient list; 
        CookTimeMin: decimal; 
        Servings: int; 
        Picture: PictureLocation;
        Description: string
        Source: Uri}
