// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open DataAccess
open System
open MongoDB.Bson
open MongoDB.Driver
open MongoDB.FSharp


[<EntryPoint>]
let main argv = 
    
    let ingredient =  {Id = BsonObjectId(ObjectId.GenerateNewId()); IngredientName = "chicken"}
    let ingredients = new System.Collections.Generic.List<RecipeIngredient>()
    ingredients.Add({ FreeIngredient =  ingredient;Qty = 2.0M; Unit= Unit.Cup})
        
    let recipe = {
        Id = BsonObjectId(ObjectId.GenerateNewId())
        Name="Chicken salad";
        Ingredients = ingredients
        CookTimeMin = 40.0M;
        Servings = 4;
        Picture =  {Thumbnail = Uri("http://www.image.com/thum.jpg"); FullSize = Uri("http://www.sddadad.com/full.jpg") };
        Description = "sfsf";
        Source = Uri("http://www.sdasdas.com/someprecipe");   
        }

    DataAccess.createRecipe(recipe)

    let readRecipe = DataAccess.readAllRecipes()

    printfn "%A" argv
    0 // return an integer exit code
