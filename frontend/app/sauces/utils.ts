import slugify from "slugify";
import {SauceResponse} from "../../models/SauceResponseSchema";

const sauceNameToSlug = (title:string) => {
  const uriSlug = slugify(title, {lower:true, trim: true});
  return encodeURI(uriSlug);
}

export const getSauceSlug = (sauce:SauceResponse) => `${sauceNameToSlug(sauce.name)}_${sauce.id}`;
export const getIdFromSlug = (slug:string) => slug.split('_').pop();

