import {z} from 'zod'
import {IngredientSchema} from "./IngredientSchema";

export const FermentationResponseSchema = z.object({
  ingredients: z.array(IngredientSchema),
  lengthInDays: z.number(),
});

export const SauceSchema = z.object({
  name: z.string(),
  id: z.string(),
  fermentation: FermentationResponseSchema,
  fermentationPercentage: z.number(),
  nonFermentedIngredients: z.array(IngredientSchema),
  notes: z.string(),
});

export type SauceResponse = z.infer<typeof SauceSchema>;
export type FermentationResponse = z.infer<typeof FermentationResponseSchema>;


