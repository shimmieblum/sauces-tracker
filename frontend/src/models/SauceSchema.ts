import { z } from 'zod'

const SauceSchema = z.object({
  name: z.string(),
  id: z.string(),
  fermentation: z.map(z.string(), z.number()),
  fermentationPercentage: ,
  nonFermentedIngredients: ,
  notes: ,
})
