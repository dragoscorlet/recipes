module DataAccess 

    open MongoDB.Bson
    open MongoDB.Driver
    open MongoDB.FSharp
    open System
    open Literals
    open Recipes.Entities

    type Ingredient = {Id : BsonObjectId; IngredientName:string}

    type RecipeIngredient = {FreeIngredient: Ingredient; Qty:decimal; Unit: Entities.Unit}

    type PictureLocation = {Thumbnail:Uri; FullSize:Uri}

    type Recipe = {
        Id: BsonObjectId;
        Name: string; 
        Ingredients: System.Collections.Generic.List<RecipeIngredient>; 
        CookTimeMin: decimal; 
        Servings: int; 
        Picture: PictureLocation;
        Description: string
        Source: Uri}


    let client = MongoClient(ConnectionString)
    let db = client.GetDatabase(DbName)
    let ingredientCollection = db.GetCollection<Ingredient>(IngredientCollectionName)
    let recipeCollection = db.GetCollection<Recipe>(RecipeCollectionName)

    let createRecipe (recipe : Recipe) = 
        recipeCollection.InsertOne(recipe)
    
    let createIngredient(ingredient: Ingredient) = 
        ingredientCollection.InsertOne(ingredient)
    
    let createManyRecipes(recipes : Recipe list) = 
        recipeCollection.InsertMany(recipes)

    let createManyIngredients(ingredients : Ingredient list) = 
        ingredientCollection.InsertMany(ingredients)

    
    let readIngredientOnName(ingredientName : string) = 
        ingredientCollection.Find(fun i -> i.IngredientName = ingredientName).ToEnumerable()

    let readAllIngredients() = 
        ingredientCollection.Find(Builders.Filter.Empty).ToEnumerable()

    let readAllRecipes () = 
        recipeCollection.Find(Builders.Filter.Empty).ToEnumerable()

    let containsAtLeastOne (source: Ingredient list) (target: Ingredient list) = 
        let sourceIds = source |> Seq.map  (fun i -> i.Id.ToString()) |> Seq.toList
        let targetIds = target |> Seq.map (fun i -> i.Id.ToString()) |> Seq.toList
        let intersection = 
            Set.intersect (Set.ofList sourceIds) (Set.ofList targetIds) |> Set.toList
        intersection.Length > 0
    
    let getFreeIngredient (recipeIngredients: RecipeIngredient seq) = 
        recipeIngredients |> Seq.map (fun r -> r.FreeIngredient) |> Seq.toList
    
    let readRecipiesWithIngredients(ingredientNames: string list) = 
        let ingredients = ingredientNames 
                        |> Seq.map (fun i -> {Id = BsonObjectId(ObjectId.GenerateNewId()); IngredientName = i }) 
                        |> Seq.toList

        recipeCollection.Find(fun i -> 
        containsAtLeastOne  (getFreeIngredient i.Ingredients) ingredients).ToEnumerable()

    let deleteIngredientById (ingredientId : BsonObjectId) = 
        ingredientCollection.DeleteOne( fun i -> i.Id = ingredientId)

    let deleteRecipeById (recipeId : BsonObjectId) = 
        recipeCollection.DeleteOne(fun r -> r.Id = recipeId)
   