namespace Recipes.Domain

open System;

[<AutoOpen>]
module DomainTypes = 

    type Unit = Kilogram
                |Gram
                |Liter
                |Deciliter
                |Milliliter
                |Cup
                |TableSpoon
                |DessertSpoon
                |TeaSpoon
                |Pound
                |Ounce
                |PintUs
                |PingGB
                |FluidOunceUS
                |FluidOunceGB

    type Ingredient = Ingredient of string

    type RecipeIngredient = {Ingredient: Ingredient; Qty:decimal; Unit:Unit}

    type PictureLocation = {Thumbnail:Uri; FullSize:Uri}

    type Recipe = {
        Name: string; 
        Ingredients: RecipeIngredient list; 
        CookTimeMin: decimal; 
        Servings: int; 
        Picture: PictureLocation;
        Description: string
        Source: Uri}