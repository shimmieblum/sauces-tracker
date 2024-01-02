import { z } from 'zod'

const IngredientSchema = z.object({
  ingredient: z.string(),
  percentage: z.number(),
});

export const FermentationSchema = z.object({
  ingredients: z.array(IngredientSchema),
  lengthInDays: z.number(),
});

export const SauceSchema = z.object({
  name: z.string(),
  id: z.string(),
  fermentation: FermentationSchema,
  fermentationPercentage: z.number(),
  nonFermentedIngredients: z.array(IngredientSchema),
  notes: z.string(),
});

export type Sauce = z.infer<typeof SauceSchema>;
export type Fermentation = z.infer<typeof FermentationSchema>;


