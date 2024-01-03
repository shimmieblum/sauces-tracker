import {useState} from "react"
import axios from 'axios';
import {Sauce, SauceSchema} from "../models/SauceSchema";
import {TraceEntryPointsPlugin} from "next/dist/build/webpack/plugins/next-trace-entrypoints-plugin";

const baseUrl = 'http://localhost:5268/sauces'
export const useGetSauces = () => {
  const [ isLoading, setIsLoading] = useState(false);
  const GetSauces = async () => { 
    try{
      setIsLoading(true);
      const response = await axios.get<Sauce[]>(baseUrl);
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

export const useDeleteSauce = (id:string) => {
  const [ isDeleting, setIsDeleting ] = useState(false);
  const deleteSauce = async () => {
    try {
      setIsDeleting(true);
      const endpoint = `${baseUrl}/${id}`;
      const response = await axios.delete(endpoint);
      setIsDeleting(false);
      return response.status === axios.HttpStatusCode.NoContent;
    }
    catch (err){
      console.error(err, {message: `error occured trying to delete sauce id=${id}`})
    }
  }
  return { isDeleting, deleteSauce }
}