import {Ingredient} from "./IngredientSchema";
import exp from "node:constants";

export type SauceUpdateRequest = {
  name: string;
  fermentationRecipe: UpdateFermentationRecipeRequest;
  fermentationPercentage: number;
  nonFermentedIngredients: Ingredient[];
  notes: string;
}

type UpdateFermentationRecipeRequest = {
  id: string;
  ingredients: Ingredient[];
  lengthInDays: number;
}