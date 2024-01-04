
import {z} from 'zod';
import {IngredientSchema} from "./IngredientSchema";

export const FermentationRequestSchema = z.object({
  lengthInDays: z.number(),
  ingredients: z.array(IngredientSchema)
});

export const SauceRequestSchema = z.object({
  name: z.string(),
  notes: z.string(),
  fermentation: FermentationRequestSchema,
  fermentationPercentage: z.number(),
  nonFermentedIngredients: z.array(IngredientSchema)
});

export type SauceRequest = z.infer<typeof SauceRequestSchema>;
export type FermentationRequest = z.infer<typeof FermentationRequestSchema>;