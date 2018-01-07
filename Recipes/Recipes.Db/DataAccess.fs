module DataAccess 

    open MongoDB.Bson
    open MongoDB.Driver
    open MongoDB.FSharp
    open System


    [<Literal>]
    let ConnectionString = "mongodb://localhost:27017"
    [<Literal>]
    let DbName = "local"
    [<Literal>]
    let CollectionName = "PlanetData"
    [<Literal>]
    let IngredientCollectionName = "Ingredients"
    [<Literal>]
    let RecipeCollectionName = "Recipes"
 
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

    type Ingredient = {Id : BsonObjectId; IngredientName:string}

    type RecipeIngredient = {FreeIngredient: Ingredient; Qty:decimal; Unit:int}

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

    let deleteIngredientById (ingredientId : BsonObjectId) = 
        ingredientCollection.DeleteOne( fun i -> i.Id = ingredientId)

    let deleteRecipeById (recipeId : BsonObjectId) = 
        recipeCollection.DeleteOne(fun r -> r.Id = recipeId)