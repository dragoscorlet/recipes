namespace Recipes.Domain

open System;

[<AutoOpen>]
module DomainTypes = 

    type Unit = Kilogram  = 0
                |Gram = 1
                |Liter = 2
                |Deciliter = 3
                |Milliliter = 4
                |Cup = 5
                |TableSpoon = 6
                |DessertSpoon = 7
                |TeaSpoon = 8
                |Pound = 9
                |Ounce = 10
                |PintUs = 11
                |PingGB = 12
                |FluidOunceUS = 13
                |FluidOunceGB = 14

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
