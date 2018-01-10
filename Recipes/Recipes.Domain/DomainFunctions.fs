
module DomainFunctions
open DataAccess
open Recipes.Domain.DomainTypes;
open System;

let getIngredientName ingredient = 
    match ingredient with
        Ingredient name -> name

let containsSameIngredients source target = 
    let containsAll = 
        source |> Seq.forall (fun i -> target |> Seq.exists (fun ri -> getIngredientName ri.Ingredient = getIngredientName i))
    let equalSize = (source |> Seq.toList).Length = (target |> Seq.toList).Length

    equalSize && containsAll

let mapIngredientsToDomain (dbIngredients:System.Collections.Generic.List<DataAccess.RecipeIngredient>) = 
    dbIngredients |> Seq.map (fun i -> {Ingredient = Ingredient i.FreeIngredient.IngredientName; Qty = i.Qty; Unit = i.Unit} ) 
    |> Seq.toList

let readRecepies(ingredients: Ingredient seq) = 
    let ingredientNames = ingredients 
                        |> Seq.map (fun i ->  match i with Ingredient name -> name) 
                        |> Seq.toList
    
    DataAccess.readRecipiesWithIngredients (ingredientNames)
    |> Seq.map (fun recipe -> 
                        {Name = recipe.Name; 
                        Picture = {Thumbnail = recipe.Picture.Thumbnail; FullSize = recipe.Picture.FullSize }; 
                        Description = recipe.Description; 
                        Source = recipe.Source; 
                        Servings = recipe.Servings; 
                        CookTimeMin = recipe.CookTimeMin; 
                        Ingredients = mapIngredientsToDomain recipe.Ingredients})

let readFixedIngredientsRecepies (ingredients:Ingredient seq) = 
    let recipes = readRecepies ingredients
    recipes |> Seq.where (fun recipe -> containsSameIngredients ingredients (recipe.Ingredients))

