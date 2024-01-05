import {z} from "zod";

export const IngredientSchema = z.object({
  ingredient: z.string(),
  percentage: z.number(),
});

export type Ingredient = z.infer<typeof IngredientSchema>;