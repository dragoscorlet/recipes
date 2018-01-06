namespace Recipes.Domain

open System;

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

type Ingredient = {Name:string; Qty:int; Unit:Unit}

type PictureLocation = {Thumbnail:Uri; FullSize:Uri}

type Recipe = {
    Name: string; 
    Ingredients: Ingredient list; 
    CookTimeMin: int; 
    Servings: int; 
    Picture: PictureLocation;
    Description: string
    Source: Uri}