
module DomainFunctions
open Recipes.Domain.DomainTypes;
open System;

let getHardcodedrecipe = 
        let recipe = {
        Name="dasdsad";
        Ingredients = [{ Ingredient = Ingredient "";Qty = 2.0M; Unit= Unit.Cup}]
        CookTimeMin = 40.0M;
        Servings = 4;
        Picture =  {Thumbnail = Uri(""); FullSize = Uri("") };
        Description = "sfsf";
        Source = Uri("");
        }
        recipe

let getFixedIngredientsRecepies (ingredients:Ingredient seq) = 
    let recepies = [getHardcodedrecipe]
    recepies

let getRecepies(ingredients: Ingredient seq) = 
    let recipes = [getHardcodedrecipe]
    recipes |> Seq.sortByDescending(fun r -> r.Ingredients.Length)




