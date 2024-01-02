import { useEffect, useState } from "react"
import axios from 'axios';
import { Sauce, SauceSchema } from "../models/SauceSchema";

export const useGetSauces = () => {
  const [ isLoading, setIsLoading] = useState(false);
  const GetSauces = async () => { 
    try{
      setIsLoading(true);
      const endpoint = 'http://localhost:5268/sauces';
      const response = await axios.get<Sauce[]>(endpoint);
      response.data.forEach(sauce => SauceSchema.parse(sauce));
      setIsLoading(false);
      return response.data;
    }
    catch(err){
      console.error(err, {message:'error occured'})
    }
  };
  return {isLoading, GetSauces};
}